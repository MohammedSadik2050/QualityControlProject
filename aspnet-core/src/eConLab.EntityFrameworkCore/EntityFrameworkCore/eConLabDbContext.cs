using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using eConLab.Authorization.Roles;
using eConLab.Authorization.Users;
using eConLab.MultiTenancy;

namespace eConLab.EntityFrameworkCore
{
    public class eConLabDbContext : AbpZeroDbContext<Tenant, Role, User, eConLabDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public eConLabDbContext(DbContextOptions<eConLabDbContext> options)
            : base(options)
        {
        }
    }
}
