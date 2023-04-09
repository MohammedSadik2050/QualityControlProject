using AutoMapper;
using eConLab.Account;
using eConLab.Agencies.Dto;
using eConLab.QCUsers.Dto;
using eConLab.TownShips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.TownShips.Dto
{
 
    public class TownShipProfile : Profile
    {
        public TownShipProfile()
        {

            CreateMap<TownShip, TownShipDto>().ReverseMap();
            CreateMap<CreateUpdateTownShipDto, TownShip>().ReverseMap();
            CreateMap<CreateUpdateTownShipDto, TownShipDto>().ReverseMap();
        }
    }
}
