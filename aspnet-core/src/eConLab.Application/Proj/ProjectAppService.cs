﻿using Abp.Application.Services.Dto;
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
using eConLab.Enum;

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

        //[AbpAuthorize(PermissionNames.Pages_Manage_Project)]
        public async Task<ProjectDto> CreateOrUpdate(ProjectDto input)
        {
            if (input.Status == ProjectStatus.ApprovedByLabProjectManager)
            {
                input.IsActive = true;
            }
            var resId = await _projectRepo.InsertOrUpdateAndGetIdAsync(_mapper.Map<Project>(input));
            await CurrentUnitOfWork.SaveChangesAsync();

            var result = _mapper.Map<ProjectDto>(input);
            result.Id = resId;
            return result;
        }




        public async Task<ProjectDto> Get(long id)
        {
            var obje = _projectRepo.FirstOrDefault(x => x.Id == id);

            var restult = _mapper.Map<ProjectDto>(obje);
            restult.StatusName = restult.Status.ToString();
            return restult ?? null;
        }


        public async Task<PagedResultDto<ProjectDto>> GetAll(ProjectPaginatedDto input)
        {
            var filter = ObjectMapper.Map<ProjectPaginatedDto>(input);

            var lstItems = await GetListAsync(input.SkipCount, input.MaxResultCount, filter);

            var totalCount = await GetTotalCountAsync(input);
            var result = ObjectMapper.Map<List<ProjectDto>>(lstItems);
            result.ForEach(m => { m.StatusName = m.Status.ToString(); });

            return new PagedResultDto<ProjectDto>(totalCount, result);
        }


        private async Task<List<Project>> GetListAsync(int skipCount, int maxResultCount, ProjectPaginatedDto filter = null)
        {

            var lstItems = _projectRepo.GetAll().OrderByDescending(s=>s.CreationTime)
                              .WhereIf(!filter.Search.IsNullOrEmpty(), x => x.Name.Contains(filter.Search))
                             .WhereIf(filter.AgencyId > 0, x => x.AgencyId == filter.AgencyId)
                             .WhereIf(filter.StatusId >= -1, x => x.Status == (ProjectStatus)filter.StatusId)
                              .WhereIf(filter.AgencyTypeId > 0, x => x.AgencyTypeId == filter.AgencyTypeId).AsQueryable();


            //check user Role 
            var userRoles = (await UserManager.GetRolesAsync(await GetCurrentUserAsync())).ToList();
            if (userRoles.Count > 0 && userRoles.Contains(StaticRoleNames.Tenants.Admin) == false)
            {
                if (userRoles.Contains(StaticRoleNames.Tenants.Consultant))
                    lstItems = lstItems.Where(d => d.ConsultantId == AbpSession.UserId);

                if (userRoles.Contains(StaticRoleNames.Tenants.Contractor))
                    lstItems = lstItems.Where(d => d.ContractorId == AbpSession.UserId);

                //if (userRoles.Contains(StaticRoleNames.Tenants.LabProjectManager))
                //    lstItems= lstItems.Where(d => d.LabProjectManagerId == AbpSession.UserId);

                if (userRoles.Contains(StaticRoleNames.Tenants.SupervisingQuality))
                    lstItems = lstItems.Where(d => d.SupervisingQualityId == AbpSession.UserId);

                if (userRoles.Contains(StaticRoleNames.Tenants.SupervisingEngineer))
                    lstItems = lstItems.Where(d => d.SupervisingEngineerId == AbpSession.UserId);
            }

            lstItems = lstItems.Skip(skipCount)
                               .Take(maxResultCount);

            return lstItems.ToList();
        }


        public async Task<List<ProjectDto>> GetAllDropdown()
        {

            var lstItems = _projectRepo.GetAll().Where(s => s.IsActive == true);

            var userRoles = (await UserManager.GetRolesAsync(await GetCurrentUserAsync())).ToList();
            if (userRoles.Count > 0 && userRoles.Contains(StaticRoleNames.Tenants.Admin) == false)
            {
                if (userRoles.Contains(StaticRoleNames.Tenants.Consultant))
                    lstItems = lstItems.Where(d => d.ConsultantId == AbpSession.UserId);

                if (userRoles.Contains(StaticRoleNames.Tenants.Contractor))
                    lstItems = lstItems.Where(d => d.ContractorId == AbpSession.UserId);

                //if (userRoles.Contains(StaticRoleNames.Tenants.LabProjectManager))
                //    lstItems = lstItems.Where(d => d.LabProjectManagerId == AbpSession.UserId);

                if (userRoles.Contains(StaticRoleNames.Tenants.SupervisingQuality))
                    lstItems = lstItems.Where(d => d.SupervisingQualityId == AbpSession.UserId);

                if (userRoles.Contains(StaticRoleNames.Tenants.SupervisingEngineer))
                    lstItems = lstItems.Where(d => d.SupervisingEngineerId == AbpSession.UserId);
            }

            var res = lstItems.ToList();
            return ObjectMapper.Map<List<ProjectDto>>(lstItems);


        }
        private async Task<int> GetTotalCountAsync(ProjectPaginatedDto filter = null)
        {

            var lstItems = _projectRepo.GetAll()
                             .WhereIf(!filter.Search.IsNullOrEmpty(), x => x.Name.Contains(filter.Search))
                              .WhereIf(filter.StatusId >= -1, x => x.Status == (ProjectStatus)filter.StatusId)
                             .WhereIf(filter.AgencyId > 0, x => x.AgencyId == filter.AgencyId)
                              .WhereIf(filter.AgencyTypeId > 0, x => x.AgencyTypeId == filter.AgencyTypeId);

            //check user Role 
            var userRoles = (await UserManager.GetRolesAsync(await GetCurrentUserAsync())).ToList();
            if (userRoles.Count > 0 && userRoles.Contains(StaticRoleNames.Tenants.Admin) == false)
            {
                if (userRoles.Contains(StaticRoleNames.Tenants.Consultant))
                    lstItems = lstItems.Where(d => d.ConsultantId == AbpSession.UserId);

                if (userRoles.Contains(StaticRoleNames.Tenants.Contractor))
                    lstItems = lstItems.Where(d => d.ContractorId == AbpSession.UserId);

                //if (userRoles.Contains(StaticRoleNames.Tenants.LabProjectManager))
                //    lstItems= lstItems.Where(d => d.LabProjectManagerId == AbpSession.UserId);

                if (userRoles.Contains(StaticRoleNames.Tenants.SupervisingQuality))
                    lstItems = lstItems.Where(d => d.SupervisingQualityId == AbpSession.UserId);

                if (userRoles.Contains(StaticRoleNames.Tenants.SupervisingEngineer))
                    lstItems = lstItems.Where(d => d.SupervisingEngineerId == AbpSession.UserId);
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
