using Hospital.Business.CustomExceptions.CommonExceptions;
using Hospital.Business.CustomExceptions.ImageExceptions;
using Hospital.Business.Services.Implementations;
using Hospital.Business.Services.Interfaces;
using Hospital.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.MVC.Areas.manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles ="SuperAdmin")]
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
           _aboutService = aboutService;
        }
        public async Task< IActionResult> Index()
        {
            var abouts = await _aboutService.GetAllAsync();
            return View(abouts);
           
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(About about)
        {
            if (!ModelState.IsValid)
            {
                return View(about);
            }
            try
            {
                await _aboutService.CreateAsync(about);
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
            catch (InvalidImageFileException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Update(int id)
        {
            var existfeature = await _aboutService.GetByIdAsync(id);
            if (existfeature == null)
            {
                return View("error");

            }
            return View(existfeature);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(About about)
        {
            if (!ModelState.IsValid)
            {
                return View(about);
            }
            try
            {
                await _aboutService.UpdateAsync(about);
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
            catch(InvalidImageFileException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            return RedirectToAction("Index");





        }
        public async Task<IActionResult> Delete(int id)
        {
            var existabout = await _aboutService.GetByIdAsync(id);
            if (existabout == null)
            {
                return View("error");

            }
            return View(existabout);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(About about)
        {
            try
            {
                await _aboutService.Delete(about.Id);
            }
            catch (EntityNotFoundException)
            {
                return View("error");
            }

            return RedirectToAction("index");
        }


    }
}
