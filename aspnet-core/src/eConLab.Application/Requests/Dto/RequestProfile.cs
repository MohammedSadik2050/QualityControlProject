using AutoMapper;
using eConLab.Proj.Dto;
using eConLab.ProjectModels;
using eConLab.Req;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Requests.Dto
{
    public class RequestProfile : Profile
    {
        public RequestProfile()
        {
            CreateMap<RequestDto, Request>().ReverseMap();
            CreateMap<RequestDto, RequestViewDto>().ReverseMap();
            CreateMap<RequestViewDto, Request>().ReverseMap();
        }
    }
}
