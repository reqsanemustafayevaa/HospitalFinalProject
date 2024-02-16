using Hospital.Core.Models;
using Hospital.Core.Repositories.Interfaces;
using Hospital.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Data.Repositories.Implementations
{
    public class ProfessionRepository : GenericRepository<Profession>, IProfessionRepository
    {
        public ProfessionRepository(AppDbContext context) : base(context)
        {
        }
    }
}
