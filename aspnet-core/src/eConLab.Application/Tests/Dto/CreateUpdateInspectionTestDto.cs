using Abp.Application.Services.Dto;
using eConLab.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Tests.Dto
{
    public class CreateUpdateInspectionTestDto : EntityDto<long>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public float Cost { get; set; }
        public bool IsLabTest { get; set; }
        public InspectionTestTypes TestType { get; set; }
        public TestForms? TestForm { get; set; }
    }
}
