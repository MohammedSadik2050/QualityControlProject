using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using AutoMapper;
using eConLab.Account;
using eConLab.Agencies.Dto;
using eConLab.Agencies;
using eConLab.QCUsers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eConLab.ProjectModels;
using eConLab.Proj.Dto;
using Microsoft.EntityFrameworkCore;
using Abp.Collections.Extensions;
using eConLab.Authorization.Roles;
using Microsoft.AspNetCore.Authorization;
using Abp.Authorization;
using eConLab.Authorization;
using Abp.Extensions;

namespace eConLab.Proj
{
    public class ProjectAppService :
        eConLabAppServiceBase,
       IProjectAppService
    {
        private readonly IRepository<Agency, long> _agencyRepository;
        private readonly IRepository<AgencyType, long> _agencyTypeRepo;
        private readonly IRepository<QCUser, long> _qcUserRepo;
        private readonly IRepository<Project, long> _projectRepo;
        private readonly IRepository<ProjectItem, long> _projectItemRepo;
        private readonly IMapper _mapper;
        public ProjectAppService(
            IMapper mapper,
            IRepository<Agency, long> agencyRepository,
            IRepository<AgencyType, long> agencyTypeRepo,
            IRepository<QCUser, long> qcUserRepo,
            IRepository<Project, long> projectRepo,
            IRepository<ProjectItem, long> projectItemRepo)

        {
            _mapper = mapper;
            _agencyRepository = agencyRepository;
            _agencyTypeRepo = agencyTypeRepo;
            _qcUserRepo = qcUserRepo;
            _projectRepo = projectRepo;
            _projectItemRepo = projectItemRepo;
        }

        [AbpAuthorize(PermissionNames.Pages_Manage_Project)]
        public async Task<ProjectDto> CreateOrUpdate(ProjectDto input)
        {

            await _projectRepo.InsertOrUpdateAsync(_mapper.Map<Project>(input));
            await CurrentUnitOfWork.SaveChangesAsync();


            return _mapper.Map<ProjectDto>(input);
        }




        public async Task<ProjectDto> Get(long id)
        {
            var obje = _projectRepo.FirstOrDefault(x => x.Id == id);
            return _mapper.Map<ProjectDto>(obje) ?? null;
        }


        public async Task<PagedResultDto<ProjectDto>> GetAll(ProjectPaginatedDto input)
        {
            var filter = ObjectMapper.Map<ProjectPaginatedDto>(input);

            var lstItems = await GetListAsync(input.SkipCount, input.MaxResultCount, filter);
            var totalCount = await GetTotalCountAsync(input);

            return new PagedResultDto<ProjectDto>(totalCount, ObjectMapper.Map<List<ProjectDto>>(lstItems));
        }


        private async Task<List<Project>> GetListAsync(int skipCount, int maxResultCount, ProjectPaginatedDto filter = null)
        {

            var lstItems = _projectRepo.GetAll()
                                          .Skip(skipCount)
                                          .Take(maxResultCount)
                                          .WhereIf(!filter.Search.IsNullOrEmpty(), x => x.Name.Contains(filter.Search))
                                            .WhereIf(filter.AgencyId > 0, x => x.AgencyId == filter.AgencyId)
                                             .WhereIf(filter.AgencyTypeId > 0, x => x.AgencyTypeId == filter.AgencyTypeId);


            //check user Role 
            var userRoles = (await UserManager.GetRolesAsync(await GetCurrentUserAsync())).ToList();
            if (userRoles.Count > 0 && userRoles.Contains(StaticRoleNames.Tenants.Admin) == false)
            {
                if (userRoles.Contains(StaticRoleNames.Tenants.Consultant))
                    lstItems.Where(d => d.ConsultantId == AbpSession.UserId);

                if (userRoles.Contains(StaticRoleNames.Tenants.Contractor))
                    lstItems.Where(d => d.ContractorId == AbpSession.UserId);

                if (userRoles.Contains(StaticRoleNames.Tenants.LabProjectManager))
                    lstItems.Where(d => d.LabProjectManagerId == AbpSession.UserId);

                if (userRoles.Contains(StaticRoleNames.Tenants.SupervisingQuality))
                    lstItems.Where(d => d.SupervisingQualityId == AbpSession.UserId);

                if (userRoles.Contains(StaticRoleNames.Tenants.SupervisingEngineer))
                    lstItems.Where(d => d.SupervisingEngineerId == AbpSession.UserId);
            }


            return lstItems.ToList();
        }

        private async Task<int> GetTotalCountAsync(ProjectPaginatedDto filter = null)
        {

            var lstItems = _projectRepo.GetAll()
                             .WhereIf(!filter.Search.IsNullOrEmpty(), x => x.Name.Contains(filter.Search))
                             .WhereIf(filter.AgencyId > 0, x => x.AgencyId == filter.AgencyId)
                              .WhereIf(filter.AgencyTypeId > 0, x => x.AgencyTypeId == filter.AgencyTypeId);

            //check user Role 
            var userRoles = (await UserManager.GetRolesAsync(await GetCurrentUserAsync())).ToList();
            if (userRoles.Count > 0 && userRoles.Contains(StaticRoleNames.Tenants.Admin) == false)
            {
                if (userRoles.Contains(StaticRoleNames.Tenants.Consultant))
                    lstItems.Where(d => d.ConsultantId == AbpSession.UserId);

                if (userRoles.Contains(StaticRoleNames.Tenants.Contractor))
                    lstItems.Where(d => d.ContractorId == AbpSession.UserId);

                if (userRoles.Contains(StaticRoleNames.Tenants.LabProjectManager))
                    lstItems.Where(d => d.LabProjectManagerId == AbpSession.UserId);

                if (userRoles.Contains(StaticRoleNames.Tenants.SupervisingQuality))
                    lstItems.Where(d => d.SupervisingQualityId == AbpSession.UserId);

                if (userRoles.Contains(StaticRoleNames.Tenants.SupervisingEngineer))
                    lstItems.Where(d => d.SupervisingEngineerId == AbpSession.UserId);
            }

            return lstItems.ToList().Count;
        }


        #region ProjectItems
        public async Task<ProjectItemDto> CreateOrUpdateProjectItem(ProjectItemDto input)
        {

            await _projectItemRepo.InsertOrUpdateAsync(_mapper.Map<ProjectItem>(input));
            await CurrentUnitOfWork.SaveChangesAsync();


            return _mapper.Map<ProjectItemDto>(input);
        }

        public async Task<List<ProjectItemDto>> GetProjectItemsByProjectId(int projectId)
        {

            var query = await _projectItemRepo.GetAll().Where(d => d.ProjectId == projectId).ToListAsync();
            return _mapper.Map<List<ProjectItemDto>>(query);
        }

        public async Task<ProjectItemDto> GetProjectItem(long id)
        {
            var obj = _projectItemRepo.FirstOrDefault(x => x.Id == id);

            return _mapper.Map<ProjectItemDto>(obj) ?? null;
        }



        public async Task<bool> DeleteProjectItem(long Id)
        {
            await _projectItemRepo.DeleteAsync(Id);
            return true;
        }


        #endregion
    }
}
