using Abp.Domain.Entities.Auditing;
using eConLab.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.TestModels
{
    public class RequestInspectionTest : AuditedEntity<long>
    {
        public long RequestId { get; set; }
        public InspectionTestTypes InspectionTestType { get; set; }
        public long InspectionTestId { get; set; }
    }
}
