using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using AycProjectBudgeting.Configuration;

namespace AycProjectBudgeting.Web.Host.Startup
{
    [DependsOn(
       typeof(AycProjectBudgetingWebCoreModule))]
    public class AycProjectBudgetingWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public AycProjectBudgetingWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AycProjectBudgetingWebHostModule).GetAssembly());
        }
    }
}
