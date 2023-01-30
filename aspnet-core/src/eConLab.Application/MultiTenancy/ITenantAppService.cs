using Abp.Application.Services;
using eConLab.MultiTenancy.Dto;

namespace eConLab.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

