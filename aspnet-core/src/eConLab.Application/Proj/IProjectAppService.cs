using Abp.Application.Services.Dto;
using eConLab.Proj.Dto;
using eConLab.ProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Proj
{
    public interface IProjectAppService
    {
        Task<ProjectDto> CreateOrUpdate(ProjectDto input);
        Task<ProjectDto> Get(long id);
        Task<PagedResultDto<ProjectDto>> GetAll(ProjectPaginatedDto input);



        #region ProjectItems
        Task<ProjectItemDto> GetProjectItem(long id);
        Task<List<ProjectItemDto>> GetProjectItemsByProjectId(int projectId);
        Task<ProjectItemDto> CreateOrUpdateProjectItem(ProjectItemDto input);
        #endregion


    }
}
