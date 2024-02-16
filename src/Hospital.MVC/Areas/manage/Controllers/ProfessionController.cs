using Hospital.Business.CustomExceptions.CommonExceptions;
using Hospital.Business.CustomExceptions.ImageExceptions;
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

    }
}
