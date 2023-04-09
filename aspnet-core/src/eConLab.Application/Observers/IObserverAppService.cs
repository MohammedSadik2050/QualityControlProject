using Abp.Application.Services.Dto;
using eConLab.Observers.Dto;
using eConLab.QCUsers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Observers
{
    public interface IObserverAppService
    {
        Task<ObserverDto> CreateOrUpdate(CreateUpdateObserverDto input);
        Task<PagedResultDto<ObserverDto>> GetAll(ObserverPaginatedDto input);
        Task<ObserverDto> GetById(long id);
    }
}
