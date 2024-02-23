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
    public class AboutService : IAboutService
    {
        private readonly IAboutRepository _aboutRepository;
        private readonly IWebHostEnvironment _env;

        public AboutService(IAboutRepository aboutRepository,IWebHostEnvironment env)
        {
            _aboutRepository = aboutRepository;
           _env = env;
        }
        public async Task CreateAsync(About about)
        {
            if (about == null)
            {
                throw new EntityNotFoundException();
            }
            if (about.ImageFile is not null)
            {
                if (about.ImageFile.ContentType != "image/png" && about.ImageFile.ContentType != "image/jpeg")
                {
                    throw new InvalidImageContentException("ImageFile", "File must be .jpeg or .png!");
                }
                if (about.ImageFile.Length > 1072346)
                {
                    throw new InvalidImageSizeException("ImageFile", "File must be lower than 1mb!");
                }
                about.ImageUrl = about.ImageFile.SaveFile(_env.WebRootPath, "uploads/abouts");
            }
            else
            {
                throw new InvalidImageFileException("ImageFile", "ImageFile is required!");
            }
            about.IsDeleted = false;
            about.CreateDate = DateTime.UtcNow;
            about.UpdateDate = DateTime.UtcNow;
           
            await _aboutRepository.createAsync(about);
            await _aboutRepository.CommitAsync();
        }

        public async Task Delete(int id)
        {
            var existabout = await _aboutRepository.GetAsync(x => x.Id == id);
            if (existabout is null)
            {
                throw new EntityNotFoundException();
            }
            Helper.DeleteFile(_env.WebRootPath, "uploads/abouts", existabout.ImageUrl);
            _aboutRepository.Delete(existabout);
            await _aboutRepository.CommitAsync();
        }

        public async Task<List<About>> GetAllAsync()
        {
            return await _aboutRepository.GetAllAsync().ToListAsync();
        }

        public  Task<About> GetByIdAsync(int id)
        {
            var about = _aboutRepository.GetAsync(x => x.Id == id);
            if (about is null)
            {
                throw new EntityNotFoundException();
            }

            return about;
        }

        public async Task UpdateAsync(About about)
        {
            var existabout = await _aboutRepository.GetAsync(x => x.Id == about.Id);
            if (about is null)
            {
                throw new EntityNotFoundException();
            }
            if (about.ImageFile is not null)
            {
                if (about.ImageFile.ContentType != "image/png" && about.ImageFile.ContentType != "image/jpeg")
                {
                    throw new InvalidImageContentException("ImageFile", "File must be .jpeg or .png!");
                }
                if (about.ImageFile.Length > 1072346)
                {
                    throw new InvalidImageSizeException("ImageFile", "File must be lower than 1mb!");
                }
                Helper.DeleteFile(_env.WebRootPath, "uploads/abouts", existabout.ImageUrl);
                existabout.ImageUrl = about.ImageFile.SaveFile(_env.WebRootPath, "uploads/abouts");
            }
            else
            {
                throw new InvalidImageFileException("ImageFile", "ImageFile is required");
            }
            existabout.Title = about.Title;
            existabout.TitleSpan = about.TitleSpan;
            existabout.Description = about.Description;
            existabout.MainDescription = about.MainDescription;
            existabout.HappyPatients = about.HappyPatients;
            existabout.HealthSections = about.HealthSections;
            existabout.QualityDoctors = about.QualityDoctors;
            
            existabout.IsDeleted = false;
            existabout.UpdateDate = DateTime.UtcNow;
            await _aboutRepository.CommitAsync();
        }
    }
}
