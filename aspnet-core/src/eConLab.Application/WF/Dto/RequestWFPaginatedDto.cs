using Abp.Application.Services.Dto;
using eConLab.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.WF.Dto
{
    public class RequestWFPaginatedDto : PagedAndSortedResultRequestDto
    {
        public long RequestId { get; set; }
        public Entities Entitiy { get; set; }
    }
    public class RequestWFFilter
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
