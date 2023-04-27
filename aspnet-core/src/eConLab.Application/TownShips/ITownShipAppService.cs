using Abp.Application.Services.Dto;
using eConLab.Agencies.Dto;
using eConLab.TownShips.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.TownShips
{
    public interface ITownShipAppService
    {

        Task<TownShipDto> CreateOrUpdate(CreateUpdateTownShipDto input);
        Task<PagedResultDto<TownShipDto>> GetAll(TownShipPaginatedDto input);

        Task<TownShipDto> Get(long id);

        Task<List<TownShipDto>> GetAllownShipList();

        Task<bool> Delete(long Id);
    }
}
