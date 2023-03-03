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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public async Task<List<DepartmentDto>> GetAllDepartmentDropDown()
        {


            var lstItems = _departmentRepository.GetAllList();
            return _mapper.Map<List<DepartmentDto>>(lstItems);
        }

        public async Task<PagedResultDto<DepartmentDto>> GetAll(DepartmentPaginatedDto input)
        {
          

            var lstItems = await GetListAsync(input.SkipCount, input.MaxResultCount, input);
            var totalCount = await GetTotalCountAsync(input);

            return new PagedResultDto<DepartmentDto>(totalCount, lstItems);
        }


        private async Task<List<DepartmentDto>> GetListAsync(int skipCount, int maxResultCount, DepartmentPaginatedDto filter = null)
        {

            var lstItems = _departmentRepository.GetAllIncluding(s => s.Agency)
                .Skip(skipCount)
                .Take(maxResultCount)
                .WhereIf(!filter.Search.IsNullOrEmpty(), x => x.Name.Contains(filter.Search))
                .WhereIf(filter.AgencyId>0, x => x.AgencyId == filter.AgencyId);
            //.WhereIf(!filter.PublishDate.IsNullOrWhiteSpace(), x => x.PublishDate.ToString().Contains(filter.PublishDate))
            var res = lstItems.Select(mod => new DepartmentDto
            {
                Id= mod.Id,
                Name= mod.Name,
                AgencyId= mod.AgencyId,
                AgencyName= mod.Agency.Name,
            }).ToList();
            return res;
        }

        private async Task<int> GetTotalCountAsync(DepartmentPaginatedDto filter = null)
        {

            var lstItems = _departmentRepository.GetAll()
                         .WhereIf(!filter.Search.IsNullOrEmpty(), x => x.Name.Contains(filter.Search))
                         .WhereIf(filter.AgencyId > 0, x => x.AgencyId == filter.AgencyId);
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
