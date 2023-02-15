using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.WF.Dto
{
    public class RequestWFPaginatedDto : PagedAndSortedResultRequestDto
    {

    }
    public class RequestWFFilter
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
