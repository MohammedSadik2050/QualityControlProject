using Abp.Application.Services.Dto;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eConLab.Agencies.Dto;
using eConLab.Account;
using eConLab.QCUsers.Dto;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Abp.UI;

namespace eConLab.Agencies
{

    public class AgencyAppService :
         eConLabAppServiceBase,
        IAgencyAppService
    {
        private readonly IRepository<Agency,long> _agencyRepository;
        private readonly IRepository<AgencyType,long> _agencyTypeRepo;
        private readonly IMapper _mapper;
        public AgencyAppService(IMapper mapper,IRepository<Agency, long> agencyRepository, IRepository<AgencyType, long> agencyTypeRepo)
          
        {
            _mapper = mapper;
            _agencyRepository = agencyRepository;
            _agencyTypeRepo = agencyTypeRepo;
        }

        public async Task<AgencyDto> CreateOrUpdate(CreateUpdateAgencyDto input)
        {
            if (input.Id == default(int))
            {
                await _agencyRepository.InsertAsync(_mapper.Map<Agency>(input));
                await CurrentUnitOfWork.SaveChangesAsync();
               
            }
            else
            {
                var obje = await _agencyRepository.FirstOrDefaultAsync(x => x.Id == input.Id);
                if (obje != null)
                {
                    await _agencyRepository.UpdateAsync(_mapper.Map<Agency>(input));
                    await CurrentUnitOfWork.SaveChangesAsync();
                    
                }
            }

            return _mapper.Map<AgencyDto>(input);
        }


        public async Task<AgencyDto> Get(long id)
        {
            var obje = await _agencyRepository.FirstOrDefaultAsync(x => x.Id == id);

            var obj = _mapper.Map<AgencyDto>(obje);
            return obj;
        }


        public async Task<PagedResultDto<AgencyDto>> GetAll(AgencyPaginatedDto input)
        {
            var filter = ObjectMapper.Map<QCUserPagedAndSortedResultRequestDto>(input);

            // var sorting = (string.IsNullOrEmpty(input.Sorting) ? "Name DESC" : input.Sorting).Replace("ShortName", "Name");

            var lstItems = await GetListAsync(input.SkipCount, input.MaxResultCount);
            var totalCount = await GetTotalCountAsync();

            return new PagedResultDto<AgencyDto>(totalCount, ObjectMapper.Map<List<AgencyDto>>(lstItems));
        }


        private async Task<List<Agency>> GetListAsync(int skipCount, int maxResultCount, AgencyFilter filter = null)
        {

            var lstItems = await _agencyRepository.GetAll()
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
            //.WhereIf(!filter.Id.IsNullOrWhiteSpace(), x => x.Id.ToString().Contains(filter.Id))
            //.WhereIf(!filter.Name.IsNullOrWhiteSpace(), x => x.Name.Contains(filter.Name))
            //.WhereIf(!filter.Price.IsNullOrWhiteSpace(), x => x.Price.ToString().Contains(filter.Price))
            //.WhereIf(!filter.PublishDate.IsNullOrWhiteSpace(), x => x.PublishDate.ToString().Contains(filter.PublishDate))

            return lstItems;
        }

        private async Task<int> GetTotalCountAsync(QCUserFilter filter = null)
        {

            var lstItems = await _agencyRepository.GetAll()
                //.WhereIf(!filter.Id.IsNullOrWhiteSpace(), x => x.Id.ToString().Contains(filter.Id))
                //.WhereIf(!filter.Name.IsNullOrWhiteSpace(), x => x.Name.Contains(filter.Name))
                //.WhereIf(!filter.Price.IsNullOrWhiteSpace(), x => x.Price.ToString().Contains(filter.Price))
                //.WhereIf(!filter.PublishDate.IsNullOrWhiteSpace(), x => x.PublishDate.ToString().Contains(filter.PublishDate))
                .ToListAsync();
            return lstItems.Count;
        }


        public async Task<List<AgencyDto>> GetAllAgenciesList()
        {
            var query = _agencyRepository.GetAll().ToList();
            return _mapper.Map<List<AgencyDto>>(query);
        }

        public async Task<List<AgencyTypeDto>> GetAllAgencyTypeList()
        {
            var query = _agencyTypeRepo.GetAll().ToList();
            return _mapper.Map<List<AgencyTypeDto>>(query);
        }
    }
}
