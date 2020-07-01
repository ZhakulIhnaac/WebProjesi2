using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace AycProjectBudgeting.Controllers
{
    public abstract class AycProjectBudgetingControllerBase: AbpController
    {
        protected AycProjectBudgetingControllerBase()
        {
            LocalizationSourceName = AycProjectBudgetingConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
