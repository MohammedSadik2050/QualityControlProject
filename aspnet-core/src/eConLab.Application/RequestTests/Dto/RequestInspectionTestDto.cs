using Abp.Application.Services.Dto;
using eConLab.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.RequestTests.Dto
{
    public class RequestInspectionTestViewDto : EntityDto<long>
    {
        public long RequestId { get; set; }
        public InspectionTestTypes InspectionTestType { get; set; }
        public long InspectionTestId { get; set; }
        public float Cost { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool HaveResult { get; set; } = false;
        public bool IsLab { get; set; } = false;

    }
}
