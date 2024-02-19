using Hospital.Business.Services.Interfaces;
using Hospital.Core.Models;
using Hospital.Data.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.MVC.Controllers
{
    public class DoctorDetailController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly AppDbContext _context;

        public DoctorDetailController(IDoctorService doctorService,AppDbContext context)
        {
           _doctorService = doctorService;
          _context = context;
        }
        public async Task<IActionResult> Detail(int id)
        {
            ViewBag.Professions = _context.Professions.ToList();
            Doctor doctor = await _doctorService.GetByIdAsync(id);
            return View(doctor);
           
        }
    }
}
