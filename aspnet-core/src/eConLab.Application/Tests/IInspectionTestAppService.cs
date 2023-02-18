using Abp.Application.Services.Dto;
using eConLab.TestModels;
using eConLab.Tests.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Test
{
    public interface IInspectionTestAppService
    {
        Task<InspectionTestDto> CreateOrUpdate(CreateUpdateInspectionTestDto input);
        Task<InspectionTestDto> Get(long id);
        Task<PagedResultDto<InspectionTestDto>> GetAll(InspectionTestPaginatedDto input);
        Task<List<InspectionTestDto>> GetAllInspectionTestList();
        Task<List<InspectionTestDto>> GetAllInspectionTestListByType(int type);
        //Task<bool> Delete(long Id);
    }
}
