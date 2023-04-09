using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Observers.Dto
{
 
    public class ObserverProfile : Profile
    {
        public ObserverProfile()
        {

            CreateMap<Observer, ObserverDto>().ReverseMap();
            CreateMap<CreateUpdateObserverDto, Observer>().ReverseMap();
            CreateMap<CreateUpdateObserverDto, ObserverDto>().ReverseMap();
        }
    }
}
