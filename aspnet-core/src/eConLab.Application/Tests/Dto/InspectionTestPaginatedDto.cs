using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Tests.Dto
{
    public class InspectionTestPaginatedDto : PagedAndSortedResultRequestDto
    {
        public long Id { get; set; }
        public string Search { get; set; }
    }
    public class InspectionTestFilter
    {
        public long Id { get; set; }
        public string Name { get; set; }

    }
}
