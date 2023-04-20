using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Account
{
    public class AgencyType : FullAuditedEntity<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
