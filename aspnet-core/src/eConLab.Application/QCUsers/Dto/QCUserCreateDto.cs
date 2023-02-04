using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.Extensions;
using eConLab.Authorization.Accounts.Dto;
using eConLab.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.QCUsers.Dto
{
    public class QCUserCreateDto : EntityDto<long>
    {
        public QCUserDto QCUserInput { get; set; }
        public RegisterInput RegisterInput { get; set; }
    }
}
