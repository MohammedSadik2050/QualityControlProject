using System.Threading.Tasks;
using Abp.Application.Services;
using eConLab.Authorization.Accounts.Dto;

namespace eConLab.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
        Task<RegisterResult> RegisterUserByRole(RegisterInput input,string roleName="");
    }
}
