using Abp.Application.Services.Dto;
using eConLab.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.RequestTests.Dto
{
    public class CreateUpdateRequestTestDto : EntityDto<long>
    {
        public long RequestId { get; set; }
        public InspectionTestTypes InspectionTestType { get; set; }
        public long InspectionTestId { get; set; }
    }
}
