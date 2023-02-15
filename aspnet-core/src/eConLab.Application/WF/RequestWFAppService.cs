﻿using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using AutoMapper;
using eConLab.Account;
using eConLab.Authorization.Roles;
using eConLab.Authorization;
using eConLab.Proj.Dto;
using eConLab.Proj;
using eConLab.ProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eConLab.WF.Dto;

namespace eConLab.WF
{

    [AbpAuthorize]
    public class RequestWFAppService :
        eConLabAppServiceBase,
       IRequestWFAppService
    {
        private readonly IRepository<Agency, long> _agencyRepository;
        private readonly IRepository<AgencyType, long> _agencyTypeRepo;
        private readonly IRepository<QCUser, long> _qcUserRepo;
        private readonly IRepository<Project, long> _projectRepo;
        private readonly IRepository<ProjectItem, long> _projectItemRepo;
        private readonly IRepository<RequestWF, long> _requestWFRepo;
        private readonly IRepository<RequestWFHistory, long> _requestWFHistoryRepo;
        private readonly IMapper _mapper;
        public RequestWFAppService(
            IMapper mapper,
            IRepository<Agency, long> agencyRepository,
            IRepository<AgencyType, long> agencyTypeRepo,
            IRepository<QCUser, long> qcUserRepo,
            IRepository<Project, long> projectRepo,
            IRepository<ProjectItem, long> projectItemRepo,
            IRepository<RequestWF, long> requestWFRepo,
            IRepository<RequestWFHistory, long> requestWFHistoryRepo)

        {
            _mapper = mapper;
            _agencyRepository = agencyRepository;
            _agencyTypeRepo = agencyTypeRepo;
            _qcUserRepo = qcUserRepo;
            _projectRepo = projectRepo;
            _projectItemRepo = projectItemRepo;
            _requestWFRepo = requestWFRepo;
            _requestWFHistoryRepo = requestWFHistoryRepo;
        }

       
        public async Task<RequestWFDto> CreateOrUpdate(RequestWFDto input)
        {

            await _requestWFRepo.InsertOrUpdateAsync(_mapper.Map<RequestWF>(input));
            await CurrentUnitOfWork.SaveChangesAsync();


            return _mapper.Map<RequestWFDto>(input);
        }




        public async Task<RequestWFDto> Get(long id)
        {
            var obje = _requestWFRepo.FirstOrDefault(x => x.Id == id);
            return _mapper.Map<RequestWFDto>(obje) ?? null;
        }


        public async Task<PagedResultDto<RequestWFDto>> GetAll(RequestWFPaginatedDto input)
        {
            var filter = ObjectMapper.Map<RequestWFPaginatedDto>(input);

            var lstItems = await GetListAsync(input.SkipCount, input.MaxResultCount);
            var totalCount = await GetTotalCountAsync();

            return new PagedResultDto<RequestWFDto>(totalCount, ObjectMapper.Map<List<RequestWFDto>>(lstItems));
        }


        private async Task<List<RequestWF>> GetListAsync(int skipCount, int maxResultCount, RequestWFFilter filter = null)
        {

            var lstItems = _requestWFRepo.GetAll()
                                          .Skip(skipCount)
                                          .Take(maxResultCount);


           


            return lstItems.ToList();
        }

        private async Task<int> GetTotalCountAsync(RequestWFFilter filter = null)
        {

            var lstItems = _requestWFRepo.GetAll();


            return lstItems.ToList().Count;
        }
    }
}
