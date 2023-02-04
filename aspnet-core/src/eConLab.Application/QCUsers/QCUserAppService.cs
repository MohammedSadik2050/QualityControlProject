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
using eConLab.Authorization.Accounts;
using eConLab.Authorization.Roles;
using eConLab.QCUsers.Dto;
using eConLab.Roles.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace eConLab.QCUsers
{
    public class QCUserAppService : eConLabAppServiceBase, IQCUserAppService
    {
        private readonly IRepository<QCUser, long> _qcUsersRepo;

        private readonly AccountAppService _accountAppService;
        private readonly IMapper _mapper;

        public QCUserAppService(IMapper mapper, IRepository<QCUser, long> qcUsersRepo, AccountAppService accountAppService)
        {
            _qcUsersRepo = qcUsersRepo;
            _accountAppService = accountAppService;
            _mapper = mapper;
        }

        public async Task<QCUserDto> CreateOrUpdate(QCUserCreateDto input)
        {
            if (input.Id == default(int))
            {
                input.RegisterInput.Surname = input.RegisterInput.Name;
                var resultCreateUser = await _accountAppService.RegisterUserByRole(input.RegisterInput, GetRoleNameByUserType(input.QCUserInput.UserTypes));
                if (resultCreateUser != null)
                {
                    var obj = _mapper.Map<QCUser>(input.QCUserInput);
                    obj.UserId = resultCreateUser.UserId;
                    await _qcUsersRepo.InsertAsync(obj);
                    await CurrentUnitOfWork.SaveChangesAsync();
                    return _mapper.Map<QCUserDto>(obj);
                }
            }
            else
            {
                //update
                //just we need to update email and user Type 
               
                var currentUser = await UserManager.FindByIdAsync(input.QCUserInput.UserId.ToString());

               
                var qcUser=await _qcUsersRepo.FirstOrDefaultAsync(d=>d.UserId== input.QCUserInput.UserId);
                qcUser = _mapper.Map<QCUser>(input.QCUserInput); 
                if (currentUser != null)
                {
                    currentUser.EmailAddress = input.RegisterInput.EmailAddress;
                    if (qcUser.UserTypes != input.QCUserInput.UserTypes)
                    {
                        await UserManager.RemoveFromRoleAsync(currentUser, GetRoleNameByUserType(qcUser.UserTypes));

                        await UserManager.AddToRoleAsync(currentUser, GetRoleNameByUserType(input.QCUserInput.UserTypes));
                    }
                    await _accountAppService.UserManager.UpdateAsync(currentUser);
                }
                await CurrentUnitOfWork.SaveChangesAsync();


            }
            throw new UserFriendlyException("Invalide Data please try again");

        }

        private string GetRoleNameByUserType(UserTypes userType)
        {
            var roleName = string.Empty;

            switch (userType)
            {
                case UserTypes.Contractor:
                    roleName = StaticRoleNames.Tenants.Contractor;
                    break;
                case UserTypes.Consultant:
                    roleName = StaticRoleNames.Tenants.Consultant;
                    break;
                case UserTypes.ConsultingEngineer:
                    roleName = StaticRoleNames.Tenants.ConsultingEngineer;
                    break;
                case UserTypes.SupervisingEngineer:
                    roleName = StaticRoleNames.Tenants.SupervisingEngineer;
                    break;
                case UserTypes.LabProjectManager:
                    roleName = StaticRoleNames.Tenants.LabProjectManager;
                    break;
                case UserTypes.SupervisingQuality:
                    roleName = StaticRoleNames.Tenants.SupervisingQuality;
                    break;
                case UserTypes.SupervisingProjects:
                    roleName = StaticRoleNames.Tenants.SupervisingProjects;
                    break;
                case UserTypes.AmanaStaff:
                    roleName = StaticRoleNames.Tenants.Contractor;
                    break;
                default:
                    break;
            }

            return roleName;
        }



        public async Task<QCUserDto> GetById(long id)
        {
            var contractor = await _qcUsersRepo.FirstOrDefaultAsync(x => x.UserId == id);
            try
            {
                var obj = _mapper.Map<QCUserDto>(contractor);
                return obj;
            }
            catch (Exception ex)
            {

                throw;
            }
            return new QCUserDto();
        }


        public async Task<PagedResultDto<QCUserDto>> GetAll(QCUserPagedAndSortedResultRequestDto input)
        {
            var filter = ObjectMapper.Map<QCUserPagedAndSortedResultRequestDto>(input);

            // var sorting = (string.IsNullOrEmpty(input.Sorting) ? "Name DESC" : input.Sorting).Replace("ShortName", "Name");

            var lstItems = await GetListAsync(input.SkipCount, input.MaxResultCount);
            var totalCount = await GetTotalCountAsync();

            return new PagedResultDto<QCUserDto>(totalCount, ObjectMapper.Map<List<QCUserDto>>(lstItems));
        }


        private async Task<List<QCUser>> GetListAsync(int skipCount, int maxResultCount, QCUserFilter filter = null)
        {

            var lstItems = await _qcUsersRepo.GetAll()
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
            //.WhereIf(!filter.Id.IsNullOrWhiteSpace(), x => x.Id.ToString().Contains(filter.Id))
            //.WhereIf(!filter.Name.IsNullOrWhiteSpace(), x => x.Name.Contains(filter.Name))
            //.WhereIf(!filter.Price.IsNullOrWhiteSpace(), x => x.Price.ToString().Contains(filter.Price))
            //.WhereIf(!filter.PublishDate.IsNullOrWhiteSpace(), x => x.PublishDate.ToString().Contains(filter.PublishDate))

            return lstItems;
        }

        private async Task<int> GetTotalCountAsync(QCUserFilter filter = null)
        {

            var lstItems = await _qcUsersRepo.GetAll()
                //.WhereIf(!filter.Id.IsNullOrWhiteSpace(), x => x.Id.ToString().Contains(filter.Id))
                //.WhereIf(!filter.Name.IsNullOrWhiteSpace(), x => x.Name.Contains(filter.Name))
                //.WhereIf(!filter.Price.IsNullOrWhiteSpace(), x => x.Price.ToString().Contains(filter.Price))
                //.WhereIf(!filter.PublishDate.IsNullOrWhiteSpace(), x => x.PublishDate.ToString().Contains(filter.PublishDate))
                .ToListAsync();
            return lstItems.Count;
        }
    }
}


