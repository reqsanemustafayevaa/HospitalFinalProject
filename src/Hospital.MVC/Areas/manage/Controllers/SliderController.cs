using Hospital.Business.CustomExceptions.CommonExceptions;
using Hospital.Business.CustomExceptions.ImageExceptions;
using Hospital.Business.Services.Interfaces;
using Hospital.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.MVC.Areas.manage.Controllers
{
    [Area("manage")]
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;

        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }
        public async Task<IActionResult> Index()
        {
            var slider=await _sliderService.GetAllAsync();
            return View(slider);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (!ModelState.IsValid)
            {
                return View(slider);
            }
            try
            {
                await _sliderService.CreateAsync(slider);
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
            var existfeature = await _sliderService.GetByIdAsync(id);
            if (existfeature == null)
            {
                return View("error");

            }
            return View(existfeature);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Slider slider)
        {
            if (!ModelState.IsValid)
            {
                return View(slider);
            }
            try
            {
                await _sliderService.UpdateAsync(slider);
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
            var existslider = await _sliderService.GetByIdAsync(id);
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
                await _sliderService.Delete(slider.Id);
            }
            catch (EntityNotFoundException)
            {
                return View("error");
            }

            return RedirectToAction("index");
        }
    }
}
