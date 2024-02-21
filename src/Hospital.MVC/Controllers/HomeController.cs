
using Hospital.Business.Services.Interfaces;
using Hospital.Core.Models;
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
        private readonly IProfessionService _professionService;

        public HomeController(AppDbContext context,IProfessionService professionService)
        {
            _context = context;
            _professionService = professionService;
        }


        public IActionResult Index()
        {
            DoctorViewModel doctorViewModel = new DoctorViewModel()
            {
               Doctors = _context.Doctors.ToList(),
              
               Professions=_context.Professions.ToList(),
               WorkSchedules=_context.WorkSchedules.ToList(),
               Sliders = _context.Sliders.ToList(),

                
            };

            return View(doctorViewModel);
        }
      
    }
}