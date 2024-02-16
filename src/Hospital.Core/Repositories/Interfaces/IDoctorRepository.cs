using Hospital.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Repositories.Interfaces
{
    public interface IDoctorRepository:IGenericReposiotry<Doctor>
    {
        Task<Doctor> GetDoctorByIdAsync(int id);
    }
}
