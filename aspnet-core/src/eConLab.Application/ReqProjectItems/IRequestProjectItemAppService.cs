using eConLab.Req;
using eConLab.ReqProjectItems.Dto;
using eConLab.RequestTests.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.ReqProjectItems
{
    public interface IRequestProjectItemAppService
    {
        Task<RequestProjectItemDto> CreateOrUpdate(CreateOrUpdateRequestProjectItemDto input);
        Task<RequestProjectItemDto> Get(long id);
        Task<List<RequestProjectItemViewDto>> GetAll(long requestId);
        Task<bool> Delete(long Id);
    }
}
