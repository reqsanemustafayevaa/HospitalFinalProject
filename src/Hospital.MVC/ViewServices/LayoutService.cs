using Hospital.Business.Services.Interfaces;
using Hospital.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.MVC.ViewServices
{
    public class LayoutService
    {
        private readonly ISettingService _settingService;

        public LayoutService(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public async Task<List<Setting>> GetSetting()
        {
            var setting = await _settingService.GetAllAsync();
            return setting;
        }
    }
}
