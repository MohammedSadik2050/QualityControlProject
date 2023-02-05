﻿using Abp.Application.Services.Dto;
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
using Abp.Collections.Extensions;
using Abp.Extensions;

namespace eConLab.Agencies
{

    public class AgencyAppService :
         eConLabAppServiceBase,
        IAgencyAppService
    {
        private readonly IRepository<Agency, long> _agencyRepository;
        private readonly IRepository<AgencyType, long> _agencyTypeRepo;
        private readonly IMapper _mapper;
        public AgencyAppService(IMapper mapper, IRepository<Agency, long> agencyRepository, IRepository<AgencyType, long> agencyTypeRepo)

        {
            _mapper = mapper;
            _agencyRepository = agencyRepository;
            _agencyTypeRepo = agencyTypeRepo;
        }

        public async Task<AgencyDto> CreateOrUpdate(CreateUpdateAgencyDto input)
        {
            await _agencyRepository.InsertOrUpdateAsync(_mapper.Map<Agency>(input));
            await CurrentUnitOfWork.SaveChangesAsync();

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

            var lstItems = await GetListAsync(input.SkipCount, input.MaxResultCount, filter);
            var totalCount = await GetTotalCountAsync(filter);

            return new PagedResultDto<AgencyDto>(totalCount, ObjectMapper.Map<List<AgencyDto>>(lstItems));
        }


        private async Task<List<Agency>> GetListAsync(int skipCount, int maxResultCount, QCUserPagedAndSortedResultRequestDto filter = null)
        {

            var lstItems = _agencyRepository.GetAll()
                .Skip(skipCount)
                .Take(maxResultCount)
                .WhereIf(!filter.Search.IsNullOrEmpty(), x => x.Name.Contains(filter.Search))
            .WhereIf(!filter.Search.IsNullOrWhiteSpace(), x => x.ResponsiblePerson.Contains(filter.Search))
             .WhereIf(!filter.Search.IsNullOrWhiteSpace(), x => x.PhoneNumber.Contains(filter.Search));
            //.WhereIf(!filter.Price.IsNullOrWhiteSpace(), x => x.Price.ToString().Contains(filter.Price))
            //.WhereIf(!filter.PublishDate.IsNullOrWhiteSpace(), x => x.PublishDate.ToString().Contains(filter.PublishDate))

            return lstItems.ToList();
        }

        private async Task<int> GetTotalCountAsync(QCUserPagedAndSortedResultRequestDto filter = null)
        {

            var lstItems =  _agencyRepository.GetAll()
                         .WhereIf(!filter.Search.IsNullOrEmpty(), x => x.Name.Contains(filter.Search))
                         .WhereIf(!filter.Search.IsNullOrWhiteSpace(), x => x.ResponsiblePerson.Contains(filter.Search))
                         .WhereIf(!filter.Search.IsNullOrWhiteSpace(), x => x.PhoneNumber.Contains(filter.Search));
                //.WhereIf(!filter.Id.IsNullOrWhiteSpace(), x => x.Id.ToString().Contains(filter.Id))
                //.WhereIf(!filter.Name.IsNullOrWhiteSpace(), x => x.Name.Contains(filter.Name))
                //.WhereIf(!filter.Price.IsNullOrWhiteSpace(), x => x.Price.ToString().Contains(filter.Price))
                //.WhereIf(!filter.PublishDate.IsNullOrWhiteSpace(), x => x.PublishDate.ToString().Contains(filter.PublishDate))
               
            return lstItems.Count();
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

        public async Task<bool> Delete(long Id)
        {
            await _agencyRepository.DeleteAsync(Id);
            return true;
        }
    }
}
