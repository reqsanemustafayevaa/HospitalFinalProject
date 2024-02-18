using Hospital.Business.CustomExceptions.CommonExceptions;
using Hospital.Business.CustomExceptions.ProfessionExceptions;
using Hospital.Business.Services.Interfaces;
using Hospital.Core.Models;
using Hospital.Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.Services.Implementations
{
    public class ProfessionService : IProfessionService
    {
        private readonly IProfessionRepository _professionRepository;

        public ProfessionService(IProfessionRepository professionRepository)
        {
           _professionRepository = professionRepository;
        }
        public async Task CreateAsync(Profession profession)
        {
            if (profession == null)
            {
                throw new EntityNotFoundException();
            }
            if (_professionRepository.Table.Any(x => x.Name.ToLower() == profession.Name.ToLower()))
            {
                throw new AllreadyExistException("Name", "Profession has already created!");
            }
            profession.IsDeleted = false;
            profession.CreateDate = DateTime.UtcNow;
            profession.UpdateDate = DateTime.UtcNow;

            await _professionRepository.createAsync(profession);
            await _professionRepository.CommitAsync();
        }

        public async Task Delete(int id)
        {
            var profession=await _professionRepository.GetAsync(x=>x.Id==id);
            if (profession==null)
            {
                throw new EntityNotFoundException();
            }
            _professionRepository.Delete(profession);
            await _professionRepository.CommitAsync();
        }

        public async Task<List<Profession>> GetAllAsync()
        {
           return await _professionRepository.GetAllAsync().ToListAsync();
        }

        public  Task<Profession> GetByIdAsync(int id)
        {
            var wantedprofession= _professionRepository.GetAsync(x=>x.Id==id);
            if (wantedprofession == null)
            {
                throw new EntityNotFoundException();
            }
            return wantedprofession;
        }

        public async Task UpdateAsync(Profession profession)
        {
            if(profession is null) { throw new EntityNotFoundException(); }
            var existprofession=await _professionRepository.GetAsync(x=> x.Id==profession.Id );
            if (existprofession == null) {  throw new EntityNotFoundException(); }
            if (_professionRepository.Table.Any(x => x.Name == profession.Name && x.Id!=profession.Id))
                throw new ProfessionAllreadyExistException("Name","Profession allready exist!");

            existprofession.Name = profession.Name;
            existprofession.Description = profession.Description;
          
            profession.IsDeleted = false;
            profession.UpdateDate = DateTime.UtcNow;
            await _professionRepository.CommitAsync();

        }
    }
}
