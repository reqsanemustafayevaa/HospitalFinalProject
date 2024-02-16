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

        public DoctorProfileController(AppDbContext context,UserManager<AppUser>userManager)
        {
            _context = context;
           _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            AppUser appUser = null;

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                appUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            }

            List<Appointment> appointments = await _context.Appointments
            
            .ToListAsync();
            return View(appointments);

        }
    }
}
