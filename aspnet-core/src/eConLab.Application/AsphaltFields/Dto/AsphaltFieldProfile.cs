using AutoMapper;
using eConLab.Req;
using eConLab.Requests.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.AsphaltFields.Dto
{
    public class AsphaltFieldProfile : Profile
    {
        public AsphaltFieldProfile()
        {
            CreateMap<AsphaltFieldDto, eConLab.TestModels.AsphaltField>().ReverseMap();
        }
    }
}
