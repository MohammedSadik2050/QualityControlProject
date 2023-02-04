using AutoMapper;
using eConLab.Account;
using eConLab.QCUsers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Agencies.Dto
{
 
    public class AgencyProfile : Profile
    {
        public AgencyProfile()
        {

            CreateMap<Agency, AgencyDto>().ReverseMap();
            CreateMap<CreateUpdateAgencyDto, Agency>().ReverseMap();
            CreateMap<CreateUpdateAgencyDto, AgencyDto>().ReverseMap();
            CreateMap<AgencyTypeDto, AgencyType>().ReverseMap();
            CreateMap<AgencyPaginatedDto, QCUserPagedAndSortedResultRequestDto>().ReverseMap();
        }
    }
}
