using Hospital.Business.CustomExceptions.CommonExceptions;
using Hospital.Business.CustomExceptions.ImageExceptions;
using Hospital.Business.Services.Implementations;
using Hospital.Business.Services.Interfaces;
using Hospital.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.MVC.Controllers
{
    public class TestimonialController : Controller
    {
        private readonly ITestimonialService _testimonialService;
        private readonly UserManager<AppUser> _userManager;

        public TestimonialController(ITestimonialService testimonialService,UserManager<AppUser>userManager)
        {
            _testimonialService = testimonialService;
           _userManager = userManager;
        }
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Index()
        {
           
            var testimonials = await _testimonialService.GetAllAsync();
            return View(testimonials);
           
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Testimonial testimonial)
        {
            if (!ModelState.IsValid)
            {
                return View(testimonial);
            }
            try
            {
                await _testimonialService.CreateAsync(testimonial);
            }
            catch (EntityNotFoundException)
            {
                return View("error");
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
            return RedirectToAction("Index", "Home");

        }
        public async Task<IActionResult> Delete(int id)
        {
            var existcomment = await _testimonialService.GetByIdAsync(id);
            if (existcomment     == null)
            {
                return View("error");

            }
            return View(existcomment);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Testimonial testimonial)
        {
            try
            {
                await _testimonialService.Delete(testimonial.Id);
            }
            catch (EntityNotFoundException)
            {
                return View("error");
            }

            return RedirectToAction("index");
        }
    }
}
