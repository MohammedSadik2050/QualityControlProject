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
    public class ConcretFieldProfile : Profile
    {
        public ConcretFieldProfile()
        {
            CreateMap<ConcretFieldDto, eConLab.TestModels.ConcretField>().ReverseMap();
        }
    }
}
