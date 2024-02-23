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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.Services.Implementations
{
    public class FeatureService : IFeatureService
    {
        private readonly IFeatureRepository _featureRepository;
        private readonly IWebHostEnvironment _env;

        public FeatureService(IFeatureRepository featureRepository,IWebHostEnvironment env)
        {
           _featureRepository = featureRepository;
           _env = env;
        }
        public async Task CreateAsync(Feature feature)
        {
            if (feature == null)
            {
                throw new EntityNotFoundException();
            }
            if (feature.ImageFile is not null)
            {
                if (feature.ImageFile.ContentType != "image/png" && feature.ImageFile.ContentType != "image/jpeg")
                {
                    throw new InvalidImageContentException("ImageFile", "File must be .jpeg or .png!");
                }
                if (feature.ImageFile.Length > 1072346)
                {
                    throw new InvalidImageSizeException("ImageFile", "File must be lower than 1mb!");
                }
                feature.ImageUrl = feature.ImageFile.SaveFile(_env.WebRootPath, "uploads/features");
            }
            else
            {
                throw new InvalidImageFileException("ImageFile", "ImageFile is required");
            }
            feature.IsDeleted = false;
            feature.CreateDate = DateTime.UtcNow;
            feature.UpdateDate = DateTime.UtcNow;

            await _featureRepository.createAsync(feature);
            await _featureRepository.CommitAsync();
        }

        public async Task Delete(int id)
        {
            var existfeature = await _featureRepository.GetAsync(x => x.Id == id);
            if (existfeature is null)
            {
                throw new EntityNotFoundException();
            }
            Helper.DeleteFile(_env.WebRootPath, "uploads/features", existfeature.ImageUrl);
            _featureRepository.Delete(existfeature);
            await _featureRepository.CommitAsync();
        }

        public async Task<List<Feature>> GetAllAsync()
        {
            return await _featureRepository.GetAllAsync().ToListAsync();
        }

        public Task<Feature> GetByIdAsync(int id)
        {
            var feature = _featureRepository.GetAsync(x => x.Id == id);
            if (feature is null)
            {
                throw new EntityNotFoundException();
            }

            return feature;
        }

        public async Task UpdateAsync(Feature feature)
        {
            var existfeature = await _featureRepository.GetAsync(x => x.Id == feature.Id);
            if (feature is null)
            {
                throw new EntityNotFoundException();
            }
            if (feature.ImageFile is not null)
            {
                if (feature.ImageFile.ContentType != "image/png" && feature.ImageFile.ContentType != "image/jpeg")
                {
                    throw new InvalidImageContentException("ImageFile", "File must be .jpeg or .png!");
                }
                if (feature.ImageFile.Length > 1072346)
                {
                    throw new InvalidImageSizeException("ImageFile", "File must be lower than 1mb!");
                }
                Helper.DeleteFile(_env.WebRootPath, "uploads/features", existfeature.ImageUrl);
                existfeature.ImageUrl = feature.ImageFile.SaveFile(_env.WebRootPath, "uploads/features");
            }
            else
            {
                throw new InvalidImageFileException("ImageFile", "ImageFile is required");
            }
            existfeature.Title = feature.Title;
            existfeature.Description = feature.Description;
            existfeature.Item1 = feature.Item1;
            existfeature.Item2 = feature.Item2;
            existfeature.Item3 = feature.Item3;
            existfeature.Item4 = feature.Item4;
            existfeature.Item5 = feature.Item5;
            existfeature.IsDeleted = false;
            existfeature.UpdateDate = DateTime.UtcNow;
            await _featureRepository.CommitAsync();
        }
    }
}
