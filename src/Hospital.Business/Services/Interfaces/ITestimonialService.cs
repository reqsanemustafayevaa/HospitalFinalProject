using Hospital.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.Services.Interfaces
{
    public interface ITestimonialService
    {
        Task CreateAsync(Testimonial testimonial);
        
        Task Delete(int id);
        Task<List<Testimonial>> GetAllAsync();
        Task<Testimonial> GetByIdAsync(int id);
    }
}
