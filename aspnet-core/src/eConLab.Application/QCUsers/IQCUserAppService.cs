using Abp.Application.Services;
using Abp.Application.Services.Dto;
using eConLab.QCUsers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.QCUsers
{
    public interface IQCUserAppService : IApplicationService
    {
        Task<QCUserDto> CreateOrUpdate(QCUserCreateDto input);
        Task<PagedResultDto<QCUserDto>> GetAll(QCUserPagedAndSortedResultRequestDto input);
        Task<QCUserDto> GetById(long id);
    }
}
