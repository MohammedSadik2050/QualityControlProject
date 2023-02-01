using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using AutoMapper;
using eConLab.Account;
using eConLab.Authorization.Accounts.Dto.Contractor;
using eConLab.Authorization.Roles;
using eConLab.Roles.Dto;
using Microsoft.AspNetCore.Hosting;
namespace eConLab.Authorization.Accounts
{
    public class ContractorAppService : eConLabAppServiceBase, IContractorAppService
    {
        private readonly IRepository<Contractor,long> _contractorRepo;
        private readonly AccountAppService _accountAppService;
        private readonly IMapper _mapper;
        
        public ContractorAppService(IMapper mapper,IRepository<Contractor, long> contractorRepo, AccountAppService accountAppService)
        {
            _contractorRepo = contractorRepo;
            _accountAppService = accountAppService;
            _mapper = mapper;
        }

        public async Task<ContractorDto> Create(CreateContractorDto input)
        {
          var resultCreateUser= await _accountAppService.RegisterUserByRole(input.RegisterInput, StaticRoleNames.Tenants.Contractor);
            if (resultCreateUser != null)
            {
                var obj = _mapper.Map<Contractor>(input.ContractorInfo);
                obj.UserId = resultCreateUser.UserId;
                await _contractorRepo.InsertAsync(obj);
                await CurrentUnitOfWork.SaveChangesAsync();
                return _mapper.Map<ContractorDto>(obj);
            }
            throw new UserFriendlyException("Invalide Data please try again");
           
        }


        public async Task<ContractorDto> GetAll(CreateContractorDto input)
        {
            var resultCreateUser = await _accountAppService.RegisterUserByRole(input.RegisterInput, StaticRoleNames.Tenants.Contractor);
            if (resultCreateUser != null)
            {
                var obj = _mapper.Map<Contractor>(input.ContractorInfo);
                obj.UserId = resultCreateUser.UserId;
                await _contractorRepo.InsertAsync(obj);
                await CurrentUnitOfWork.SaveChangesAsync();
                return _mapper.Map<ContractorDto>(obj);
            }
            throw new UserFriendlyException("Invalide Data please try again");

        }

        public async Task<ListResultDto<ContractorDto>> GetAllContractors()
        {
            var contractorLst =await _contractorRepo.GetAllListAsync();

            return new ListResultDto<ContractorDto>(
                ObjectMapper.Map<List<ContractorDto>>(contractorLst).OrderBy(p => p.ContactName).ToList()
            );
        }
    }
}
