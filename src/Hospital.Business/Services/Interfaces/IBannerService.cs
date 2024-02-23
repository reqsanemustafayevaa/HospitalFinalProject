using Hospital.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.Services.Interfaces
{
    public interface IBannerService
    {
        Task CreateAsync(Banner banner);
        Task UpdateAsync(Banner banner);
        Task Delete(int id);
        Task<List<Banner>> GetAllAsync();
        Task<Banner> GetByIdAsync(int id);
    }
}
