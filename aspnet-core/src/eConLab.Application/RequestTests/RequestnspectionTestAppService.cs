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
using eConLab.Tests.Dto;
using Abp.Collections.Extensions;
using Abp.Extensions;
using Microsoft.AspNetCore.Authorization;
using Abp.Authorization;
using eConLab.Authorization;
using eConLab.RequestTests.Dto;
using eConLab.Req;

namespace eConLab.Test
{

   // [AbpAuthorize]
    public class RequestnspectionTestAppService :
        eConLabAppServiceBase,
       IRequestInspectionTestAppService
    {
        private readonly IRepository<eConLab.TestModels.RC2, long> _rcRepository;
        private readonly IRepository<eConLab.TestModels.AsphaltField, long> _asphaltFieldRepository;
        private readonly IRepository<RequestInspectionTest, long> _requestInspectionTestRepository;
        private readonly IMapper _mapper;
        public RequestnspectionTestAppService(IMapper mapper, IRepository<eConLab.TestModels.RC2, long> rcRepository,
             IRepository<eConLab.TestModels.AsphaltField, long> asphaltFieldRepository,
        IRepository<RequestInspectionTest, long> requestInspectionTestRepository
            )

        {
            _mapper = mapper;
            _requestInspectionTestRepository = requestInspectionTestRepository;
            _rcRepository = rcRepository;
            _asphaltFieldRepository = asphaltFieldRepository;
        }

        //[AbpAuthorize(PermissionNames.Pages_Manage_InspectionTest)]
        public async Task<RequestInspectionTestDto> CreateOrUpdate(CreateUpdateRequestTestDto input)
        {

            await _requestInspectionTestRepository.InsertOrUpdateAsync(_mapper.Map<RequestInspectionTest>(input));
           // await CurrentUnitOfWork.SaveChangesAsync();

            return _mapper.Map<RequestInspectionTestDto>(input);
        }


        public async Task<RequestInspectionTestDto> Get(long id)
        {
            var obje = await _requestInspectionTestRepository.FirstOrDefaultAsync(x => x.Id == id);

            var obj = _mapper.Map<RequestInspectionTestDto>(obje);
            return obj;
        }


        public async Task<List<RequestInspectionTestViewDto>> GetAll(long requestId)
        {

            return _requestInspectionTestRepository.GetAllIncluding(s => s.InspectionTest).Where(s => s.RequestId == requestId)
                    .Select(s => new RequestInspectionTestViewDto
                    {
                        Id= s.Id,
                        HaveResult = _rcRepository.GetAll().Any(x=> x.RequestInspectionTestId == s.Id) ||
                                     _asphaltFieldRepository.GetAll().Any(x => x.RequestInspectionTestId == s.Id),
                        RequestId = s.RequestId,
                        InspectionTestType = s.InspectionTestType,
                        InspectionTestId = s.InspectionTestId,
                        IsLab = s.InspectionTest.IsLabTest,
                        Cost = s.InspectionTest.Cost,
                        Name = s.InspectionTest.Name ?? "",
                        Code = s.InspectionTest.Code ?? "",
                    }).ToList();
         
        }

        //[AbpAuthorize(PermissionNames.Pages_Manage_InspectionTest)]
        public async Task<bool> Delete(long Id)
        {
            await _requestInspectionTestRepository.DeleteAsync(Id);
            return true;
        }
    }
}
