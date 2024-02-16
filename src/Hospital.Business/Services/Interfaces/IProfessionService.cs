using Hospital.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.Services.Interfaces
{
    public interface IProfessionService
    {
        Task CreateAsync(Profession profession);
        Task UpdateAsync(Profession profession);
        Task Delete(int id);
        Task<Profession> GetByIdAsync(int id);
        Task<List<Profession>> GetAllAsync();
    }
}
