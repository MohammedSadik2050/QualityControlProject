using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using eConLab.Authorization.Roles;
using eConLab.Authorization.Users;
using eConLab.MultiTenancy;
using eConLab.Account;

namespace eConLab.EntityFrameworkCore
{
    public class eConLabDbContext : AbpZeroDbContext<Tenant, Role, User, eConLabDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<QCUser> QCUsers { get; set; }
        public DbSet<AgencyType> AgencyTypes { get; set; }
        public DbSet<Agency> Agencies { get; set; }
        public eConLabDbContext(DbContextOptions<eConLabDbContext> options)
            : base(options)
        {
        }
    }
}
