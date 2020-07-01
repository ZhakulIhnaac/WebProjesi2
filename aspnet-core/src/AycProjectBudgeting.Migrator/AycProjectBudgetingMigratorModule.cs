using Microsoft.Extensions.Configuration;
using Castle.MicroKernel.Registration;
using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using AycProjectBudgeting.Configuration;
using AycProjectBudgeting.EntityFrameworkCore;
using AycProjectBudgeting.Migrator.DependencyInjection;

namespace AycProjectBudgeting.Migrator
{
    [DependsOn(typeof(AycProjectBudgetingEntityFrameworkModule))]
    public class AycProjectBudgetingMigratorModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public AycProjectBudgetingMigratorModule(AycProjectBudgetingEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

            _appConfiguration = AppConfigurations.Get(
                typeof(AycProjectBudgetingMigratorModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                AycProjectBudgetingConsts.ConnectionStringName
            );

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(
                typeof(IEventBus), 
                () => IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                )
            );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AycProjectBudgetingMigratorModule).GetAssembly());
            ServiceCollectionRegistrar.Register(IocManager);
        }
    }
}
