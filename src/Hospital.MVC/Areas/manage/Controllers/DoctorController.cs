using Hospital.Business.CustomExceptions.CommonExceptions;
using Hospital.Business.CustomExceptions.ImageExceptions;
using Hospital.Business.CustomExceptions.ProfessionExceptions;
using Hospital.Business.Services.Interfaces;
using Hospital.Core.Models;
using Hospital.Core.Repositories.Interfaces;
using Hospital.Data.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.MVC.Areas.manage.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    [Area("manage")]
   
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly AppDbContext _context;
        private readonly IDoctorRepository _doctorRepository;
        private readonly UserManager<AppUser> _userManager;

        public DoctorController(IDoctorService doctorService,
                                AppDbContext context
                                ,IDoctorRepository doctorRepository
                                ,UserManager<AppUser>userManager)
        {
            _doctorService = doctorService;
           _context = context;
            _doctorRepository = doctorRepository;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Professions = _context.Professions.ToList();
            var existdoctor=await _doctorService.GetAllAsync();

            return View(existdoctor);
        }
        public async  Task<IActionResult> Create()
        {

            ViewBag.Professions=_context.Professions.ToList();

           

            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Doctor doctor)
        {
           
           
            if (doctor == null)
            {
                return View("error");
            }
            ViewBag.Professions = _context.Professions.ToList();
            if (!ModelState.IsValid)
            {
               return View();
            }
            try
            {
                await _doctorService.CreateAsync(doctor);
            }
            catch (InvalidImageContentException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch(ProfessionNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (InvalidImageSizeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (EntityNotFoundException ex)
            {
               
                return View("error");
            }
            catch (ImageRequiredException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            return RedirectToAction("Index");
        }
        public  async Task<IActionResult> Update(int id)
        {
            ViewBag.Professions = _context.Professions.ToList();
           
            var existdostor =await _doctorRepository.GetDoctorByIdAsync(id);
           
           
            
            if(existdostor == null)
            {
                return View("error");
            }
            return View(existdostor);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Doctor doctor)
        {
            ViewBag.Professions = _context.Professions.ToList();
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                await _doctorService.UpdateAsync (doctor);
            }
            catch (InvalidImageContentException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (InvalidImageSizeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);    
                return View();
            }
            catch (EntityNotFoundException ex)
            {
               return View("error");
            }
            catch (ImageRequiredException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            return RedirectToAction("index");


        }
    }
}
