using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using eConLab.Authorization.Roles;
using eConLab.Authorization.Users;
using eConLab.MultiTenancy;
using eConLab.Account;
using eConLab.ProjectModels;
using System.Linq;
using eConLab.LookupModel;
using eConLab.TestModels;
using eConLab.Req;
using eConLab.WF;
using eConLab.Departments;
using eConLab.Attachment;

namespace eConLab.EntityFrameworkCore
{
    public class eConLabDbContext : AbpZeroDbContext<Tenant, Role, User, eConLabDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<QCUser> QCUsers { get; set; }
        public DbSet<AgencyType> AgencyTypes { get; set; }
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Attachments> Attachments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<ProjectItem> ProjectItems { get; set; }
        public DbSet<LookupApp> LookupApp { get; set; }
        public DbSet<InspectionTest> InspectionTests { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestProjectItem> RequestProjectItems { get; set; }
        public DbSet<RequestWF> RequestWFs { get; set; }
        public DbSet<RequestWFHistory> RequestWFHistories { get; set; }
        public DbSet<RequestInspectionTest> RequestInspectionTests { get; set; }
        public eConLabDbContext(DbContextOptions<eConLabDbContext> options)
            : base(options)
        {
        }
    }
}
