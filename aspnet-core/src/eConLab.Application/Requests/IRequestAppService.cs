using Abp.Application.Services.Dto;
using eConLab.Proj.Dto;
using eConLab.Requests.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Requests
{
    public interface IRequestAppService
    {
        Task<RequestDto> CreateOrUpdate(RequestDto input);
        Task<PagedResultDto<RequestViewDto>> GetAll(RequestPaginatedDto input);
        Task<RequestViewDto> GetRequestView([Required] long id);
        Task<RequestDto> Get(long id);

    }
}
