using Abp.Domain.Repositories;
using AutoMapper;
using eConLab.AsphaltFields;
using eConLab.AsphaltFields.Dto;
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
    public class ConcretFieldAppService :
        eConLabAppServiceBase, IConcretFieldAppService
    {
        private readonly IRepository<ConcretField, long> _concretFieldRepository;
        private readonly IMapper _mapper;
        public ConcretFieldAppService(IMapper mapper,
          IRepository<ConcretField, long> concretFieldRepository
          )

        {
            _mapper = mapper;
            _concretFieldRepository = concretFieldRepository;
        }
        public async Task<ConcretFieldDto> CreateOrUpdate(ConcretFieldDto input)
        {
            var queryable = _concretFieldRepository.GetAll().AsQueryable();
            var existRc = await queryable.AsNoTracking().FirstOrDefaultAsync(x => x.RequestInspectionTestId == input.RequestInspectionTestId
                                                                    && x.RequestId == input.RequestId);
            if (existRc != null) { input.Id = existRc.Id; };
            var obj = _mapper.Map<ConcretField>(input);

            await _concretFieldRepository.InsertOrUpdateAsync(obj);
            await CurrentUnitOfWork.SaveChangesAsync();

            return input;
        }

        public async Task<List<ConcretFieldDto>> GetByRequesId(long testId, long requestId)
        {
            var obje = _concretFieldRepository.GetAll().Where(x => x.RequestInspectionTestId == testId && x.RequestId == requestId).ToList();

            var obj = _mapper.Map<List<ConcretFieldDto>>(obje);
            return obj;
        }

        public async Task<ConcretFieldDto> GetByRequestandTest(long testId, long requestId)
        {
            var obje = await _concretFieldRepository.FirstOrDefaultAsync(x => x.RequestInspectionTestId == testId && x.RequestId == requestId);
            if (obje == null)
            {
                return new ConcretFieldDto();
            }
            var obj = _mapper.Map<ConcretFieldDto>(obje);
            return obj;
        }
    }
}
