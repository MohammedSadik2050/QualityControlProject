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
        public long ProjectId { get; set; }
        public long ObserverId { get; set; }
        public string ContractNumber { get; set; }
        public string RequestCode { get; set; }
        public int Status { get; set; }
        public long TownShipId { get; set; }
    }
    public class RequestFilter
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
