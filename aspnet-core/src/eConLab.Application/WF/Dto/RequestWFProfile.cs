using AutoMapper;
using eConLab.Proj.Dto;
using eConLab.ProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.WF.Dto
{
    public class RequestWFProfile : Profile
    {
        public RequestWFProfile()
        {
            CreateMap<RequestWFDto, RequestWF>().ReverseMap();
            CreateMap<RequestWFHistoryDto, RequestWFHistory>().ReverseMap();
        }
    }
}
