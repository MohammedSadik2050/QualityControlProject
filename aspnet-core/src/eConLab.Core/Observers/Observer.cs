using Abp.Domain.Entities.Auditing;
using eConLab.Authorization.Users;
using eConLab.ProjectModels;
using eConLab.TownShips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Observers
{
    public class Observer : FullAuditedEntity<long>
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string NationalId { get; set; }
        public string NationalityName { get; set; }
        public long TownShipId { get; set; }
        public TownShip TownShip { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
