using Hospital.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.Services.Interfaces
{
    public interface IFeatureService
    {
        Task CreateAsync(Feature feature);
        Task UpdateAsync(Feature feature);
        Task Delete(int id);
        Task<List<Feature>> GetAllAsync();
        Task<Feature> GetByIdAsync(int id);
    }
}
