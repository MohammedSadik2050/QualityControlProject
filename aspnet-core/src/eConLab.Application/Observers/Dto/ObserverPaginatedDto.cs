using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Observers.Dto
{
    public class ObserverPaginatedDto : PagedAndSortedResultRequestDto
    {
        public long Id { get; set; }
        public string Search { get; set; }
    }
   
}
