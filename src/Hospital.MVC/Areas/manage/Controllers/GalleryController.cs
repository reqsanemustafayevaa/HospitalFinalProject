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
    [Authorize(Roles = "SuperAdmin")]
    public class GalleryController : Controller
    {
        private readonly IGalleryService _galleryService;

        public GalleryController(IGalleryService galleryService)
        {
            _galleryService = galleryService;
        }
        public async Task<IActionResult> Index()
        {
            var slider = await _galleryService.GetAllAsync();
            return View(slider);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Gallery gallery)
        {
            if (!ModelState.IsValid)
            {
                return View(gallery);
            }
            try
            {
                await _galleryService.CreateAsync(gallery);
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
            var existgallery = await _galleryService.GetByIdAsync(id);
            if (existgallery == null)
            {
                return View("error");

            }
            return View(existgallery);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Gallery gallery)
        {
            if (!ModelState.IsValid)
            {
                return View(gallery);
            }
            try
            {
                await _galleryService.UpdateAsync(gallery);
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
            var existgallery = await _galleryService.GetByIdAsync(id);
            if (existgallery == null)
            {
                return View("error");

            }
            return View(existgallery);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Gallery gallery)
        {
            try
            {
                await _galleryService.Delete(gallery.Id);
            }
            catch (EntityNotFoundException)
            {
                return View("error");
            }

            return RedirectToAction("index");
        }
    }
}
