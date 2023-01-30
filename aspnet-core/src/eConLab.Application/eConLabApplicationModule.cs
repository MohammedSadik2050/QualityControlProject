using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using eConLab.Authorization;

namespace eConLab
{
    [DependsOn(
        typeof(eConLabCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class eConLabApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<eConLabAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(eConLabApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
