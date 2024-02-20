using Hospital.Business.CustomExceptions.CommonExceptions;
using Hospital.Business.Services.Interfaces;
using Hospital.Core.Models;
using Hospital.Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.Services.Implementations
{
    public class SettingService : ISettingService
    {
        private readonly ISettingRepository _settingRepository;

        public SettingService(ISettingRepository settingRepository)
        {
           _settingRepository = settingRepository;
        }
        public async Task<List<Setting>> GetAllAsync(Expression<Func<Setting, bool>>? expression = null, params string[]? includes)
        {
            return await _settingRepository.GetAllAsync(expression, includes).ToListAsync();
        }

        public async Task<Setting> GetByIdAsync(Expression<Func<Setting, bool>>? expression = null, params string[]? includes)
        {
            return await _settingRepository.GetAsync(expression, includes);
        }

        public async Task UpdateAsync(Setting setting)
        {
            var existSetting = await _settingRepository.GetAsync(x => x.Id == setting.Id);
            if (existSetting == null) throw new EntityNotFoundException();
            existSetting.UpdateDate = DateTime.UtcNow.AddHours(4);
            existSetting.Value = setting.Value;
            await _settingRepository.CommitAsync();
        }
    }
}
