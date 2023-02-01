using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore;
using Abp.UI;
using AutoMapper;
using eConLab.Account;
using eConLab.Authorization.Accounts.Dto.Contractor;
using eConLab.Authorization.Roles;
using eConLab.Roles.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

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

        //public async Task<ListResultDto<ContractorDto>> GetAllContractors()
        //{
        //    var contractorLst =await _contractorRepo.GetAllListAsync();

        //    return new ListResultDto<ContractorDto>(
        //        ObjectMapper.Map<List<ContractorDto>>(contractorLst).OrderBy(p => p.ContactName).ToList()
        //    );
        //}

        public  async Task<PagedResultDto<ContractorDto>> GetAllContractors(ContractorPagedAndSortedResultRequestDto input)
        {
            var filter = ObjectMapper.Map<ContractorPagedAndSortedResultRequestDto>(input);

           // var sorting = (string.IsNullOrEmpty(input.Sorting) ? "Name DESC" : input.Sorting).Replace("ShortName", "Name");

            var lstItems = await GetListAsync(input.SkipCount, input.MaxResultCount);
            var totalCount = await GetTotalCountAsync();

            return new PagedResultDto<ContractorDto>(totalCount, ObjectMapper.Map<List<ContractorDto>>(lstItems));
        }


            public async Task<List<Contractor>> GetListAsync(int skipCount, int maxResultCount, ContractorFilter filter = null)
            {
               
                var lstItems = await _contractorRepo.GetAll()
                    .Skip(skipCount)
                    .Take(maxResultCount)
                    .ToListAsync();
            //.WhereIf(!filter.Id.IsNullOrWhiteSpace(), x => x.Id.ToString().Contains(filter.Id))
            //.WhereIf(!filter.Name.IsNullOrWhiteSpace(), x => x.Name.Contains(filter.Name))
            //.WhereIf(!filter.Price.IsNullOrWhiteSpace(), x => x.Price.ToString().Contains(filter.Price))
            //.WhereIf(!filter.PublishDate.IsNullOrWhiteSpace(), x => x.PublishDate.ToString().Contains(filter.PublishDate))

            return lstItems;
            }

            public async Task<int> GetTotalCountAsync(ContractorFilter filter=null)
            {
               
                var lstItems =  await _contractorRepo.GetAll()
                    //.WhereIf(!filter.Id.IsNullOrWhiteSpace(), x => x.Id.ToString().Contains(filter.Id))
                    //.WhereIf(!filter.Name.IsNullOrWhiteSpace(), x => x.Name.Contains(filter.Name))
                    //.WhereIf(!filter.Price.IsNullOrWhiteSpace(), x => x.Price.ToString().Contains(filter.Price))
                    //.WhereIf(!filter.PublishDate.IsNullOrWhiteSpace(), x => x.PublishDate.ToString().Contains(filter.PublishDate))
                    .ToListAsync();
                return lstItems.Count;
            }
        }
    }


