using Hospital.Business.ViewModels;
using Hospital.Core.Models;
using Hospital.Data.DAL;
using Hospital.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.MVC.Controllers
{
	[Authorize(Roles ="user,SuperAdmin,Manager")]
	public class AppointmentController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly AppDbContext _context;

		public AppointmentController(UserManager<AppUser>userManager
			                         ,AppDbContext context)
        {
			_userManager = userManager;
			_context = context;
		}
        public async Task<IActionResult> Create()
		{
            ViewBag.Doctors = await _context.Doctors.ToListAsync();
            ViewBag.Professions = await _context.Professions.ToListAsync();
            var user = await _userManager.GetUserAsync(User);
            if (!User.Identity.IsAuthenticated)
			{
				return RedirectToAction("login", "Account");
			}
            var viewModel = new AppointmentViewModel
            {
				AppUserId = user.Id
			};

			return View(viewModel);

		}
		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public async Task<IActionResult> Create(AppointmentViewModel appointmentViewModel)
		{
            ViewBag.Doctors = await _context.Doctors.ToListAsync();
            ViewBag.Professions = await _context.Professions.ToListAsync();
            if (!ModelState.IsValid)
			{

				
				return View(appointmentViewModel);
			}
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var doctor = await _context.Doctors
         .Include(d => d.Profession)
         .FirstOrDefaultAsync(d => d.Id == appointmentViewModel.DocotrId);

            if (doctor == null)
            {
                ModelState.AddModelError("Doctor", "Invalid doctor selected.");
               
                return View(appointmentViewModel);
            }
            if (doctor.Profession.Id != appointmentViewModel.Professionnid)
            {
                ModelState.AddModelError("Professionnid", "Selected doctor does not belong to the selected profession.");
               
                return View(appointmentViewModel);
            }

            if (appointmentViewModel.AppointmentDate.Date < DateTime.UtcNow)
            {
                ModelState.AddModelError("AppointmentDate", "Appointment date cannot be in the past.");
                
                return View(appointmentViewModel);
            }
            var isAppointmentValid = await IsAppointmentValid(appointmentViewModel);
            if (!isAppointmentValid)
            {

                ModelState.AddModelError("", "Appointment date and time overlap with doctor's schedule.");
               
                return View(appointmentViewModel);
            }





            var appointment = new Appointment()
            {
                FullName = appointmentViewModel.FullName,
                Phone = appointmentViewModel.Phone,
                AppointmentDate = appointmentViewModel.AppointmentDate,
                AppointmentStartTime = appointmentViewModel.AppointmentStartTime,
                AppointmentEndTime = appointmentViewModel.AppointmentEndTime,
                DoctorId = appointmentViewModel.DocotrId,
                AppUserId = user?.Id,
                Email = appointmentViewModel.Email,
                ProfessionId = appointmentViewModel.Professionnid,
                Note = appointmentViewModel.Note,
                CreateDate = DateTime.UtcNow.AddHours(4),
               IsDeleted=false,

				
			};


			


			_context.Appointments.Add(appointment);
			await _context.SaveChangesAsync();


			return RedirectToAction("profile", "account");
		}
        private async Task<bool> IsAppointmentValid(AppointmentViewModel model)
        {
           
            var doctorSchedule = await _context.WorkSchedules
                .FirstOrDefaultAsync(ws => ws.DoctorId == model.DocotrId && ws.Day == model.AppointmentDate.DayOfWeek);

            
            if (doctorSchedule == null || model.AppointmentDate == null)
            {
                return false;
            }

           
            TimeSpan appointmentStartTime = model.AppointmentDate.TimeOfDay.Add(model.AppointmentStartTime);
            TimeSpan appointmentEndTime = model.AppointmentDate.TimeOfDay.Add(model.AppointmentEndTime);

           
            if (doctorSchedule.StartTime.Hours >= 6 && doctorSchedule.StartTime.Hours < 12)
            {
               
                if ( appointmentEndTime > doctorSchedule.EndTime)
                {
                    return false;
                }
            }
            else
            {
               
                if (appointmentStartTime < doctorSchedule.StartTime || appointmentEndTime > doctorSchedule.EndTime)
                {
                    return false;
                }
            }

            return true;
        }

    }
}
