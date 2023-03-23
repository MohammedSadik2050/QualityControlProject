using Abp.Application.Services.Dto;
using eConLab.Enum;
using eConLab.WF.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.WF
{
    public interface IRequestWFAppService
    {
        Task<RequestWFDto> CreateOrUpdate(RequestWFDto input);
        Task<RequestWFDto> Get(long id);
        Task<List<RequestWFDto>> GetAll(RequestWFPaginatedDto input);
        Task<List<RequestWFHistoryDto>> GetAllHistory(long requestId, Entities entity);
    }
}
