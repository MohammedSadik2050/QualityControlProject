using Abp.Application.Services.Dto;
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
using eConLab.Req;
using eConLab.Requests.Dto;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace eConLab.Requests
{

    [AbpAuthorize]
    public class RequestAppService :
      eConLabAppServiceBase,
     IRequestAppService
    {
        private readonly IRepository<Agency, long> _agencyRepository;
        private readonly IRepository<AgencyType, long> _agencyTypeRepo;
        private readonly IRepository<QCUser, long> _qcUserRepo;
        private readonly IRepository<Project, long> _projectRepo;
        private readonly IRepository<ProjectItem, long> _projectItemRepo;
        private readonly IRepository<Request, long> _requestRepo;
        private readonly IMapper _mapper;
        public RequestAppService(
            IMapper mapper,
            IRepository<Agency, long> agencyRepository,
            IRepository<AgencyType, long> agencyTypeRepo,
            IRepository<QCUser, long> qcUserRepo,
            IRepository<Project, long> projectRepo,
            IRepository<ProjectItem, long> projectItemRepo,
            IRepository<Request, long> requestRepo)

        {
            _mapper = mapper;
            _agencyRepository = agencyRepository;
            _agencyTypeRepo = agencyTypeRepo;
            _qcUserRepo = qcUserRepo;
            _projectRepo = projectRepo;
            _projectItemRepo = projectItemRepo;
            _requestRepo= requestRepo;
        }

       
        public async Task<RequestDto> CreateOrUpdate(RequestDto input)
        {
            var max= await _requestRepo.GetAll().MaxAsync(d => d.Id);
            input.Code = "R-"+input.MainRequestType + "-C-" + max + 1;
            await _requestRepo.InsertOrUpdateAsync(_mapper.Map<Request>(input));
            await CurrentUnitOfWork.SaveChangesAsync();
            return _mapper.Map<RequestDto>(input);
        }
        public async Task<RequestDto> Get(long id)
        {
            var obj = _requestRepo.FirstOrDefault(x => x.Id == id);
            return _mapper.Map<RequestDto>(obj) ?? null;
        }


        public async Task<PagedResultDto<RequestDto>> GetAll(RequestPaginatedDto input)
        {
            var filter = ObjectMapper.Map<RequestPaginatedDto>(input);

            var lstItems = await GetListAsync(input.SkipCount, input.MaxResultCount);
            var totalCount = await GetTotalCountAsync();

            return new PagedResultDto<RequestDto>(totalCount, ObjectMapper.Map<List<RequestDto>>(lstItems));
        }


        private async Task<List<Request>> GetListAsync(int skipCount, int maxResultCount, RequestFilter filter = null)
        {

            var lstItems = _requestRepo.GetAll()
                                          .Skip(skipCount)
                                          .Take(maxResultCount);

            return lstItems.ToList();
        }

        private async Task<int> GetTotalCountAsync(RequestFilter filter = null)
        {

            var lstItems = _projectRepo.GetAll();
            return lstItems.ToList().Count;
        }

        public async Task<RequestViewDto> GetRequestView([Required]long id)
        {
            var obj =await _requestRepo.GetAllIncluding(d => d.Project).FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<RequestViewDto>(obj) ?? null;
        }
    }
}
