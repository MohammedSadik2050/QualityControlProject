using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.TownShips.Dto
{
    public class TownShipPaginatedDto : PagedAndSortedResultRequestDto
    {
        public long Id { get; set; }
        public string Search { get; set; }
    }
    public class TownShipFilter
    {
        public long Id { get; set; }
        public string Name { get; set; }
       
    }
}
