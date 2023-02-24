using AutoMapper;
using eConLab.Account;
using eConLab.QCUsers.Dto;
using eConLab.Req;
using eConLab.Tests.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.RequestTests.Dto
{

    public class RequestInspectionTestServiceProfile : Profile
    {
        public RequestInspectionTestServiceProfile()
        {

            CreateMap<RequestInspectionTest, RequestInspectionTestDto>().ReverseMap();
            CreateMap<CreateUpdateRequestTestDto, RequestInspectionTestDto>().ReverseMap();
            CreateMap<CreateUpdateRequestTestDto, RequestInspectionTest>().ReverseMap();
            //CreateMap<InspectionTestPaginatedDto, QCUserPagedAndSortedResultRequestDto>().ReverseMap();
        }
    }
}
