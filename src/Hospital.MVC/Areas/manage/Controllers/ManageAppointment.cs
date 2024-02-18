using Hospital.Business.CustomExceptions.CommonExceptions;
using Hospital.Core.Models;
using Hospital.Data.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.MVC.Areas.manage.Controllers
{
	[Area("manage")]
	[Authorize(Roles ="Manager")]
	public class ManageAppointment : Controller
	{
		private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ManageAppointment(AppDbContext context,UserManager<AppUser>userManager)
        {
			_context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
		{
			ViewBag.Doctors=await _context.Doctors.ToListAsync();
			List<Appointment>appointments=await _context.Appointments.ToListAsync();

			return View(appointments);
		}
		public async Task<IActionResult> Detail(int id)
		{
            ViewBag.Doctors = await _context.Doctors.ToListAsync();
            Appointment appointment =await _context.Appointments.Include(a => a.appUser).FirstOrDefaultAsync(x => x.Id==id);
			if(appointment == null)
			{
				return View("error");
			}
			return View(appointment);
		}
		public async Task<IActionResult> Accept(int id)
		{
			Appointment appointment=await _context.Appointments.FirstOrDefaultAsync(x => x.Id==id);
            if (appointment == null)
            {
                return View("error");
            }
			appointment.AppointmentStatus = Core.Enums.AppointmentStatus.Accepted;
            await _context.SaveChangesAsync();
            return RedirectToAction("index","ManageAppointment");
		}
        [HttpPost]
        public async Task<IActionResult> Reject(int id,string Comment)
        {
            ViewBag.Doctors = await _context.Doctors.ToListAsync();
            Appointment appointment = await _context.Appointments.Include(a => a.appUser).FirstOrDefaultAsync(x => x.Id == id);
            if (appointment == null)
            {
                return View("error");
            }
            if (Comment == null)
            {
                ModelState.AddModelError("Comment", "Must be written!");
                return View("detail", appointment);
            }
            if (Comment.Length > 60)
            {
               
                return View("reject");
            }
            appointment.Comment = Comment;
            appointment.AppointmentStatus = Core.Enums.AppointmentStatus.Rejected;
            await _context.SaveChangesAsync();
            return RedirectToAction("index", "ManageAppointment");
        }

    }
}
