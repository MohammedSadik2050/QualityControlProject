using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Agencies.Dto
{
    public class AgencyTypeDto:EntityDto<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
