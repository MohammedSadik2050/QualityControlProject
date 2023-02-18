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
using eConLab.TestModels;
using eConLab.Tests.Dto;
using Abp.Collections.Extensions;
using Abp.Extensions;
using Microsoft.AspNetCore.Authorization;
using Abp.Authorization;
using eConLab.Authorization;

namespace eConLab.Test
{

    [AbpAuthorize]
    public class InspectionTestAppService :
        eConLabAppServiceBase,
       IInspectionTestAppService
    {
        private readonly IRepository<InspectionTest, long> _inspectionTestRepository;
        private readonly IMapper _mapper;
        public InspectionTestAppService(IMapper mapper,
            IRepository<InspectionTest, long> inspectionTestRepository
            )

        {
            _mapper = mapper;
            _inspectionTestRepository = inspectionTestRepository;
        }

        [AbpAuthorize(PermissionNames.Pages_Manage_InspectionTest)]
        public async Task<InspectionTestDto> CreateOrUpdate(CreateUpdateInspectionTestDto input)
        {

            await _inspectionTestRepository.InsertOrUpdateAsync(_mapper.Map<InspectionTest>(input));
            await CurrentUnitOfWork.SaveChangesAsync();

            return _mapper.Map<InspectionTestDto>(input);
        }


        public async Task<InspectionTestDto> Get(long id)
        {
            var obje = await _inspectionTestRepository.FirstOrDefaultAsync(x => x.Id == id);

            var obj = _mapper.Map<InspectionTestDto>(obje);
            return obj;
        }


        public async Task<PagedResultDto<InspectionTestDto>> GetAll(InspectionTestPaginatedDto input)
        {
            //  var filter = ObjectMapper.Map<InspectionTestFilter>(input);

            // var sorting = (string.IsNullOrEmpty(input.Sorting) ? "Name DESC" : input.Sorting).Replace("ShortName", "Name");

            var lstItems = await GetListAsync(input.SkipCount, input.MaxResultCount, input);
            var totalCount = await GetTotalCountAsync(input);

            return new PagedResultDto<InspectionTestDto>(totalCount, ObjectMapper.Map<List<InspectionTestDto>>(lstItems));
        }


        private async Task<List<InspectionTest>> GetListAsync(int skipCount, int maxResultCount, InspectionTestPaginatedDto filter = null)
        {

            var lstItems = _inspectionTestRepository.GetAll()
                .Skip(skipCount)
                .Take(maxResultCount)
                .WhereIf(!filter.Search.IsNullOrEmpty(), x => x.Name.Contains(filter.Search));

            return lstItems.ToList();
        }

        private async Task<int> GetTotalCountAsync(InspectionTestPaginatedDto filter = null)
        {

            var lstItems = _inspectionTestRepository.GetAll()
                         .WhereIf(!filter.Search.IsNullOrEmpty(), x => x.Name.Contains(filter.Search));


            return lstItems.Count();
        }


        public async Task<List<InspectionTestDto>> GetAllInspectionTestList()
        {
            var query = _inspectionTestRepository.GetAll().ToList();
            return _mapper.Map<List<InspectionTestDto>>(query);
        }


        //[AbpAuthorize(PermissionNames.Pages_Manage_InspectionTest)]
        //public async Task<bool> Delete(long Id)
        //{
        //    await _inspectionTestRepository.DeleteAsync(Id);
        //    return true;
        //}
    }
}
