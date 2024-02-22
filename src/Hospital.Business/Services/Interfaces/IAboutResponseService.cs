using Hospital.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.Services.Interfaces
{
    public interface IAboutResponseService
    {
        Task CreateAsync(AboutResponse aboutResponse);
        Task UpdateAsync(AboutResponse aboutResponse);
        Task Delete(int id);
        Task<List<AboutResponse>> GetAllAsync();
        Task<AboutResponse> GetByIdAsync(int id);
    }
}
