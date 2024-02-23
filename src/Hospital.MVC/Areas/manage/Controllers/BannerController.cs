using Hospital.Business.CustomExceptions.CommonExceptions;
using Hospital.Business.CustomExceptions.ImageExceptions;
using Hospital.Business.Services.Implementations;
using Hospital.Business.Services.Interfaces;
using Hospital.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.MVC.Areas.manage.Controllers
{
    [Area("manage")]
    public class BannerController : Controller
    {
        private readonly IBannerService _bannerService;

        public BannerController(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }
        public async Task<IActionResult> Index()
        {
            var features = await _bannerService.GetAllAsync();
            return View(features);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Banner banner)
        {
            if (!ModelState.IsValid)
            {
                return View(banner);
            }
            try
            {
                await _bannerService.CreateAsync(banner);
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
            var existbanner = await _bannerService.GetByIdAsync(id);
            if (existbanner == null)
            {
                return View("error");

            }
            return View(existbanner);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Banner banner)
        {
            if (!ModelState.IsValid)
            {
                return View(banner);
            }
            try
            {
                await _bannerService.UpdateAsync(banner);
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

        public async Task<IActionResult> Delete(int id)
        {
            var existbanner = await _bannerService.GetByIdAsync(id);
            if (existbanner == null)
            {
                return View("error");

            }
            return View(existbanner);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Banner banner)
        {
            try
            {
                await _bannerService.Delete(banner.Id);
            }
            catch (EntityNotFoundException)
            {
                return View("error");
            }

            return RedirectToAction("index");
        }
    }
}
