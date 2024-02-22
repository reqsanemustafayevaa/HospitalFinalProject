using Hospital.Business.CustomExceptions.CommonExceptions;
using Hospital.Business.CustomExceptions.ImageExceptions;
using Hospital.Business.Extentions;
using Hospital.Business.Services.Interfaces;
using Hospital.Core.Models;
using Hospital.Core.Repositories.Interfaces;
using Hospital.Data.Repositories.Implementations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.Services.Implementations
{
    public class TestimonialService : ITestimonialService
    {
        private readonly ITestimonialRepository _testimonialRepository;
        private readonly IWebHostEnvironment _env;

        public TestimonialService(ITestimonialRepository testimonialRepository,IWebHostEnvironment env)
        {
            _testimonialRepository = testimonialRepository;
           _env = env;
        }
        public async Task CreateAsync(Testimonial testimonial)
        {
            if (testimonial == null)
            {
                throw new EntityNotFoundException();
            }
            if (testimonial.ImageFile is not null)
            {
                if (testimonial.ImageFile.ContentType != "image/png" && testimonial.ImageFile.ContentType != "image/jpeg")
                {
                    throw new InvalidImageContentException("ImageFile", "File must be .jpeg or .png!");
                }
                if (testimonial.ImageFile.Length > 1072346)
                {
                    throw new InvalidImageSizeException("ImageFile", "File must be lower than 1mb!");
                }
                testimonial.ImageUrl = testimonial.ImageFile.SaveFile(_env.WebRootPath, "uploads/testimonials");
            }
            testimonial.IsDeleted = false;
            testimonial.CreateDate = DateTime.UtcNow;
            testimonial.UpdateDate = DateTime.UtcNow;

            await _testimonialRepository.createAsync(testimonial);
            await _testimonialRepository.CommitAsync();
        }

        public async Task Delete(int id)
        {
            var existtestimonial = await _testimonialRepository.GetAsync(x => x.Id == id);
            if (existtestimonial is null)
            {
                throw new EntityNotFoundException();
            }
            Helper.DeleteFile(_env.WebRootPath, "uploads/testimonials", existtestimonial.ImageUrl);
            _testimonialRepository.Delete(existtestimonial);
            await _testimonialRepository.CommitAsync();
        }

        public async Task<List<Testimonial>> GetAllAsync()
        {
            return await _testimonialRepository.GetAllAsync().ToListAsync();
        }

        public Task<Testimonial> GetByIdAsync(int id)
        {
            var testimonial = _testimonialRepository.GetAsync(x => x.Id == id);
            if (testimonial is null)
            {
                throw new EntityNotFoundException();
            }

            return testimonial; 
        }

      
    }
}
