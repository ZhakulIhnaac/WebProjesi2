using System.Collections.Generic;

namespace AycProjectBudgeting.Authentication.External
{
    public interface IExternalAuthConfiguration
    {
        List<ExternalLoginProviderInfo> Providers { get; }
    }
}
