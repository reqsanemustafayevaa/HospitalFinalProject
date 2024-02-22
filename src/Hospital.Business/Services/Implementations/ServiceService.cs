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
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IWebHostEnvironment _env;

        public ServiceService(IServiceRepository serviceRepository,IWebHostEnvironment env)
        {
           _serviceRepository = serviceRepository;
           _env = env;
        }
        public async Task CreateAsync(Service service)
        {
            if (service == null)
            {
                throw new EntityNotFoundException();
            }
            if (service.ImageFile is not null)
            {
                if (service.ImageFile.ContentType != "image/png" && service.ImageFile.ContentType != "image/jpeg")
                {
                    throw new InvalidImageContentException("ImageFile", "File must be .jpeg or .png!");
                }
                if (service.ImageFile.Length > 1072346)
                {
                    throw new InvalidImageSizeException("ImageFile", "File must be lower than 1mb!");
                }
                service.ImageUrl = service.ImageFile.SaveFile(_env.WebRootPath, "uploads/services");
            }
            service.IsDeleted = false;
            service.CreateDate = DateTime.UtcNow;
            service.UpdateDate = DateTime.UtcNow;

            await _serviceRepository.createAsync(service);
            await _serviceRepository.CommitAsync();
        }

        public async Task Delete(int id)
        {
            var existservice = await _serviceRepository.GetAsync(x => x.Id == id);
            if (existservice is null)
            {
                throw new EntityNotFoundException();
            }
            Helper.DeleteFile(_env.WebRootPath, "uploads/services", existservice.ImageUrl);
            _serviceRepository.Delete(existservice);
            await _serviceRepository.CommitAsync();
        }

        public async Task<List<Service>> GetAllAsync()
        {
            return await _serviceRepository.GetAllAsync().ToListAsync();
        }

        public Task<Service> GetByIdAsync(int id)
        {
            var service = _serviceRepository.GetAsync(x => x.Id == id);
            if (service is null)
            {
                throw new EntityNotFoundException();
            }

            return service;
        }

        public async Task UpdateAsync(Service service)
        {
            var existservice = await _serviceRepository.GetAsync(x => x.Id == service.Id);
            if (service is null)
            {
                throw new EntityNotFoundException();
            }
            if (service.ImageFile is not null)
            {
                if (service.ImageFile.ContentType != "image/png" && service.ImageFile.ContentType != "image/jpeg")
                {
                    throw new InvalidImageContentException("ImageFile", "File must be .jpeg or .png!");
                }
                if (service.ImageFile.Length > 1072346)
                {
                    throw new InvalidImageSizeException("ImageFile", "File must be lower than 1mb!");
                }
                Helper.DeleteFile(_env.WebRootPath, "uploads/services", existservice.ImageUrl);
                existservice.ImageUrl = service.ImageFile.SaveFile(_env.WebRootPath, "uploads/services");
            }
            existservice.Title = service.Title;
            existservice.Description = service.Description;
            existservice.Name = service.Name;
           
            existservice.IsDeleted = false;
            existservice.UpdateDate = DateTime.UtcNow;
            await _serviceRepository.CommitAsync();
        }
    }
}
