using Abp.Application.Services.Dto;
using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eConLab.Agencies.Dto;
using eConLab.Account;
using eConLab.QCUsers.Dto;
using eConLab.Departments.Dto;

namespace eConLab.Agencies
{
  
    public interface IDepartmentAppService :
        IApplicationService
    {
        Task<DepartmentDto> CreateOrUpdate(CreateUpdateDepartmentDto input);
        Task<DepartmentDto> Get(long id);
        Task<PagedResultDto<DepartmentDto>> GetAll(DepartmentPaginatedDto input);
        Task<bool> Delete(long Id);
    }

}
