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
    public class BannerService : IBannerService
    {
        private readonly IBannerRepository _bannerRepository;
        private readonly IWebHostEnvironment _env;

        public BannerService(IBannerRepository bannerRepository,IWebHostEnvironment env)
        {
            _bannerRepository = bannerRepository;
           _env = env;
        }
        public async Task CreateAsync(Banner banner)
        {
            if (banner == null)
            {
                throw new EntityNotFoundException();
            }
            if (banner.ImageFile is not null)
            {
                if (banner.ImageFile.ContentType != "image/png" && banner.ImageFile.ContentType != "image/jpeg")
                {
                    throw new InvalidImageContentException("ImageFile", "File must be .jpeg or .png!");
                }
                if (banner.ImageFile.Length > 1072346)
                {
                    throw new InvalidImageSizeException("ImageFile", "File must be lower than 1mb!");
                }
                banner.ImageUrl = banner.ImageFile.SaveFile(_env.WebRootPath, "uploads/banners");
            }
            banner.IsDeleted = false;
            banner.CreateDate = DateTime.UtcNow;
            banner.UpdateDate = DateTime.UtcNow;

            await _bannerRepository.createAsync(banner);
            await _bannerRepository.CommitAsync();
        }

        public async Task Delete(int id)
        {
            var existbanner= await _bannerRepository.GetAsync(x => x.Id == id);
            if (existbanner is null)
            {
                throw new EntityNotFoundException();
            }
            Helper.DeleteFile(_env.WebRootPath, "uploads/banners", existbanner.ImageUrl);
            _bannerRepository.Delete(existbanner);
            await _bannerRepository.CommitAsync();
        }

        public async Task<List<Banner>> GetAllAsync()
        {
            return await _bannerRepository.GetAllAsync().ToListAsync();
        }

        public Task<Banner> GetByIdAsync(int id)
        {
            var banner = _bannerRepository.GetAsync(x => x.Id == id);
            if (banner is null)
            {
                throw new EntityNotFoundException();
            }

            return banner;
        }

        public async Task UpdateAsync(Banner banner)
        {
            var existbanner = await _bannerRepository.GetAsync(x => x.Id == banner.Id);
            if (banner is null)
            {
                throw new EntityNotFoundException();
            }
            if (banner.ImageFile is not null)
            {
                if (banner.ImageFile.ContentType != "image/png" && banner.ImageFile.ContentType != "image/jpeg")
                {
                    throw new InvalidImageContentException("ImageFile", "File must be .jpeg or .png!");
                }
                if (banner.ImageFile.Length > 1072346)
                {
                    throw new InvalidImageSizeException("ImageFile", "File must be lower than 1mb!");
                }
                Helper.DeleteFile(_env.WebRootPath, "uploads/banners", existbanner.ImageUrl);
                existbanner.ImageUrl = banner.ImageFile.SaveFile(_env.WebRootPath, "uploads/banners");
            }
            existbanner.Title = banner.Title;
            existbanner.Description = banner.Description;
            existbanner.Phone = banner.Phone;
           
            existbanner.IsDeleted = false;
            existbanner.UpdateDate = DateTime.UtcNow;
            await _bannerRepository.CommitAsync();
        }
    }
}
