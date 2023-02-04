using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Abp.Authorization.Users;
using Abp.Domain.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using Abp.UI;
using eConLab.Authorization.Roles;
using eConLab.MultiTenancy;
using System.Linq.Dynamic.Core;
using Abp.Domain.Repositories;
using eConLab.Account;

namespace eConLab.Authorization.Users
{
    public class UserRegistrationManager : DomainService
    {
        public IAbpSession AbpSession { get; set; }

        //    private readonly eConLab.roles.IRoleAppService _roleService;
        private readonly TenantManager _tenantManager;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IRepository<Role, int> _roleRepo;

        public UserRegistrationManager(
            IRepository<Role, int> roleRepo,
            TenantManager tenantManager,
            UserManager userManager,
            RoleManager roleManager,
            IPasswordHasher<User> passwordHasher)
        {
            _tenantManager = tenantManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _passwordHasher = passwordHasher;
            _roleRepo = roleRepo;
            AbpSession = NullAbpSession.Instance;
        }

        public async Task<User> RegisterAsync(string name, string surname, string emailAddress, string userName, string plainPassword, bool isEmailConfirmed, string roleName = "")
        {
            CheckForTenant();

            var tenant = await GetActiveTenantAsync();

            var user = new User
            {
                TenantId = tenant.Id,
                Name = name,
                Surname = surname,
                EmailAddress = emailAddress,
                IsActive = true,
                UserName = userName,
                IsEmailConfirmed = isEmailConfirmed,
                Roles = new List<UserRole>()
            };

            user.SetNormalizedNames();

            if (!string.IsNullOrEmpty(roleName))
            {

                var defaultRole = await _roleManager.Roles.Where(r => r.Name == roleName).FirstOrDefaultAsync();
                if (defaultRole is null)
                {
                    //add new role 
                    //
                    var role = new Role
                    {
                        Name = roleName,
                        DisplayName = roleName,
                        NormalizedName = roleName.ToUpper(),
                    };
                    var roleId = _roleRepo.InsertAndGetId(role);

                    user.Roles.Add(new UserRole(tenant.Id, user.Id, role.Id));

                }
                else
                {
                    user.Roles.Add(new UserRole(tenant.Id, user.Id, defaultRole.Id));
                }

            }
            else
            {
                foreach (var defaultRole in await _roleManager.Roles.Where(r => r.IsDefault).ToListAsync())
                {
                    user.Roles.Add(new UserRole(tenant.Id, user.Id, defaultRole.Id));
                }
            }


            await _userManager.InitializeOptionsAsync(tenant.Id);

            CheckErrors(await _userManager.CreateAsync(user, plainPassword));
            await CurrentUnitOfWork.SaveChangesAsync();

            return user;
        }

        private void CheckForTenant()
        {

            if (!AbpSession.TenantId.HasValue)
            {
                throw new InvalidOperationException("Can not register host users!");
            }
        }

        private async Task<Tenant> GetActiveTenantAsync()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                return null;
            }

            return await GetActiveTenantAsync(AbpSession.TenantId.Value);
        }

        private async Task<Tenant> GetActiveTenantAsync(int tenantId)
        {
            var tenant = await _tenantManager.FindByIdAsync(tenantId);
            if (tenant == null)
            {
                throw new UserFriendlyException(L("UnknownTenantId{0}", tenantId));
            }

            if (!tenant.IsActive)
            {
                throw new UserFriendlyException(L("TenantIdIsNotActive{0}", tenantId));
            }

            return tenant;
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
