using Hospital.Business.CustomExceptions.CommonExceptions;
using Hospital.Business.CustomExceptions.ImageExceptions;
using Hospital.Business.CustomExceptions.ProfessionExceptions;
using Hospital.Business.Extentions;
using Hospital.Business.Services.Interfaces;
using Hospital.Core.Models;
using Hospital.Core.Repositories.Interfaces;
using Hospital.Data.DAL;
using Hospital.Data.Repositories.Implementations;
using Microsoft.AspNetCore.Hosting;
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
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IWebHostEnvironment _env;
        private readonly IProfessionRepository _professionRepository;
        private readonly IWorkScheduleRepository _workScheduleRepository;
        private readonly UserManager<AppUser> _userManager;
		private readonly AppDbContext _context;

		public DoctorService(IDoctorRepository doctorRepository
                            ,IWebHostEnvironment env
                            ,IProfessionRepository professionRepository
                            ,IWorkScheduleRepository workScheduleRepository
                            ,UserManager<AppUser> userManager
                            ,AppDbContext context
                           )
        {
            _doctorRepository = doctorRepository;
           _env = env;
          _professionRepository = professionRepository;
           _workScheduleRepository = workScheduleRepository;
           _userManager = userManager;
			_context = context;
		}
        public async Task CreateAsync(Doctor doctor)
        {


			

			if (doctor == null)
            {
                throw new EntityNotFoundException();
            }
            if (!_professionRepository.Table.Any(x => x.Id == doctor.ProfessionId))
            {
                throw new ProfessionNotFoundException("ProfessionId", "Profession not found");
            }
            if (doctor.ImageFile!=null)
            {
                if(doctor.ImageFile.ContentType!="image/jpeg" && doctor.ImageFile.ContentType != "image/png")
                {
                    throw new InvalidImageContentException("ImageFile", "File must be .png or  .jpeg");
                }
                if (doctor.ImageFile.Length > 1048576)
                {
                    throw new InvalidImageSizeException("ImageFile", "File must be lower than 1 mb!");
                }
                doctor.ImageUrl = doctor.ImageFile.SaveFile(_env.WebRootPath, "uploads/doctors");
            }
            else
            {
                throw new ImageRequiredException("ImageFile", "Image must be choosed!");
            }
           
            doctor.IsDeleted = false;
            doctor.CreateDate = DateTime.UtcNow;
            doctor.UpdateDate = DateTime.UtcNow;
            
            await _doctorRepository.createAsync(doctor);
            await _doctorRepository.CommitAsync();

        }

        public async Task Delete(int id)
        {
            var doctor=await _doctorRepository.GetAsync(x=>x.Id==id && x.IsDeleted==false);
            if (doctor == null)
            {
                throw new EntityNotFoundException();
            }
            Helper.DeleteFile(_env.WebRootPath, "uploads/doctors", doctor.ImageUrl);
            _doctorRepository.Delete(doctor);
            await _doctorRepository.CommitAsync();
        }

        public async Task<List<Doctor>> GetAllAsync()
        {
            return await _doctorRepository.GetAllAsync().ToListAsync();   
        }

        public async Task<Doctor> GetByIdAsync(int id)
        {
            var existdoctor = await _doctorRepository.GetAsync(x => x.Id == id && x.IsDeleted == false);
            if (existdoctor == null)
            {
                throw new EntityNotFoundException();
            }
           
            return existdoctor;
        }

		
		public async Task UpdateAsync(Doctor doctor)
        {
            if (doctor == null)
            {
                throw new EntityNotFoundException();
            }
            var existdoctor=await _doctorRepository.GetDoctorByIdAsync(doctor.Id);
            if(existdoctor == null)
            {
                throw new EntityNotFoundException();
            }
            if (!_professionRepository.Table.Any(x => x.Id == doctor.ProfessionId))
            {
                throw new ProfessionNotFoundException("ProfessionId", "Profession not found");
            }
           

            if (doctor.ImageFile != null)
            {
                if (doctor.ImageFile.ContentType != "image/jpeg" && doctor.ImageFile.ContentType != "image/png")
                {
                    throw new InvalidImageContentException("ImageFile", "File must be .png or  .jpeg");
                }
                if (doctor.ImageFile.Length > 1048576)
                {
                    throw new InvalidImageSizeException("ImageFile", "File must be lower than 1 mb!");
                }
                Helper.DeleteFile(_env.WebRootPath, "uploads/doctors",existdoctor.ImageUrl);

                
                existdoctor.ImageUrl = doctor.ImageFile.SaveFile(_env.WebRootPath, "uploads/doctors");

            }
           
            else
            {
                throw new ImageRequiredException("ImageFile", "Image must be choosed!");
            }
            if (doctor.WorkSchedules != null)
            {
                var newWorkSchedules = new List<WorkSchedule>();

              
                foreach (WorkSchedule workSchedule in doctor.WorkSchedules)
                {
                    WorkSchedule newWorkSchedule = new WorkSchedule()
                    {
                        DoctorId = doctor.Id,
                        StartTime = workSchedule.StartTime,
                        EndTime = workSchedule.EndTime,
                        Day = workSchedule.Day,
                    };
                    newWorkSchedules.Add(newWorkSchedule);
                }

               
                existdoctor.WorkSchedules = newWorkSchedules;
            }  
            else
            {
                throw new ArgumentNullException("WorkSchedules", "WorkSchedules cannot be null");
            }

            existdoctor.Fullname = doctor.Fullname;
            existdoctor.Description = doctor.Description;
            existdoctor.ProfessionId= doctor.ProfessionId;
            existdoctor.Email= doctor.Email;
          
            existdoctor.Degree = doctor.Degree;
            existdoctor.Institute = doctor.Institute;
           
          
            existdoctor.Phone = doctor.Phone;
            existdoctor.Office = doctor.Office;
            existdoctor.Year = doctor.Year;
            await _doctorRepository.CommitAsync();
        }
    }
}
