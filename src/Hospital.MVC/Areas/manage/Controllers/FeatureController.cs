using Hospital.Business.CustomExceptions.CommonExceptions;
using Hospital.Business.CustomExceptions.ImageExceptions;
using Hospital.Business.Services.Implementations;
using Hospital.Business.Services.Interfaces;
using Hospital.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.MVC.Areas.manage.Controllers
{
    [Area("manage")]
    public class FeatureController : Controller
    {
        private readonly IFeatureService _featureService;

        public FeatureController(IFeatureService featureService)
        {
           _featureService = featureService;
        }
        public async Task<IActionResult> Index()
        {
            var features = await _featureService.GetAllAsync();
            return View(features);
           
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Feature feature)
        {
            if (!ModelState.IsValid)
            {
                return View(feature);
            }
            try
            {
                await _featureService.CreateAsync(feature);
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
            var existfeature = await _featureService.GetByIdAsync(id);
            if (existfeature == null)
            {
                return View("error");

            }
            return View(existfeature);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Feature feature)
        {
            if (!ModelState.IsValid)
            {
                return View(feature);
            }
            try
            {
                await _featureService.UpdateAsync(feature);
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
        public async Task<IActionResult> Delete(int id)
        {
            var existslider = await _featureService.GetByIdAsync(id);
            if (existslider == null)
            {
                return View("error");

            }
            return View(existslider);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Slider slider)
        {
            try
            {
                await _featureService.Delete(slider.Id);
            }
            catch (EntityNotFoundException)
            {
                return View("error");
            }

            return RedirectToAction("index");
        }
    }
}
