using Hospital.Business.CustomExceptions.AuthExceptions;
using Hospital.Business.CustomExceptions.RegisterExceptions;
using Hospital.Business.Services.Interfaces;
using Hospital.Business.ViewModels;
using Hospital.Core.Models;
using Hospital.Data.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Authentication;

namespace Hospital.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;

        public AccountController(IAccountService accountService
                                ,UserManager<AppUser>userManager
                                 ,AppDbContext context)
        {
            _accountService = accountService;
            _userManager = userManager;
           _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
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
			return RedirectToAction("create", "appointment");
		}
		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			await _accountService.Logout();

			return RedirectToAction("login", "account");
		}
		public IActionResult Register()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);
            try
            {
                await _accountService.Register(registerViewModel);
            }
            catch (InvalidRegisterOperation ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(registerViewModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(registerViewModel);
            }
            return RedirectToAction("index", "home");
        }

        [Authorize(Roles ="user,SuperAdmin")]
        public async Task<IActionResult> Profile()
        {
            ViewBag.Doctors=_context.Doctors.ToList();
            AppUser appUser = null;

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                appUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            }
          
                List<Appointment> appointments = await _context.Appointments.Where(x => x.AppUserId == appUser.Id).ToListAsync();

                return View(appointments);
        
           
           
            
        }
        


    }
}
