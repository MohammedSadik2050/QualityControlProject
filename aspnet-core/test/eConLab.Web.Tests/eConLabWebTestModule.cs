using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using eConLab.EntityFrameworkCore;
using eConLab.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace eConLab.Web.Tests
{
    [DependsOn(
        typeof(eConLabWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class eConLabWebTestModule : AbpModule
    {
        public eConLabWebTestModule(eConLabEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(eConLabWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(eConLabWebMvcModule).Assembly);
        }
    }
}