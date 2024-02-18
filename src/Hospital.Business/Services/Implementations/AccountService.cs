using Hospital.Business.CustomExceptions.AuthExceptions;
using Hospital.Business.CustomExceptions.CommonExceptions;
using Hospital.Business.CustomExceptions.RegisterExceptions;
using Hospital.Business.Services.Interfaces;
using Hospital.Business.ViewModels;
using Hospital.Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

       

       
        public async Task Login(LoginViewModel loginViewModel)
        {
            if (loginViewModel == null) throw new EntityNotFoundException();
            AppUser admin = null;
            admin = await _userManager.FindByNameAsync(loginViewModel.UserName);
            if (admin == null)
            {
                throw new InvalidCredsException("", "UserName or Password is incorrect");

            }
            var result = await _signInManager.PasswordSignInAsync(admin, loginViewModel.Password, false, false);
            if (!result.Succeeded)
            {
                throw new InvalidCredsException("", "UserName or Password is incorrect");
            }

        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task Register(RegisterViewModel registerViewModel)
        {
            AppUser user = null;

            user = await _userManager.FindByNameAsync(registerViewModel.UserName);
            if (user is not null)
            {
                throw new InvalidRegisterOperation("Username", "Username already exist!");

            }
            user = await _userManager.FindByEmailAsync(registerViewModel.Email);

            if (user is not null)
            {
                throw new InvalidRegisterOperation("Email", "Email already exist!");

            }
            AppUser appUser = new AppUser
            {
                FullName = registerViewModel.Fullname,
                UserName = registerViewModel.UserName,
               
              
                Email = registerViewModel.Email,
               
               
            };
            var result = await _userManager.CreateAsync(appUser, registerViewModel.Password);

            if (result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    throw new InvalidRegisterOperation("", error.Description);

                }
            }
            await _userManager.AddToRoleAsync(appUser, "user");
            await _signInManager.SignInAsync(appUser, isPersistent: false);

        }
    }
}
