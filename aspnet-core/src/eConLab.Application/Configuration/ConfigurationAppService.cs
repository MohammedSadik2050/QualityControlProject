﻿using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using eConLab.Configuration.Dto;

namespace eConLab.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : eConLabAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
