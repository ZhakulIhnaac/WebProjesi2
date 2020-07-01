using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace AycProjectBudgeting.EntityFrameworkCore
{
    public static class AycProjectBudgetingDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<AycProjectBudgetingDbContext> builder, string connectionString)
        {
            builder.UseNpgsql(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<AycProjectBudgetingDbContext> builder, DbConnection connection)
        {
            builder.UseNpgsql(connection);
        }
    }
}