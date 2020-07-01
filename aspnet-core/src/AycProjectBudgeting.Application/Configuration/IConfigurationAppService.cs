using System.Threading.Tasks;
using AycProjectBudgeting.Configuration.Dto;

namespace AycProjectBudgeting.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
