using Hospital.Business.CustomExceptions.CommonExceptions;
using Hospital.Business.CustomExceptions.ImageExceptions;
using Hospital.Business.CustomExceptions.ProfessionExceptions;
using Hospital.Business.Services.Interfaces;
using Hospital.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.MVC.Areas.manage.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    [Area("manage")]
   
    public class ProfessionController : Controller
    {
        private readonly IProfessionService _professionService;

        public ProfessionController(IProfessionService professionService)
        {
            _professionService = professionService;
        }
        public async Task<IActionResult> Index()
        {
            var profession=await _professionService.GetAllAsync();
            return View(profession);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Profession profession)
        {
            if (!ModelState.IsValid)
            {
                return View(profession);
            }
            try
            {
                await _professionService.CreateAsync(profession);
            }
            catch(EntityNotFoundException ex)
            {
                return View("error");
            }
            catch(InvalidImageContentException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (InvalidImageSizeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (AllreadyExistException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }

            return RedirectToAction("index");

        }
        public async Task<IActionResult> Update(int id)
        {
            var existprofession=await _professionService.GetByIdAsync(id);
            if (existprofession == null)
            {
                return View("error");
            }
            return View(existprofession);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(Profession profession)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                await _professionService.UpdateAsync(profession);
            }
            catch (ProfessionNotFoundException ex)
            {
                return View("error");
            }
            catch (ProfessionAllreadyExistException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message); return View();
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction("index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _professionService.Delete(id);
            }
            catch (ProfessionNotFoundException ex)
            {
                return View("error");
            }
            catch (Exception)
            {
                throw;
            }
            return Ok();
        }
    }
}
