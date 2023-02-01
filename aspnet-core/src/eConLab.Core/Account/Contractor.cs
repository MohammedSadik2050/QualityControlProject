using Abp.Authorization.Users;
using Abp.Domain.Entities.Auditing;
using eConLab.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Account
{
    public class Contractor: AuditedAggregateRoot<long>
    {
     
        public string CommericalName { get; set; }
        public string CommericalRegisterationNumber { get; set; }
        public string Address { get; set; }
        public string ContactName { get; set; }

        public long UserId { get; set; }
    }
}
