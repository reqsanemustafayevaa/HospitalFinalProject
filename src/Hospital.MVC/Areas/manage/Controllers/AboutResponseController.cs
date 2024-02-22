using Hospital.Business.CustomExceptions.CommonExceptions;
using Hospital.Business.CustomExceptions.ImageExceptions;
using Hospital.Business.Services.Interfaces;
using Hospital.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.MVC.Areas.manage.Controllers
{
    [Area("manage")]
    public class AboutResponseController : Controller
    {
        private readonly IAboutResponseService _aboutResponseService;

        public AboutResponseController(IAboutResponseService aboutResponseService)
        {
           _aboutResponseService = aboutResponseService;
        }
        public async Task<IActionResult> Index()
        {
            var abouts = await _aboutResponseService.GetAllAsync();
            return View(abouts);
          
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AboutResponse aboutResponse)
        {
            if (!ModelState.IsValid)
            {
                return View(aboutResponse);
            }
            try
            {
                await _aboutResponseService.CreateAsync(aboutResponse);
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
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Update(int id)
        {
            var existaboutresponse = await _aboutResponseService.GetByIdAsync(id);
            if (existaboutresponse == null)
            {
                return View("error");

            }
            return View(existaboutresponse);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(AboutResponse aboutResponse)
        {
            if (!ModelState.IsValid)
            {
                return View(aboutResponse);
            }
            try
            {
                await _aboutResponseService.UpdateAsync(aboutResponse);
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
            return RedirectToAction("Index");





        }
    }
}
