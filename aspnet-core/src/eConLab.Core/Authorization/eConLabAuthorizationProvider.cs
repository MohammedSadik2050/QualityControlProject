using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace eConLab.Authorization
{
    public class eConLabAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Manage_Agences, L("Agencies"));
            context.CreatePermission(PermissionNames.Pages_Manage_Departments, L("Departments"));
            context.CreatePermission(PermissionNames.Pages_Manage_TownShip, L("TownShip"));
            context.CreatePermission(PermissionNames.Pages_Manage_Contractor, L("Contractor"));
            context.CreatePermission(PermissionNames.Pages_Manage_LabProjectManager, L("LabProjectManager"));
            context.CreatePermission(PermissionNames.Pages_Manage_SupervisingQuality, L("SupervisingQuality"));
            context.CreatePermission(PermissionNames.Pages_Manage_Observer, L("Observer"));
            context.CreatePermission(PermissionNames.Pages_Manage_Requests, L("Requests"));
            context.CreatePermission(PermissionNames.Pages_Manage_Project, L("Projects"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.Pages_Manage_InspectionTest, L("InspectionTest"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, eConLabConsts.LocalizationSourceName);
        }
    }
}
