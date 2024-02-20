using Hospital.Core.Repositories.Interfaces;
using Hospital.Data.Repositories.Implementations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Data
{
    public static class RepositoryRegistration
    {
        public static void RepositoriesRegister(this IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddScoped<IDoctorRepository, DoctorRepository>();
            serviceDescriptors.AddScoped<IProfessionRepository, ProfessionRepository>();
            serviceDescriptors.AddScoped<IWorkScheduleRepository, WorkScheduleRepository>();
            serviceDescriptors.AddScoped<ISettingRepository, SettingRepository>();
            serviceDescriptors.AddScoped<ISliderRepository, SliderRepository>();


        }
    }
}
