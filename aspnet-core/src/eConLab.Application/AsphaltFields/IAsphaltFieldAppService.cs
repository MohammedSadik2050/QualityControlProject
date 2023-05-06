using eConLab.AsphaltFields.Dto;
using eConLab.RC2.Dto;
using eConLab.TownShips.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.AsphaltFields
{
    public interface IAsphaltFieldAppService
    {
        Task<AsphaltFieldDto> CreateOrUpdate(AsphaltFieldDto input);

        Task<AsphaltFieldDto> GetByRequestandTest(long testId, long requestId);
        Task<List<AsphaltFieldDto>> GetByRequesId(long testId, long requestId);
    }
}
