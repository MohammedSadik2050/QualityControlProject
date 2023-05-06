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
    public class AsphaltFieldAppService :
        eConLabAppServiceBase, IAsphaltFieldAppService
    {
        private readonly IRepository<eConLab.TestModels.AsphaltField, long> _asphaltFieldRepository;
        private readonly IMapper _mapper;
        public AsphaltFieldAppService(IMapper mapper,
          IRepository<eConLab.TestModels.AsphaltField, long> asphaltFieldRepository
          )

        {
            _mapper = mapper;
            _asphaltFieldRepository = asphaltFieldRepository;
        }
        public async Task<AsphaltFieldDto> CreateOrUpdate(AsphaltFieldDto input)
        {
            var queryable = _asphaltFieldRepository.GetAll().AsQueryable();
            var existRc = await queryable.AsNoTracking().FirstOrDefaultAsync(x => x.RequestInspectionTestId == input.RequestInspectionTestId
                                                                    && x.RequestId == input.RequestId);
            if (existRc != null) { input.Id = existRc.Id; };
            var obj = _mapper.Map<eConLab.TestModels.AsphaltField>(input);

            await _asphaltFieldRepository.InsertOrUpdateAsync(obj);
            await CurrentUnitOfWork.SaveChangesAsync();

            return input;
        }

        public async Task<List<AsphaltFieldDto>> GetByRequesId(long testId, long requestId)
        {
            var obje = _asphaltFieldRepository.GetAll().Where(x => x.RequestInspectionTestId == testId && x.RequestId == requestId).ToList();

            var obj = _mapper.Map<List<AsphaltFieldDto>>(obje);
            return obj;
        }

        public async Task<AsphaltFieldDto> GetByRequestandTest(long testId, long requestId)
        {
            var obje = await _asphaltFieldRepository.FirstOrDefaultAsync(x => x.RequestInspectionTestId == testId && x.RequestId == requestId);
            if (obje == null)
            {
                return new AsphaltFieldDto();
            }
            var obj = _mapper.Map<AsphaltFieldDto>(obje);
            return obj;
        }
    }
}
