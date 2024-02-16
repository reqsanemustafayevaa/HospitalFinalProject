using Hospital.Core.Models;
using Hospital.Data.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.MVC.Controllers
{
    [Authorize(Roles ="user")]
    public class DoctorProfileController : Controller
    {
        private readonly AppDbContext _context;

        public DoctorProfileController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Doctors = await _context.Doctors.ToListAsync();
            List<Appointment> appointments = await _context.Appointments.ToListAsync();
            return View(appointments);
        }
    }
}
