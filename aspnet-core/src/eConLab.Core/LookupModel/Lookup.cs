using Abp.Domain.Entities.Auditing;
using eConLab.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.LookupModel
{
   
    public class LookupApp : AuditedEntity<long>
    {
        public string Name { get; set; }
        public LookupTypes LookupType { get; set; }

    }
}
