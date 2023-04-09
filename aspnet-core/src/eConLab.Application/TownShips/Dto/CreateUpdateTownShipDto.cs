using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.TownShips.Dto
{
    public class CreateUpdateTownShipDto : EntityDto<long>
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
