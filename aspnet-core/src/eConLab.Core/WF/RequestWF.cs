using Abp.Domain.Entities.Auditing;
using eConLab.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.WF
{
    public class RequestWF : AuditedEntity<long>
    {
       public long RequestId { get; set; }
       public long CurrentUserId { get; set; }
        public Entities Entity { get; set; } = Entities.Request;

    }

    public class RequestWFHistory : AuditedEntity<long>
    {
        public long RequestWFId { get; set; }
        public long UserId { get; set; }
        public string ActionNotes { get; set; }
        public string  ActionName { get; set; }
        public Entities Entity { get; set; } = Entities.Request;

    }

  
}
