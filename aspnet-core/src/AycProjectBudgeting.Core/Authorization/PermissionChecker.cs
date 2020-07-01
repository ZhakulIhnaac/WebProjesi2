using Abp.Authorization;
using AycProjectBudgeting.Authorization.Roles;
using AycProjectBudgeting.Authorization.Users;

namespace AycProjectBudgeting.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
