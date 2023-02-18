using Abp.Application.Services.Dto;
using eConLab.RequestTests.Dto;
using eConLab.TestModels;
using eConLab.Tests.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Test
{
    public interface IRequestInspectionTestAppService
    {
        Task<RequestInspectionTestDto> CreateOrUpdate(CreateUpdateRequestTestDto input);
        Task<RequestInspectionTestDto> Get(long id);
         Task<List<RequestInspectionTestViewDto>> GetAll(long requestId);
        Task<bool> Delete(long Id);
    }
}
