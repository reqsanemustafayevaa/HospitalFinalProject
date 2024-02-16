using Hospital.Business.ViewModels;
using Hospital.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.Services.Interfaces
{
    public interface IDoctorService
    {
        Task CreateAsync(Doctor doctor);
        Task UpdateAsync(Doctor doctor);
        Task Delete(int id);
        Task<Doctor> GetByIdAsync(int id);
        Task<List<Doctor>> GetAllAsync();
        
    }
}
