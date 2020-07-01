using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using AycProjectBudgeting.Configuration.Dto;

namespace AycProjectBudgeting.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : AycProjectBudgetingAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
