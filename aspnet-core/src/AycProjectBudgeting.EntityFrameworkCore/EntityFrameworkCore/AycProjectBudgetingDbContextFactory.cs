using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using AycProjectBudgeting.Configuration;
using AycProjectBudgeting.Web;

namespace AycProjectBudgeting.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class AycProjectBudgetingDbContextFactory : IDesignTimeDbContextFactory<AycProjectBudgetingDbContext>
    {
        public AycProjectBudgetingDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AycProjectBudgetingDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            AycProjectBudgetingDbContextConfigurer.Configure(builder, configuration.GetConnectionString(AycProjectBudgetingConsts.ConnectionStringName));

            return new AycProjectBudgetingDbContext(builder.Options);
        }
    }
}
