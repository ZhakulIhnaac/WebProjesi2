using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using AycProjectBudgeting.Authorization;

namespace AycProjectBudgeting
{
    [DependsOn(
        typeof(AycProjectBudgetingCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class AycProjectBudgetingApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<AycProjectBudgetingAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(AycProjectBudgetingApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
