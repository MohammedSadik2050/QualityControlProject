using Abp.Application.Services;
using eConLab.Authorization.Accounts.Dto.Contractor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Authorization.Accounts
{
    public interface  IContractorAppService: IApplicationService
    {
        Task<ContractorDto> Create(CreateContractorDto input);
    }
}
