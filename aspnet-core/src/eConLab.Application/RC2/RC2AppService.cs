using Abp.Domain.Repositories;
using AutoMapper;
using eConLab.RC2.Dto;
using eConLab.TestModels;
using eConLab.TownShips;
using eConLab.TownShips.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.RC2
{
    public class RC2AppService :
        eConLabAppServiceBase, IRC2AppService
    {
        private readonly IRepository<eConLab.TestModels.RC2, long> _rc2Repository;
        private readonly IMapper _mapper;
        public RC2AppService(IMapper mapper,
          IRepository<eConLab.TestModels.RC2, long> rc2Repository
          )

        {
            _mapper = mapper;
            _rc2Repository = rc2Repository;
        }
        public async Task<RC2Dto> CreateOrUpdate(RC2Dto input)
        {
            var queryable =  _rc2Repository.GetAll().AsQueryable();
            var existRc=await  queryable.AsNoTracking().FirstOrDefaultAsync(x => x.RequestInspectionTestId == input.RequestInspectionTestId
                                                                    && x.RequestId == input.RequestId);
            if (existRc != null) { input.Id = existRc.Id; };
            var obj = _mapper.Map<eConLab.TestModels.RC2>(input);
            
                await _rc2Repository.InsertOrUpdateAsync(obj);
                await CurrentUnitOfWork.SaveChangesAsync();

            return input;
        }

        public async Task<List<RC2Dto>> GetByRequesId(long testId, long requestId)
        {
            var obje = _rc2Repository.GetAll().Where(x => x.RequestInspectionTestId == testId && x.RequestId == requestId).ToList();

            var obj = _mapper.Map<List<RC2Dto>>(obje);
            return obj;
        }

        public async Task<RC2Dto> GetByRequestandTest(long testId, long requestId)
        {
            var obje = await _rc2Repository.FirstOrDefaultAsync(x => x.RequestInspectionTestId == testId && x.RequestId == requestId);
            if (obje==null)
            {
                return new RC2Dto();
            }
            var obj = _mapper.Map<RC2Dto>(obje);
            return obj;
        }
    }
}
