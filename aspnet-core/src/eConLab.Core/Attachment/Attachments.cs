using Abp.Domain.Entities.Auditing;
using eConLab.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Attachment
{
    public class Attachments : AuditedEntity<long>
    {
        public long EntityId  { get; set; }
        public string FilePath { get; set; }
        public string FileURL { get; set; }
        public string Description { get; set; }
        public Entities Entity { get; set; }
    }
}
