using Hospital.Business.CustomExceptions.CommonExceptions;
using Hospital.Business.Services.Interfaces;
using Hospital.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.MVC.Areas.manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "SuperAdmin")]
    public class SettingController : Controller
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public async Task<IActionResult> Index()
        {
            var settings = await _settingService.GetAllAsync();
            return View(settings);
           
        }
        public async Task<IActionResult> Update(int id)
        {
            if (id == null) return View("error");
            var existSetting = await _settingService.GetByIdAsync(x => x.Id == id);
            if (existSetting == null) return View("error");
            return View(existSetting);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Setting setting)
        {
            if (!ModelState.IsValid)
            {
                return View(setting);
            }
            try
            {
                await _settingService.UpdateAsync(setting);
            }
            catch (EntityNotFoundException)
            {
                return View("error");
            }

            return RedirectToAction("Index");
        }
    }
}
