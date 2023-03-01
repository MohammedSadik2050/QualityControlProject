using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Departments.Dto
{
    public class DepartmentPaginatedDto : PagedAndSortedResultRequestDto
    {
        public long Id { get; set; }
        public string Search { get; set; }
    }
    public class DepartmentFilter
    {
        public long Id { get; set; }
        public string Name { get; set; }
       
    }
}
