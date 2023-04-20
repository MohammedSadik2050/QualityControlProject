using Abp.Domain.Entities.Auditing;
using eConLab.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Departments
{
    public class Department : FullAuditedEntity<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long AgencyId { get; set; }
        public Agency Agency { get; set; }
    }
}
