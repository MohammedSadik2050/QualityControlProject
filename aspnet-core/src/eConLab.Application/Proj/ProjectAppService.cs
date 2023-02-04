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
            IRepository<ProjectItem, long> projectItemRepo )

        {
            _mapper = mapper;
            _agencyRepository = agencyRepository;
            _agencyTypeRepo = agencyTypeRepo;
            _qcUserRepo = qcUserRepo;
            _projectRepo = projectRepo;
            _projectItemRepo = projectItemRepo;
        }

        public async Task<ProjectDto> CreateOrUpdate(ProjectDto input)
        {
            if (input.Id == default(int))
            {
                await _projectRepo.InsertAsync(_mapper.Map<Project>(input));
                await CurrentUnitOfWork.SaveChangesAsync();

            }
            else
            {
                var obje = await _projectRepo.FirstOrDefaultAsync(x => x.Id == input.Id);
                if (obje != null)
                {
                    await _projectRepo.UpdateAsync(_mapper.Map<Project>(input));
                    await CurrentUnitOfWork.SaveChangesAsync();

                }
            }

            return _mapper.Map<ProjectDto>(input);
        }


        public async Task<ProjectDto> Get(long id)
        {
            var obje = _projectRepo.FirstOrDefault(x => x.Id == id);

            var obj = _mapper.Map<ProjectDto>(obje);
            return obj;
        }


        public async Task<PagedResultDto<ProjectDto>> GetAll(ProjectPaginatedDto input)
        {
            var filter = ObjectMapper.Map<ProjectPaginatedDto>(input);

            var lstItems = await GetListAsync(input.SkipCount, input.MaxResultCount);
            var totalCount = await GetTotalCountAsync();

            return new PagedResultDto<ProjectDto>(totalCount, ObjectMapper.Map<List<ProjectDto>>(lstItems));
        }


        private async Task<List<Project>> GetListAsync(int skipCount, int maxResultCount, ProjectFilter filter = null)
        {

            var lstItems = _projectRepo.GetAll()
                                          .Skip(skipCount)
                                          .Take(maxResultCount);
                                          
            if (filter.IscurrentUser)
            {
               
                //check user Role 
                var userRoles =(await UserManager.GetRolesAsync(await GetCurrentUserAsync())).ToList();
                if (userRoles.Count > 0)
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
            }

            return lstItems.ToList();
        }

        private async Task<int> GetTotalCountAsync(ProjectFilter filter = null)
        {

            var lstItems = _projectRepo.GetAll();
            if (filter.IscurrentUser)
            {

                //check user Role 
                var userRoles = (await UserManager.GetRolesAsync(await GetCurrentUserAsync())).ToList();
                if (userRoles.Count > 0)
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
            }
            return lstItems.ToList().Count;
        }


        public async Task<List<ProjectDto>> GetAllProjectList()
        {
            var query = _projectRepo.GetAll().ToList();
            return _mapper.Map<List<ProjectDto>>(query);
        }

        
    }
}
