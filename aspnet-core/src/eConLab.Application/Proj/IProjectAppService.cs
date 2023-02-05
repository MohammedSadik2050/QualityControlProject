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
      

    }
}
