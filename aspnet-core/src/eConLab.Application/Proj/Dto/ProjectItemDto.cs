using Abp.Application.Services.Dto;
using eConLab.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Proj.Dto
{
    public class ProjectItemDto : EntityDto<long>
    {
        public long ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Quantity { get; set; }
    }
}
