using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Observers.Dto
{
    public class ObserverDto : EntityDto<long>
    {
        public long? UserId { get; set; }
        public string Name { get; set; }
        public string NationalId { get; set; }
        public string NationalityName { get; set; }
        public long TownShipId { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }

        //  [Required]
     
    }
}
