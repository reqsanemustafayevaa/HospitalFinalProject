using Hospital.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.Services.Interfaces
{
    public interface IGalleryService
    {
        Task CreateAsync(Gallery gallery);
        Task UpdateAsync(Gallery gallery);
        Task Delete(int id);
        Task<List<Gallery>> GetAllAsync();
        Task<Gallery> GetByIdAsync(int id);
    }
}
