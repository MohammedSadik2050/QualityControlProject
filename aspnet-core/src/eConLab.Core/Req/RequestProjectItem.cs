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
    public class RequestProjectItem : AuditedEntity<long>
    {
        public long RequestId { get; set; }
        public ProjectItem ProjectItem { get; set; }
        public long ProjectItemId { get; set; }
    }
}
