using Hospital.Business.ViewModels;
using Hospital.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task Create(AppointmentViewModel appointmentViewModel);
        Task<bool> IsAppointmentValid(AppointmentViewModel appointmentViewModel);
    }
}
