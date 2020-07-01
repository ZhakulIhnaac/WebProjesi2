using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using AycProjectBudgeting.EntityFrameworkCore.Seed;

namespace AycProjectBudgeting.EntityFrameworkCore
{
    [DependsOn(
        typeof(AycProjectBudgetingCoreModule), 
        typeof(AbpZeroCoreEntityFrameworkCoreModule))]
    public class AycProjectBudgetingEntityFrameworkModule : AbpModule
    {
        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }

        public override void PreInitialize()
        {
            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<AycProjectBudgetingDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        AycProjectBudgetingDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        AycProjectBudgetingDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                    }
                });
            }
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AycProjectBudgetingEntityFrameworkModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            if (!SkipDbSeed)
            {
                SeedHelper.SeedHostDb(IocManager);
            }
        }
    }
}
