using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Departments.Dto
{
    public class CreateUpdateDepartmentDto : EntityDto<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long AgencyId { get; set; }
    }
}
