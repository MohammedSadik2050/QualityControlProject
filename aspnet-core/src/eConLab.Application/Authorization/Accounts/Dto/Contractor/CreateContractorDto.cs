using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.Extensions;
using eConLab.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Authorization.Accounts.Dto.Contractor
{
    public class CreateContractorDto
    {
       public ContractorDto ContractorInfo { get; set; }
       public RegisterInput RegisterInput { get; set; }
    }
}
