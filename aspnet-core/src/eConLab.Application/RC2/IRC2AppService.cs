using eConLab.RC2.Dto;
using eConLab.TownShips.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.RC2
{
    public interface IRC2AppService
    {
        Task<RC2Dto> CreateOrUpdate(RC2Dto input);

        Task<RC2Dto> GetByRequestandTest(long testId, long requestId);
        Task<List<RC2Dto>> GetByRequesId(long testId, long requestId);
    }
}
