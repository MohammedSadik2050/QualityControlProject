using AutoMapper;
using eConLab.Req;
using eConLab.RequestTests.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.ReqProjectItems.Dto
{
    public class RequestProjectItemProfile : Profile
    {
        public RequestProjectItemProfile()
        {

            CreateMap<RequestProjectItemDto, RequestProjectItem>().ReverseMap();
        }
    }
}

