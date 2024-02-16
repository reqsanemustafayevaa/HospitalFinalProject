using Hospital.Business.CustomExceptions.AppointmentExceptions;
using Hospital.Business.CustomExceptions.CommonExceptions;
using Hospital.Business.Services.Interfaces;
using Hospital.Business.ViewModels;
using Hospital.Core.Models;
using Hospital.Core.Repositories.Interfaces;
using Hospital.Data.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.Services.Implementations
{
    public class AppointmentService : IAppointmentService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public AppointmentService(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task Create(AppointmentViewModel appointmentViewModel)
        {
            var user =await _userManager.FindByIdAsync(appointmentViewModel.AppUserId);
            var doctor = await _context.Doctors
         .Include(d => d.Profession)
         .FirstOrDefaultAsync(d => d.Id == appointmentViewModel.DocotrId);

            if (doctor == null)
            {


                throw new EntityNotFoundException("Doctor", "Invalid doctor selected.");
            }
            if (doctor.Profession.Id != appointmentViewModel.Professionnid)
            {


                throw new EntityNotFoundException("Professionnid", "Selected doctor does not belong to the selected profession.");
            }

            if (appointmentViewModel.AppointmentDate.Date < DateTime.UtcNow)
            {


                throw new AppointmentTimeException("AppointmentDate", "Appointment date cannot be in the past.");
            }
            var isAppointmentValid = await IsAppointmentValid(appointmentViewModel);
            if (!isAppointmentValid)
            {



                throw new AppointmentTimeException("", "Appointment date and time overlap with doctor's schedule.");
            }





            var appointment = new Appointment()
            {
                FullName = appointmentViewModel.FullName,
                Phone = appointmentViewModel.Phone,
                AppointmentDate = appointmentViewModel.AppointmentDate,
                AppointmentStartTime = appointmentViewModel.AppointmentStartTime,
                AppointmentEndTime = appointmentViewModel.AppointmentEndTime,
                DoctorId = appointmentViewModel.DocotrId,
                AppUserId = appointmentViewModel.appUser?.Id,
                Email = appointmentViewModel.Email,
                ProfessionId = appointmentViewModel.Professionnid,
                Note = appointmentViewModel.Note
            };
            await _context.AddAsync(appointmentViewModel);
            await _context.SaveChangesAsync();

        }

        public async Task<bool> IsAppointmentValid(AppointmentViewModel appointmentViewModel)
        {
            var doctorSchedule = await _context.WorkSchedules
        .FirstOrDefaultAsync(ws => ws.DoctorId == appointmentViewModel.DocotrId && ws.Day == appointmentViewModel.AppointmentDate.DayOfWeek);


            if (doctorSchedule == null)
            {
                throw new EntityNotFoundException("", "Required!");
            }

            if ((appointmentViewModel.AppointmentStartTime < doctorSchedule.StartTime
                || appointmentViewModel.AppointmentEndTime > doctorSchedule.EndTime) ||
                (appointmentViewModel.AppointmentStartTime > doctorSchedule.EndTime
                || appointmentViewModel.AppointmentEndTime < doctorSchedule.StartTime))
            {
                throw new EntityNotFoundException("", "Required!");
            }







            return true;
        }
    }
}
