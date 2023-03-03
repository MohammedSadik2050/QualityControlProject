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
  
    public interface IAgencyAppService :
        IApplicationService
    {
        Task<AgencyDto> CreateOrUpdate(CreateUpdateAgencyDto input);
        Task<PagedResultDto<AgencyDto>> GetAll(AgencyPaginatedDto input);
        Task<AgencyDto> Get(long id);
        Task<List<AgencyTypeDto>> GetAllAgencyTypeList();
        Task<List<AgencyDto>> GetAllAgenciesList();

        Task<bool> Delete(long Id);
    }

}
