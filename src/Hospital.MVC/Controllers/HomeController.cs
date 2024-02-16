
using Hospital.Data.DAL;
using Hospital.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Hospital.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            DoctorViewModel doctorViewModel = new DoctorViewModel()
            {
               Doctors = _context.Doctors.ToList(),
              
               Professions=_context.Professions.ToList(),
               WorkSchedules=_context.WorkSchedules.ToList(),

                
            };

            return View(doctorViewModel);
        }
        
    }
}