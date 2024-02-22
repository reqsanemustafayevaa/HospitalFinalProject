using Hospital.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.Services.Interfaces
{
    public interface IServiceService
    {
        Task CreateAsync(Service service);
        Task UpdateAsync(Service service);
        Task Delete(int id);
        Task<List<Service>> GetAllAsync();
        Task<Service> GetByIdAsync(int id);
    }
}
