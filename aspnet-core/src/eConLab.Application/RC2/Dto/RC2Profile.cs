using AutoMapper;
using eConLab.Req;
using eConLab.Requests.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.RC2.Dto
{
    public class RC2Profile : Profile
    {
        public RC2Profile()
        {
            CreateMap<RC2Dto, eConLab.TestModels.RC2>().ReverseMap();
        }
    }
}
