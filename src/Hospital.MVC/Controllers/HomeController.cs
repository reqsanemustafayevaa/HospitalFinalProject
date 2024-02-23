
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
               abouts = _context.Abouts.ToList(),
               Features = _context.Features.ToList(),
               Services = _context.Services.ToList(),
               Testimonials= _context.Testimonials.ToList(),
               Galleries= _context.Galleries.ToList(),
               Banners= _context.Banners.ToList(),
              

                
            };

            return View(doctorViewModel);
        }
        public IActionResult About()
        {
            AboutViewModel aboutViewModel = new AboutViewModel()
            {
                abouts = _context.Abouts.ToList(),
                doctors = _context.Doctors.ToList(),
                professions= _context.Professions.ToList(),
                workschedules= _context.WorkSchedules.ToList(),
                aboutResponses=_context.AboutsResponse.ToList(),
                
            };
            return View(aboutViewModel);
        }
      
    }
}