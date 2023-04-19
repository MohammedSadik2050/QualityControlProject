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
using Abp.Collections.Extensions;
using eConLab.Enum;
using Abp.Extensions;
using Abp.Runtime.Session;
using eConLab.WF;
using eConLab.WF.Dto;
using eConLab.Observers;

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
        private readonly IRepository<Observer, long> _observerRepo;
        private readonly IMapper _mapper;
        private readonly IRequestWFAppService _requestWFAppService;
        public RequestAppService(
            IMapper mapper,
            IRepository<Observer, long> observerRepo,
             IRequestWFAppService requestWFAppService,
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
            _requestRepo = requestRepo;
            _requestWFAppService = requestWFAppService;
            _observerRepo = observerRepo;
        }


        public async Task<RequestDto> CreateOrUpdate(RequestDto input)
        {
            if (string.IsNullOrEmpty(input.Code))
            {
                long max = 0;
                try
                {
                    max = _requestRepo.GetAll().Max(d => d.Id);
                }
                catch
                {
                }
                input.Code = "R-" + input.MainRequestType + "-C-" + (max + 1);
            }

            input.Name = input.MainRequestType.ToString();
            var res = await _requestRepo.InsertOrUpdateAsync(_mapper.Map<Request>(input));
            await CurrentUnitOfWork.SaveChangesAsync();
            input.Id = res.Id;
            return _mapper.Map<RequestDto>(input);
        }
        public async Task<RequestDto> Get(long id)
        {
            var obj = _requestRepo.FirstOrDefault(x => x.Id == id);
            var result = _mapper.Map<RequestDto>(obj);
            result.StatusName = obj.Status.ToString();
            return result ?? null;
        }


        public async Task<PagedResultDto<RequestViewDto>> GetAll(RequestPaginatedDto input)
        {
            var filter = ObjectMapper.Map<RequestPaginatedDto>(input);

            var lstItems = await GetListAsync(input.SkipCount, input.MaxResultCount, input);
            var totalCount = await GetTotalCountAsync(input);

            return new PagedResultDto<RequestViewDto>(totalCount, ObjectMapper.Map<List<RequestViewDto>>(lstItems));
        }


        private async Task<List<RequestViewDto>> GetListAsync(int skipCount, int maxResultCount, RequestPaginatedDto filter = null)
        {

            var lstItems = _requestRepo.GetAll().Include(s=>s.TownShip).Include(s => s.Project).Include(s => s.Observer).OrderByDescending(s => s.CreationTime)

                                           .WhereIf(filter.ProjectId > 0, x => x.ProjectId == filter.ProjectId)
                                           .WhereIf(filter.TownShipId > 0, x => x.TownShipId == filter.TownShipId)
                                         .WhereIf(!filter.ContractNumber.IsNullOrEmpty(), x => x.Project.ContractNumber.Contains(filter.ContractNumber))
                                          .WhereIf(!filter.RequestCode.IsNullOrEmpty(), x => x.Code.Contains(filter.RequestCode))
                                          .WhereIf(filter.Status > 0, x => (int)x.Status == filter.Status).AsQueryable();


            var userRoles = (await UserManager.GetRolesAsync(await GetCurrentUserAsync())).ToList();
            if (userRoles.Count > 0 && userRoles.Contains(StaticRoleNames.Tenants.Admin) == false)
            {
                if (userRoles.Contains(StaticRoleNames.Tenants.Consultant))
                    lstItems = lstItems.Where(d => d.Project.ConsultantId == AbpSession.UserId);
                if (userRoles.Contains(StaticRoleNames.Tenants.Contractor))
                    lstItems = lstItems.Where(d => d.Project.ContractorId == AbpSession.UserId);

                if (userRoles.Contains(StaticRoleNames.Tenants.Observer))
                    lstItems = lstItems.Where(d => d.Observer.UserId == AbpSession.UserId);
                //if (userRoles.Contains(StaticRoleNames.Tenants.LabProjectManager))
                //    lstItems= lstItems.Where(d => d.Project.LabProjectManagerId == AbpSession.UserId);

                if (userRoles.Contains(StaticRoleNames.Tenants.SupervisingQuality))
                    lstItems = lstItems.Where(d => d.Project.SupervisingQualityId == AbpSession.UserId);

                if (userRoles.Contains(StaticRoleNames.Tenants.SupervisingEngineer))
                    lstItems = lstItems.Where(d => d.Project.SupervisingEngineerId == AbpSession.UserId);
            }
            lstItems = lstItems.Skip(skipCount)
                                          .Take(maxResultCount);

            var result = lstItems.Select(mod => new RequestViewDto
            {
                Id = mod.Id,
                Code = mod.Code,
                InspectionDate = mod.InspectionDate,
                Description = mod.Description,
                TownShipName = mod.TownShip ==null?"": mod.TownShip.Name,
                ProjectId = mod.ProjectId,
                DistrictName = mod.DistrictName,
                PhomeNumberSiteResponsibleOne = mod.PhomeNumberSiteResponsibleOne,
                PhomeNumberSiteResponsibleTwo = mod.PhomeNumberSiteResponsibleTwo,
                MainRequestType = mod.MainRequestType,
                HasSample = mod.HasSample,
                Status = mod.Status.ToString(),
                Project = new ProjectDto
                {
                    Id = mod.Project.Id,
                    Name = mod.Project.Name,
                    ContractNumber = mod.Project.ContractNumber,
                    StartDate = mod.Project.StartDate,
                    SiteDelivedDate = mod.Project.SiteDelivedDate,
                },

                ObserverId = mod.ObserverId,
                ObserverName = mod.Observer != null ? mod.Observer.Name : "",

            }).ToList();
            return result;
        }

        private async Task<int> GetTotalCountAsync(RequestPaginatedDto filter = null)
        {

            var lstItems = _requestRepo.GetAll().Include(s => s.Project).OrderByDescending(s => s.CreationTime)
                                        .WhereIf(filter.TownShipId > 0, x => x.TownShipId == filter.TownShipId)
                                           .WhereIf(filter.ProjectId > 0, x => x.ProjectId == filter.ProjectId)
                                         .WhereIf(!filter.ContractNumber.IsNullOrEmpty(), x => x.Project.ContractNumber.Contains(filter.ContractNumber))
                                          .WhereIf(!filter.RequestCode.IsNullOrEmpty(), x => x.Code.Contains(filter.RequestCode))
                                          .WhereIf(filter.Status > 0, x => (int)x.Status == filter.Status);

            var userRoles = (await UserManager.GetRolesAsync(await GetCurrentUserAsync())).ToList();
            if (userRoles.Count > 0 && userRoles.Contains(StaticRoleNames.Tenants.Admin) == false)
            {
                if (userRoles.Contains(StaticRoleNames.Tenants.Consultant))
                    lstItems = lstItems.Where(d => d.Project.ConsultantId == AbpSession.UserId && d.Status == RequestStatus.Submitted);
                if (userRoles.Contains(StaticRoleNames.Tenants.Contractor))
                    lstItems = lstItems.Where(d => d.Project.CreatorUserId == AbpSession.UserId);

                //if (userRoles.Contains(StaticRoleNames.Tenants.LabProjectManager))
                //    lstItems= lstItems.Where(d => d.Project.LabProjectManagerId == AbpSession.UserId);

                if (userRoles.Contains(StaticRoleNames.Tenants.SupervisingQuality))
                    lstItems = lstItems.Where(d => d.Project.SupervisingQualityId == AbpSession.UserId);

                if (userRoles.Contains(StaticRoleNames.Tenants.SupervisingEngineer))
                    lstItems = lstItems.Where(d => d.Project.SupervisingEngineerId == AbpSession.UserId);

                if (userRoles.Contains(StaticRoleNames.Tenants.Observer))
                    lstItems = lstItems.Where(d => d.Observer.UserId == AbpSession.UserId);
            }

            return lstItems.ToList().Count;
        }


        public async Task<PagedResultDto<RequestViewDto>> GetAllForAssign(RequestPaginatedDto input)
        {
            var filter = ObjectMapper.Map<RequestPaginatedDto>(input);

            var lstItems = await GetListForAssignAsync(input.SkipCount, input.MaxResultCount, input);
            var totalCount = await GetTotalCountForAssignAsync(input);

            return new PagedResultDto<RequestViewDto>(totalCount, ObjectMapper.Map<List<RequestViewDto>>(lstItems));
        }


        private async Task<List<RequestViewDto>> GetListForAssignAsync(int skipCount, int maxResultCount, RequestPaginatedDto filter = null)
        {

            var lstItems = _requestRepo.GetAll().Include(s=>s.TownShip)
                .Include(s => s.Project).Include(s => s.Observer).OrderByDescending(s => s.CreationTime)

                                           .WhereIf(filter.ProjectId > 0, x => x.ProjectId == filter.ProjectId)
                                           .WhereIf(filter.TownShipId > 0, x => x.TownShipId == filter.TownShipId)
                                         .WhereIf(!filter.ContractNumber.IsNullOrEmpty(), x => x.Project.ContractNumber.Contains(filter.ContractNumber))
                                          .WhereIf(!filter.RequestCode.IsNullOrEmpty(), x => x.Code.Contains(filter.RequestCode))
                                          .WhereIf(filter.Status > 0, x => (int)x.Status == filter.Status)
                                          .WhereIf(filter.Status < 1, x => x.Status == RequestStatus.ApprovedByLabProjectManager ||
                                                  x.Status == RequestStatus.AssignedToObserver).AsQueryable();


            var userRoles = (await UserManager.GetRolesAsync(await GetCurrentUserAsync())).ToList();
            if (userRoles.Count > 0 && userRoles.Contains(StaticRoleNames.Tenants.Admin) == false)
            {
                if (userRoles.Contains(StaticRoleNames.Tenants.Consultant))
                    lstItems = lstItems.Where(d => d.Project.ConsultantId == AbpSession.UserId);
                if (userRoles.Contains(StaticRoleNames.Tenants.Contractor))
                    lstItems = lstItems.Where(d => d.Project.ContractorId == AbpSession.UserId);

                if (userRoles.Contains(StaticRoleNames.Tenants.Observer))
                    lstItems = lstItems.Where(d => d.Observer.UserId == AbpSession.UserId);
                //if (userRoles.Contains(StaticRoleNames.Tenants.LabProjectManager))
                //    lstItems= lstItems.Where(d => d.Project.LabProjectManagerId == AbpSession.UserId);

                if (userRoles.Contains(StaticRoleNames.Tenants.SupervisingQuality))
                    lstItems = lstItems.Where(d => d.Project.SupervisingQualityId == AbpSession.UserId);

                if (userRoles.Contains(StaticRoleNames.Tenants.SupervisingEngineer))
                    lstItems = lstItems.Where(d => d.Project.SupervisingEngineerId == AbpSession.UserId);
            }
            lstItems = lstItems.Skip(skipCount)
                                          .Take(maxResultCount);

            var result = lstItems.Select(mod => new RequestViewDto
            {
                Id = mod.Id,
                Code = mod.Code,
                InspectionDate = mod.InspectionDate,
                Description = mod.Description,
                TownShipName = mod.TownShip.Name,
                ProjectId = mod.ProjectId,
                DistrictName = mod.DistrictName,
                PhomeNumberSiteResponsibleOne = mod.PhomeNumberSiteResponsibleOne,
                PhomeNumberSiteResponsibleTwo = mod.PhomeNumberSiteResponsibleTwo,
                MainRequestType = mod.MainRequestType,
                HasSample = mod.HasSample,
                Status = mod.Status.ToString(),
                Project = new ProjectDto
                {
                    Id = mod.Project.Id,
                    Name = mod.Project.Name,
                    ContractNumber = mod.Project.ContractNumber,
                    StartDate = mod.Project.StartDate,
                    SiteDelivedDate = mod.Project.SiteDelivedDate,
                },

                ObserverId = mod.ObserverId,
                ObserverName = mod.Observer != null ? mod.Observer.Name : "",

            }).ToList();
            return result;
        }

        private async Task<int> GetTotalCountForAssignAsync(RequestPaginatedDto filter = null)
        {

            var lstItems = _requestRepo.GetAll().Include(s => s.Project).OrderByDescending(s => s.CreationTime)
                                           .WhereIf(filter.ProjectId > 0, x => x.ProjectId == filter.ProjectId)
                                              .WhereIf(filter.TownShipId > 0, x => x.TownShipId == filter.TownShipId)
                                         .WhereIf(!filter.ContractNumber.IsNullOrEmpty(), x => x.Project.ContractNumber.Contains(filter.ContractNumber))
                                          .WhereIf(!filter.RequestCode.IsNullOrEmpty(), x => x.Code.Contains(filter.RequestCode))
                                          .WhereIf(filter.Status > 0, x => (int)x.Status == filter.Status)
                                              .WhereIf(filter.Status < 1, x => x.Status == RequestStatus.ApprovedByLabProjectManager ||
                                                  x.Status == RequestStatus.AssignedToObserver);

            var userRoles = (await UserManager.GetRolesAsync(await GetCurrentUserAsync())).ToList();
            if (userRoles.Count > 0 && userRoles.Contains(StaticRoleNames.Tenants.Admin) == false)
            {
                if (userRoles.Contains(StaticRoleNames.Tenants.Consultant))
                    lstItems = lstItems.Where(d => d.Project.ConsultantId == AbpSession.UserId && d.Status == RequestStatus.Submitted);
                if (userRoles.Contains(StaticRoleNames.Tenants.Contractor))
                    lstItems = lstItems.Where(d => d.Project.CreatorUserId == AbpSession.UserId);

                //if (userRoles.Contains(StaticRoleNames.Tenants.LabProjectManager))
                //    lstItems= lstItems.Where(d => d.Project.LabProjectManagerId == AbpSession.UserId);

                if (userRoles.Contains(StaticRoleNames.Tenants.SupervisingQuality))
                    lstItems = lstItems.Where(d => d.Project.SupervisingQualityId == AbpSession.UserId);

                if (userRoles.Contains(StaticRoleNames.Tenants.SupervisingEngineer))
                    lstItems = lstItems.Where(d => d.Project.SupervisingEngineerId == AbpSession.UserId);

                if (userRoles.Contains(StaticRoleNames.Tenants.Observer))
                    lstItems = lstItems.Where(d => d.Observer.UserId == AbpSession.UserId);
            }

            return lstItems.ToList().Count;
        }

        public async Task<RequestViewDto> GetRequestView([Required] long id)
        {
            var obj = await _requestRepo.GetAllIncluding(d => d.Project).FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<RequestViewDto>(obj) ?? null;
        }

        public async Task<bool> AssignRequest(long requestId, long observerId)
        {
            var request = await _requestRepo.FirstOrDefaultAsync(s => s.Id == requestId);

            if (request != null)
            {
                if (request.ObserverId == observerId)
                {
                    return true;
                }
                request.ObserverId = observerId;
                request.Status = RequestStatus.AssignedToObserver;
            }
            var currentObserver = await _observerRepo.FirstOrDefaultAsync(s => s.Id == observerId);
            var history = new RequestWFDto()
            {
                ActionName = "تم تعيين مراقب",
                RequestId = requestId,
                Entity = Entities.Request,
                CurrentUserId = AbpSession.UserId.Value,
                ActionNotes = $@"تم تعيين {currentObserver.Name}"
            };
            await _requestWFAppService.CreateOrUpdate(history);
            return true;
        }
    }
}
