using Abp.Application.Services.Dto;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eConLab.Agencies.Dto;
using eConLab.Account;
using eConLab.QCUsers.Dto;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Abp.UI;
using Abp.Collections.Extensions;
using Abp.Extensions;
using eConLab.Departments;
using eConLab.Departments.Dto;

namespace eConLab.Agencies
{

    public class DepartmentAppService :
         eConLabAppServiceBase,
        IDepartmentAppService
    {
        private readonly IRepository<Department, long> _departmentRepository;
        private readonly IMapper _mapper;
        public DepartmentAppService(IMapper mapper, IRepository<Department, long> departmentRepository
           )

        {
            _mapper = mapper;
            _departmentRepository = departmentRepository;
        }

        public async Task<DepartmentDto> CreateOrUpdate(CreateUpdateDepartmentDto input)
        {
            await _departmentRepository.InsertOrUpdateAsync(_mapper.Map<Department>(input));
            await CurrentUnitOfWork.SaveChangesAsync();

            return _mapper.Map<DepartmentDto>(input);
        }


        public async Task<DepartmentDto> Get(long id)
        {
            var obje = await _departmentRepository.FirstOrDefaultAsync(x => x.Id == id);

            var obj = _mapper.Map<DepartmentDto>(obje);
            return obj;
        }


        public async Task<PagedResultDto<DepartmentDto>> GetAll(DepartmentPaginatedDto input)
        {
            var filter = ObjectMapper.Map<QCUserPagedAndSortedResultRequestDto>(input);

            // var sorting = (string.IsNullOrEmpty(input.Sorting) ? "Name DESC" : input.Sorting).Replace("ShortName", "Name");

            var lstItems = await GetListAsync(input.SkipCount, input.MaxResultCount, input);
            var totalCount = await GetTotalCountAsync(input);

            return new PagedResultDto<DepartmentDto>(totalCount, ObjectMapper.Map<List<DepartmentDto>>(lstItems));
        }


        private async Task<List<Department>> GetListAsync(int skipCount, int maxResultCount, DepartmentPaginatedDto filter = null)
        {

            var lstItems = _departmentRepository.GetAll()
                .Skip(skipCount)
                .Take(maxResultCount)
                .WhereIf(!filter.Search.IsNullOrEmpty(), x => x.Name.Contains(filter.Search));
            //.WhereIf(!filter.Price.IsNullOrWhiteSpace(), x => x.Price.ToString().Contains(filter.Price))
            //.WhereIf(!filter.PublishDate.IsNullOrWhiteSpace(), x => x.PublishDate.ToString().Contains(filter.PublishDate))

            return lstItems.ToList();
        }

        private async Task<int> GetTotalCountAsync(DepartmentPaginatedDto filter = null)
        {

            var lstItems = _departmentRepository.GetAll()
                         .WhereIf(!filter.Search.IsNullOrEmpty(), x => x.Name.Contains(filter.Search));
                //.WhereIf(!filter.Id.IsNullOrWhiteSpace(), x => x.Id.ToString().Contains(filter.Id))
                //.WhereIf(!filter.Name.IsNullOrWhiteSpace(), x => x.Name.Contains(filter.Name))
                //.WhereIf(!filter.Price.IsNullOrWhiteSpace(), x => x.Price.ToString().Contains(filter.Price))
                //.WhereIf(!filter.PublishDate.IsNullOrWhiteSpace(), x => x.PublishDate.ToString().Contains(filter.PublishDate))
               
            return lstItems.Count();
        }


        public async Task<bool> Delete(long Id)
        {
            await _departmentRepository.DeleteAsync(Id);
            return true;
        }
    }
}
