using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Authorization.Accounts.Dto.Contractor
{
    public class ContractorPagedAndSortedResultRequestDto : PagedAndSortedResultRequestDto
    {
        public long Id { get; set; }
    }
    public class ContractorFilter
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
