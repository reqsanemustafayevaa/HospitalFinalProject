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
    public class GalleryService : IGalleryService
    {
        private readonly IGalleryRepository _galleryRepository;
        private readonly IWebHostEnvironment _env;

        public GalleryService(IGalleryRepository galleryRepository,IWebHostEnvironment env)
        {
            _galleryRepository = galleryRepository;
           _env = env;
        }
        public async Task CreateAsync(Gallery gallery)
        {
            if (gallery == null)
            {
                throw new EntityNotFoundException();
            }
            if (gallery.ImageFile is not null)
            {
                if (gallery.ImageFile.ContentType != "image/png" && gallery.ImageFile.ContentType != "image/jpeg")
                {
                    throw new InvalidImageContentException("ImageFile", "File must be .jpeg or .png!");
                }
                if (gallery.ImageFile.Length > 1072346)
                {
                    throw new InvalidImageSizeException("ImageFile", "File must be lower than 1mb!");
                }
                gallery.ImageUrl = gallery.ImageFile.SaveFile(_env.WebRootPath, "uploads/galleries");
            }
            else
            {
                throw new InvalidImageFileException("ImageFile", "ImageFile is required");
            }
            gallery.IsDeleted = false;
            gallery.CreateDate = DateTime.UtcNow;
            gallery.UpdateDate = DateTime.UtcNow;
            await _galleryRepository.createAsync(gallery);
            await _galleryRepository.CommitAsync();
        }

        public async Task Delete(int id)
        {
            var existgallery = await _galleryRepository.GetAsync(x => x.Id == id);
            if (existgallery is null)
            {
                throw new EntityNotFoundException();
            }
            Helper.DeleteFile(_env.WebRootPath, "uploads/galleries", existgallery.ImageUrl);
            _galleryRepository.Delete(existgallery);
            await _galleryRepository.CommitAsync();
        }

        public async Task<List<Gallery>> GetAllAsync()
        {
            return await _galleryRepository.GetAllAsync().ToListAsync();
        }

        public Task<Gallery> GetByIdAsync(int id)
        {
            var gallery = _galleryRepository.GetAsync(x => x.Id == id);
            if (gallery is null)
            {
                throw new EntityNotFoundException();
            }

            return gallery;
        }

        public async Task UpdateAsync(Gallery gallery)
        {
            var existgallery = await _galleryRepository.GetAsync(x => x.Id == gallery.Id);
            if (gallery is null)
            {
                throw new EntityNotFoundException();
            }
            if (gallery.ImageFile is not null)
            {
                if (gallery.ImageFile.ContentType != "image/png" && gallery.ImageFile.ContentType != "image/jpeg")
                {
                    throw new InvalidImageContentException("ImageFile", "File must be .jpeg or .png!");
                }
                if (gallery.ImageFile.Length > 1072346)
                {
                    throw new InvalidImageSizeException("ImageFile", "File must be lower than 1mb!");
                }
                Helper.DeleteFile(_env.WebRootPath, "uploads/galleries", existgallery.ImageUrl);
                existgallery.ImageUrl = gallery.ImageFile.SaveFile(_env.WebRootPath, "uploads/galleries");
            }
            else
            {
                throw new InvalidImageFileException("ImageFile", "ImageFile is required");
            }

            existgallery.IsDeleted = false;
            existgallery.UpdateDate = DateTime.UtcNow;
            await _galleryRepository.CommitAsync();
        }
    }
}
