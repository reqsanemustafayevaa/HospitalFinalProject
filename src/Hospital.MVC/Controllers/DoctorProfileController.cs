using Hospital.Business.Services.Implementations;
using Hospital.Core.Models;
using Hospital.Data.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.MVC.Controllers
{
    [Authorize(Roles ="user")]
    public class DoctorProfileController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
		private readonly DoctorService _doctorService;

		public DoctorProfileController(AppDbContext context,UserManager<AppUser>userManager
                                       ,DoctorService doctorService)
        {
            _context = context;
           _userManager = userManager;
			_doctorService = doctorService;
		}
        public async Task<IActionResult> Index()
        {
            ViewBag.Doctors=await _context.Doctors.ToListAsync();
			//var user = await _userManager.GetUserAsync(User);
			//if (user == null)
			//{
			//	return NotFound();
			//}

			//var doctor = await _context.Doctors
			//	.Include(d => d.Appointments)
			//	.FirstOrDefaultAsync(d => d.UserId == user.Id);

			//if (doctor == null)
			//{
			//	return NotFound();
			//}

			return View();

		}
    }
}
