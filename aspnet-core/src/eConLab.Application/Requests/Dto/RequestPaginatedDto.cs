using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Requests.Dto
{
    public class RequestPaginatedDto : PagedAndSortedResultRequestDto
    {

    }
    public class RequestFilter
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
