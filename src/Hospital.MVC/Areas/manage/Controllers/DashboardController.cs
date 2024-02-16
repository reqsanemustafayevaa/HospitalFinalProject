using Hospital.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.MVC.Areas.manage.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    [Area("manage")]
    
    public class DashboardController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public DashboardController(UserManager<AppUser>userManager
                                  ,RoleManager<IdentityRole>roleManager
                                  ,SignInManager<AppUser>signInManager)
        {
           _userManager = userManager;
           _roleManager = roleManager;
           _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
      
       

    }
}
