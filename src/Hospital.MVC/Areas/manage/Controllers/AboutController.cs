using Hospital.Business.Services.Implementations;
using Hospital.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.MVC.Areas.manage.Controllers
{
    [Area("manage")]
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
           _aboutService = aboutService;
        }
        public async Task< IActionResult> Index()
        {
            var slider = await _aboutService.GetAllAsync();
            return View(slider);
           
        }

    }
}
