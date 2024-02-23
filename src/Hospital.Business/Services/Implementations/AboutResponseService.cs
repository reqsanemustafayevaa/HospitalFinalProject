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
    public class AboutResponseService : IAboutResponseService
    {
        private readonly IAboutResponseRepository _aboutResponseRepository;
        private readonly IWebHostEnvironment _env;

        public AboutResponseService(IAboutResponseRepository aboutResponseRepository,IWebHostEnvironment env)
        {
            _aboutResponseRepository = aboutResponseRepository;
          _env = env;
        }
        public async Task CreateAsync(AboutResponse aboutResponse)
        {
            if (aboutResponse == null)
            {
                throw new EntityNotFoundException();
            }
            if (aboutResponse.ImageFile is not null)
            {
                if (aboutResponse.ImageFile.ContentType != "image/png" && aboutResponse.ImageFile.ContentType != "image/jpeg")
                {
                    throw new InvalidImageContentException("ImageFile", "File must be .jpeg or .png!");
                }
                if (aboutResponse.ImageFile.Length > 1072346)
                {
                    throw new InvalidImageSizeException("ImageFile", "File must be lower than 1mb!");
                }
                aboutResponse.ImageUrl = aboutResponse.ImageFile.SaveFile(_env.WebRootPath, "uploads/aboutresponse");
            }
            else
            {
                throw new InvalidImageFileException("ImageFile", "ImageFile is required!");
            }
            aboutResponse.IsDeleted = false;
            aboutResponse.CreateDate = DateTime.UtcNow;
            aboutResponse.UpdateDate = DateTime.UtcNow;

            await _aboutResponseRepository.createAsync(aboutResponse);
            await _aboutResponseRepository.CommitAsync();
        }

        public async Task Delete(int id)
        {
            var existabout = await _aboutResponseRepository.GetAsync(x => x.Id == id);
            if (existabout is null)
            {
                throw new EntityNotFoundException();
            }
            Helper.DeleteFile(_env.WebRootPath, "uploads/aboutresponse", existabout.ImageUrl);
            _aboutResponseRepository.Delete(existabout);
            await _aboutResponseRepository.CommitAsync();
        }

        public async Task<List<AboutResponse>> GetAllAsync()
        {
            return await _aboutResponseRepository.GetAllAsync().ToListAsync();
        }

        public  Task<AboutResponse> GetByIdAsync(int id)
        {
            var about = _aboutResponseRepository.GetAsync(x => x.Id == id);
            if (about is null)
            {
                throw new EntityNotFoundException();
            }

            return about;
        }

        public async Task UpdateAsync(AboutResponse aboutResponse)
        {
            var existabout = await _aboutResponseRepository.GetAsync(x => x.Id == aboutResponse.Id);
            if (aboutResponse is null)
            {
                throw new EntityNotFoundException();
            }
            if (aboutResponse.ImageFile is not null)
            {
                if (aboutResponse.ImageFile.ContentType != "image/png" && aboutResponse.ImageFile.ContentType != "image/jpeg")
                {
                    throw new InvalidImageContentException("ImageFile", "File must be .jpeg or .png!");
                }
                if (aboutResponse.ImageFile.Length > 1072346)
                {
                    throw new InvalidImageSizeException("ImageFile", "File must be lower than 1mb!");
                }
                Helper.DeleteFile(_env.WebRootPath, "uploads/abouts", existabout.ImageUrl);
                existabout.ImageUrl = aboutResponse.ImageFile.SaveFile(_env.WebRootPath, "uploads/aboutresponse");
            }
            else
            {
                throw new InvalidImageFileException("ImageFile", "ImageFile is required!");
            }
            existabout.Response1 = aboutResponse.Response1;
            existabout.Response2 = aboutResponse.Response2;
            existabout.Response3 = aboutResponse.Response3;
            existabout.Description1 = aboutResponse.Description1;
            existabout.Description2 = aboutResponse.Description2;
            existabout.Description3 = aboutResponse.Description3;
           
            existabout.IsDeleted = false;
            existabout.UpdateDate = DateTime.UtcNow;
            await _aboutResponseRepository.CommitAsync();
        }
    }
}
