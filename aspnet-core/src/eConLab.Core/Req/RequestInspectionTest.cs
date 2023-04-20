using Abp.Domain.Entities.Auditing;
using eConLab.Enum;
using eConLab.ProjectModels;
using eConLab.TestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Req
{
    public class RequestInspectionTest : FullAuditedEntity<long>
    {
        public long RequestId { get; set; }
        public InspectionTestTypes InspectionTestType { get; set; }
        public long InspectionTestId { get; set; }
        public InspectionTest InspectionTest { get; set; }
    }
}
