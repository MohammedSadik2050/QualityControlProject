using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Account
{
    public class Agency : AuditedAggregateRoot<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long AgencyTypeId { get; set; }
        public string ResponsiblePerson { get; set; }
        public string PhoneNumber { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string WorkNumber { get; set; }
        public string Address { get; set; }
    }
}
