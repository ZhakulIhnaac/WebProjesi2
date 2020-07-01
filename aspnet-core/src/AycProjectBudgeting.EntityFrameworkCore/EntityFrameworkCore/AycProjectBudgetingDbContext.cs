using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using AycProjectBudgeting.Authorization.Roles;
using AycProjectBudgeting.Authorization.Users;
using AycProjectBudgeting.MultiTenancy;
using Abp.Localization;
using Domain.Entity;

namespace AycProjectBudgeting.EntityFrameworkCore
{
    public class AycProjectBudgetingDbContext : AbpZeroDbContext<Tenant, Role, User, AycProjectBudgetingDbContext>
    {


        #region projectBudgetingDbSets
        public virtual DbSet<Activity> Activity { get; set; }
        public virtual DbSet<Expense> Expense { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<Expert> Expert { get; set; }
        public virtual DbSet<ExpertChannel> ExpertChannel { get; set; }
        public virtual DbSet<ExpertProjectAssignment> ExpertProjectAssignment { get; set; }

        #endregion

        #region definitionDbSets
        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<ExpenseType> ExpenseType { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<ChannelType> ChannelType { get; set; }

        #endregion


        public AycProjectBudgetingDbContext(DbContextOptions<AycProjectBudgetingDbContext> options)
            : base(options)
        {
        }

        // add these lines to override max length of property
        // we should set max length smaller than the PostgreSQL allowed size (10485760)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationLanguageText>()
                .Property(p => p.Value)
                .HasMaxLength(100); // any integer that is smaller than 10485760
        }
    }
}
