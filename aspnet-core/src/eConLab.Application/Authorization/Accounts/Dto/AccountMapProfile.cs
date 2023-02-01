using Abp.Authorization.Roles;
using Abp.Authorization;
using AutoMapper;
using eConLab.Authorization.Roles;
using eConLab.Roles.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eConLab.Authorization.Accounts.Dto.Contractor;

namespace eConLab.Authorization.Accounts.Dto
{
    
    public class AccountMapProfile : Profile
    {
        public AccountMapProfile()
        {
           
            CreateMap<ContractorDto, eConLab.Account.Contractor>().ReverseMap();
        }
    }
}
