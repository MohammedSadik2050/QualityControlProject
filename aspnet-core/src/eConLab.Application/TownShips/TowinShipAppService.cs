using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using AutoMapper;
using eConLab.Account;
using eConLab.Agencies.Dto;
using eConLab.QCUsers.Dto;
using eConLab.TestModels;
using eConLab.TownShips.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.TownShips
{
    public class TowinShipAppService :
        eConLabAppServiceBase, ITownShipAppService
    {
        private readonly IRepository<TownShip, long> _towinShipRepository;
        private readonly IMapper _mapper;
        public TowinShipAppService(IMapper mapper,
          IRepository<TownShip, long> towinShipRepository
          )

        {
            _mapper = mapper;
            _towinShipRepository = towinShipRepository;
        }

        public async Task<TownShipDto> CreateOrUpdate(CreateUpdateTownShipDto input)
        {
            await _towinShipRepository.InsertOrUpdateAsync(_mapper.Map<TownShip>(input));
            await CurrentUnitOfWork.SaveChangesAsync();

            return _mapper.Map<TownShipDto>(input);
        }

        public async Task<bool> Delete(long Id)
        {
            await _towinShipRepository.DeleteAsync(Id);
            return true;
        }

        public async Task<TownShipDto> Get(long id)
        {
            var obje = await _towinShipRepository.FirstOrDefaultAsync(x => x.Id == id);

            var obj = _mapper.Map<TownShipDto>(obje);
            return obj;
        }

        public async Task<PagedResultDto<TownShipDto>> GetAll(TownShipPaginatedDto input)
        {
            var lstItems = await GetListAsync(input.SkipCount, input.MaxResultCount, input);
            var totalCount = await GetTotalCountAsync(input);

            return new PagedResultDto<TownShipDto>(totalCount, ObjectMapper.Map<List<TownShipDto>>(lstItems));
        }

        private async Task<List<TownShip>> GetListAsync(int skipCount, int maxResultCount, TownShipPaginatedDto filter = null)
        {

            var lstItems = _towinShipRepository.GetAll()
                .Skip(skipCount)
                .Take(maxResultCount)
                .WhereIf(!filter.Search.IsNullOrEmpty(), x => x.Name.Contains(filter.Search))
                .WhereIf(!filter.Search.IsNullOrEmpty(), x => x.Address.Contains(filter.Search));

            return lstItems.ToList();
        }

        private async Task<int> GetTotalCountAsync(TownShipPaginatedDto filter = null)
        {

            var lstItems = _towinShipRepository.GetAll()
                         .WhereIf(!filter.Search.IsNullOrEmpty(), x => x.Name.Contains(filter.Search))
                         .WhereIf(!filter.Search.IsNullOrEmpty(), x => x.Address.Contains(filter.Search));
            return lstItems.Count();
        }

        public async Task<List<TownShipDto>> GetAllownShipList()
        {
            var allTowinShip = await _towinShipRepository.GetAllListAsync();
            return _mapper.Map<List<TownShipDto>>(allTowinShip);
        }
    }
}
