using Hospital.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.Services.Interfaces
{
    public interface IAboutService
    {
        Task CreateAsync(About about);
        Task UpdateAsync(About about);
        Task Delete(int id);
        Task<List<About>> GetAllAsync();
        Task<About> GetByIdAsync(int id);
    }
}
