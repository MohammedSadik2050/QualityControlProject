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
using eConLab.TestModels;
using eConLab.Tests.Dto;
using Abp.Collections.Extensions;
using Abp.Extensions;
using Microsoft.AspNetCore.Authorization;
using Abp.Authorization;
using eConLab.Authorization;
using eConLab.RequestTests.Dto;

namespace eConLab.Test
{

    [AbpAuthorize]
    public class RequestnspectionTestAppService :
        eConLabAppServiceBase,
       IRequestInspectionTestAppService
    {
        private readonly IRepository<RequestInspectionTest, long> _requestInspectionTestRepository;
        private readonly IMapper _mapper;
        public RequestnspectionTestAppService(IMapper mapper,
            IRepository<RequestInspectionTest, long> requestInspectionTestRepository
            )

        {
            _mapper = mapper;
            _requestInspectionTestRepository = requestInspectionTestRepository;
        }

        [AbpAuthorize(PermissionNames.Pages_Manage_InspectionTest)]
        public async Task<RequestInspectionTestDto> CreateOrUpdate(CreateUpdateRequestTestDto input)
        {

            await _requestInspectionTestRepository.InsertOrUpdateAsync(_mapper.Map<RequestInspectionTest>(input));
            await CurrentUnitOfWork.SaveChangesAsync();

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
                        RequestId = s.RequestId,
                        InspectionTestType = s.InspectionTestType,
                        InspectionTestId = s.InspectionTestId,
                        Cost = s.InspectionTest.Cost,
                        Name = s.InspectionTest.Name,
                        Code = s.InspectionTest.Code,
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