using Hospital.Business.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.Services.Interfaces
{
    public interface IAccountService
    {
        Task Login(LoginViewModel loginViewModel);
        Task Logout();
        Task Register(RegisterViewModel registerViewModel);
    }
}
