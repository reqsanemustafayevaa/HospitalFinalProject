using Hospital.Core.Models;
using Hospital.Core.Repositories.Interfaces;
using Hospital.Data.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Data.Repositories.Implementations
{
    public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
    {
        private readonly AppDbContext _context;

        public DoctorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Doctor> GetDoctorByIdAsync(int id)
        {
            var doctor = await _context.Doctors
        .Include(d => d.WorkSchedules)
        .FirstOrDefaultAsync(d => d.Id == id);

            if (doctor == null)
            {
                throw new NullReferenceException();
            }

            // Kopya bir iş programı listesi oluştur
            doctor.WorkSchedules = new List<WorkSchedule>(doctor.WorkSchedules);

            return doctor;

        }
    }
}
