using AutoMapper;
using eConLab.Account;
using eConLab.QCUsers.Dto;
using eConLab.TestModels;
using eConLab.Tests.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Test.Dto
{
 
    public class InspectionTestServiceProfile : Profile
    {
        public InspectionTestServiceProfile()
        {

            CreateMap<InspectionTest, InspectionTestDto>().ReverseMap();
            CreateMap<CreateUpdateInspectionTestDto, InspectionTestDto>().ReverseMap();
            CreateMap<CreateUpdateInspectionTestDto, InspectionTest>().ReverseMap();
            //CreateMap<InspectionTestPaginatedDto, QCUserPagedAndSortedResultRequestDto>().ReverseMap();
        }
    }
}
