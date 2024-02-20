using Hospital.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.Services.Interfaces
{
    public interface ISliderService
    {
        Task CreateAsync(Slider slider);
        Task UpdateAsync(Slider slider);
        Task Delete(int id);
        Task<List<Slider>> GetAllAsync();
        Task<Slider> GetByIdAsync(int id);
    }
}
