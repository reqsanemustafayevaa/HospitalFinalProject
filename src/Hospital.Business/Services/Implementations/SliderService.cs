using Hospital.Business.CustomExceptions.CommonExceptions;
using Hospital.Business.CustomExceptions.ImageExceptions;
using Hospital.Business.Extentions;
using Hospital.Business.Services.Interfaces;
using Hospital.Core.Models;
using Hospital.Core.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.Services.Implementations
{

    public class SliderService : ISliderService
    {
        private readonly ISliderRepository _sliderRepository;
        private readonly IWebHostEnvironment _env;

        public SliderService(ISliderRepository sliderRepository,IWebHostEnvironment env)
        {
            _sliderRepository = sliderRepository;
           _env = env;
        }
        public async Task CreateAsync(Slider slider)
        {
            if (slider == null)
            {
                throw new EntityNotFoundException();
            }
            if (slider.ImageFile is not null)
            {
                if (slider.ImageFile.ContentType != "image/png" && slider.ImageFile.ContentType != "image/jpeg")
                {
                    throw new InvalidImageContentException("ImageFile", "File must be .jpeg or .png!");
                }
                if (slider.ImageFile.Length > 1072346)
                {
                    throw new InvalidImageSizeException("ImageFile", "File must be lower than 1mb!");
                }
                slider.ImageUrl = slider.ImageFile.SaveFile(_env.WebRootPath, "uploads/sliders");
            }
            else
            {
                throw new InvalidImageFileException("ImageFile", "ImageFile is required");
            }
            slider.IsDeleted = false;
            slider.CreateDate = DateTime.UtcNow;
            slider.UpdateDate = DateTime.UtcNow;
            await _sliderRepository.createAsync(slider);
            await _sliderRepository.CommitAsync();
        }

        public async Task Delete(int id)
        {
            var existslider = await _sliderRepository.GetAsync(x => x.Id == id);
            if (existslider is null)
            {
                throw new EntityNotFoundException();
            }
            Helper.DeleteFile(_env.WebRootPath, "uploads/sliders", existslider.ImageUrl);
            _sliderRepository.Delete(existslider);
            await _sliderRepository.CommitAsync();
        }

        public async Task<List<Slider>> GetAllAsync()
        {
            return await _sliderRepository.GetAllAsync().ToListAsync();
        }

        public Task<Slider> GetByIdAsync(int id)
        {
            var slider = _sliderRepository.GetAsync(x => x.Id == id);
            if (slider is null)
            {
                throw new EntityNotFoundException();
            }

            return slider;
        }

        public async Task UpdateAsync(Slider slider)
        {
            var existslider = await _sliderRepository.GetAsync(x => x.Id == slider.Id);
            if (slider is null)
            {
                throw new EntityNotFoundException();
            }
            if (slider.ImageFile is not null)
            {
                if (slider.ImageFile.ContentType != "image/png" && slider.ImageFile.ContentType != "image/jpeg")
                {
                    throw new InvalidImageContentException("ImageFile", "File must be .jpeg or .png!");
                }
                if (slider.ImageFile.Length > 1072346)
                {
                    throw new InvalidImageSizeException("ImageFile", "File must be lower than 1mb!");
                }
                Helper.DeleteFile(_env.WebRootPath, "uploads/sliders", existslider.ImageUrl);
                existslider.ImageUrl = slider.ImageFile.SaveFile(_env.WebRootPath, "uploads/sliders");
            }
            else
            {
                throw new InvalidImageFileException("ImageFile", "ImageFile is required");
            }
            existslider.Title = slider.Title;
            existslider.Description = slider.Description;
            existslider.IsDeleted = false;
            existslider.UpdateDate = DateTime.UtcNow;
            await _sliderRepository.CommitAsync();
        }
    }
}
