using System.Threading.Tasks;
using Abp.Application.Services;
using eConLab.Sessions.Dto;

namespace eConLab.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
