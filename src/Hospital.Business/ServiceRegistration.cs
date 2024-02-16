using Hospital.Business.Services.Implementations;
using Hospital.Business.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business
{
    public static class ServiceRegistration
    {
        public static void ServicesRegister(this IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddScoped<IDoctorService, DoctorService>();
            serviceDescriptors.AddScoped<IProfessionService, ProfessionService>();
            serviceDescriptors.AddScoped<IAccountService, AccountService>();
            serviceDescriptors.AddScoped<IAppointmentService, AppointmentService>();



        }
    }
}
