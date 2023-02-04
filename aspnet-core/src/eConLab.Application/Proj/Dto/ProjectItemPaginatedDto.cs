using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Proj.Dto
{
    public class ProjectItemPaginatedDto : PagedAndSortedResultRequestDto
    {

    }
    public class ProjectItemFilter
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
