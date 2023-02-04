using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.Domain.Entities;
using eConLab.Account;
using eConLab.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.QCUsers.Dto
{
    public class QCUserDto:EntityDto<long>
    {
        public UserTypes UserTypes { get; set; }
        public string Name { get; set; }
        public string NationalId { get; set; }
        public string NationalityName { get; set; }
        public string StaffNumber { get; set; }
        public long AgencyId { get; set; }
        public string PhoneNumber { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string WorkPlace { get; set; }
        public string Address { get; set; }
        public string Fax { get; set; }
        public long UserId { get; set; }
    }
}
