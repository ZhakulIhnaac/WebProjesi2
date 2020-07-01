using Abp.MultiTenancy;
using AycProjectBudgeting.Authorization.Users;

namespace AycProjectBudgeting.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}
