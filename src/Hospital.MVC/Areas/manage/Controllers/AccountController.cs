using Hospital.Business.Services.Interfaces;
using Hospital.Business.ViewModels;
using Hospital.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;

namespace Hospital.MVC.Areas.manage.Controllers
{
    [Area("manage")]
    public class AccountController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAccountService _accountService;
        public AccountController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, IAccountService accountService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _accountService = accountService;
        }
        //public async Task<IActionResult> CreateRole()
        //{
        //    var role1 = new IdentityRole("SuperAdmin");
        //    var role2 = new IdentityRole("Admin");
        //    var role3 = new IdentityRole("user");
        //    var role4 = new IdentityRole("Doctor");
        //    await _roleManager.CreateAsync(role1);
        //    await _roleManager.CreateAsync(role2);
        //    await _roleManager.CreateAsync(role3);
        //    await _roleManager.CreateAsync(role4);
        //    return Ok();
        //}

        //public async Task<IActionResult> CreateAdmin()
        //{
        //    var admin = new AppUser
        //    {
        //        FullName = "Raghsana Mustafayeva",
        //        UserName = "SuperAdmin"
        //    };
        //    await _userManager.CreateAsync(admin, "Admin200@");
        //    await _userManager.AddToRoleAsync(admin, "SuperAdmin");
        //    return Ok();
        //}
        public IActionResult Login()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);
            try
            {
                await _accountService.Login(loginViewModel);
            }
            catch (InvalidCredentialException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(loginViewModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(loginViewModel);
            }
            return RedirectToAction("Index", "doctor");
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _accountService.Logout();

            return RedirectToAction("login", "account");
        }


    }
}
