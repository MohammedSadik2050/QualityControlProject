using Abp.Authorization.Users;
using eConLab.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Authorization.Accounts.Dto.Contractor
{
    public class ContractorDto
    {
        public string CommericalName { get; set; }
        public string CommericalRegisterationNumber { get; set; }
        public string Address { get; set; }
        public string ContactName { get; set; }

        public long UserId { get; set; }
    }
}
