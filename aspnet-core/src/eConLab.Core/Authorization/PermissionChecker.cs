using Abp.Authorization;
using eConLab.Authorization.Roles;
using eConLab.Authorization.Users;

namespace eConLab.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
