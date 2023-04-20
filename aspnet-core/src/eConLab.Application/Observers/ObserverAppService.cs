using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using AutoMapper;
using eConLab.Account;
using eConLab.Authorization.Accounts;
using eConLab.Authorization.Accounts.Dto;
using eConLab.Authorization.Roles;
using eConLab.Observers.Dto;
using eConLab.QCUsers.Dto;
using eConLab.TownShips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Observers
{
    public class ObserverAppService : eConLabAppServiceBase, IObserverAppService
    {
        private readonly IRepository<Observer, long> _observerRepo;
        private readonly IRepository<TownShip, long> _townShipRepo;

        private readonly AccountAppService _accountAppService;
        private readonly IMapper _mapper;

        public ObserverAppService(IMapper mapper,
            IRepository<TownShip, long> townShipRepo,
            IRepository<Observer, long> observerRepo, AccountAppService accountAppService)
        {
            _observerRepo = observerRepo;
            _accountAppService = accountAppService;
            _townShipRepo = townShipRepo;
            _mapper = mapper;
        }
        public async Task<ObserverDto> CreateOrUpdate(CreateUpdateObserverDto input)
        {
            if (input.UserId ==null)
            {
                var userInput = new RegisterInput {
                    Name = input.Name,
                    EmailAddress = input.EmailAddress,
                    Surname = input.EmailAddress,
                    UserName = input.UserName,
                    Password = input.Password
                };
                var resultCreateUser = await _accountAppService.RegisterUserByRole(userInput, StaticRoleNames.Tenants.Observer);
                if (resultCreateUser != null)
                {
                    var obj = _mapper.Map<Observer>(input);
                    obj.UserId = resultCreateUser.UserId;
                    await _observerRepo.InsertAsync(obj);
                    await CurrentUnitOfWork.SaveChangesAsync();
                    return _mapper.Map<ObserverDto>(obj);
                }
            }
            else
            {
                //update
                //just we need to update email and user Type 

                var currentUser = await UserManager.FindByIdAsync(input.UserId.ToString());

                var observer = new Observer();
                observer = await _observerRepo.FirstOrDefaultAsync(d => d.UserId == input.UserId.Value);
                //  qcUser = _mapper.Map<QCUser>(input.QCUserInput);
                 observer = _mapper.Map<Observer>(input);
               
                _observerRepo.Update(observer);
               
                await CurrentUnitOfWork.SaveChangesAsync();
                return _mapper.Map<ObserverDto>(input);

            }

            return new ObserverDto();
        }

        public async Task<PagedResultDto<ObserverDto>> GetAll(ObserverPaginatedDto input)
        {
             

            // var sorting = (string.IsNullOrEmpty(input.Sorting) ? "Name DESC" : input.Sorting).Replace("ShortName", "Name");

            var lstItems = await GetListAsync(input.SkipCount, input.MaxResultCount, input);
            var totalCount = await GetTotalCountAsync(input);
            var result = ObjectMapper.Map<List<ObserverDto>>(lstItems);
            result.ForEach(s => s.TownShipName = _townShipRepo.FirstOrDefault(z => z.Id == s.TownShipId)?.Name);
            return new PagedResultDto<ObserverDto>(totalCount, result);
        }


        private async Task<List<Observer>> GetListAsync(int skipCount, int maxResultCount, ObserverPaginatedDto filter = null)
        {
            //var currentUserType = (UserTypes)filter.Id;
            var lstItems = _observerRepo.GetAll()
                .Skip(skipCount)
                .Take(maxResultCount)
                .WhereIf(!filter.Search.IsNullOrEmpty(), x => x.Name.Contains(filter.Search))
            .WhereIf(!filter.Search.IsNullOrWhiteSpace(), x => x.NationalId.Contains(filter.Search))
             .WhereIf(!filter.Search.IsNullOrWhiteSpace(), x => x.PhoneNumber.Contains(filter.Search));


            return lstItems.ToList();
        }

        private async Task<int> GetTotalCountAsync(ObserverPaginatedDto filter = null)
        {

            var lstItems = _observerRepo.GetAll()
                          .WhereIf(!filter.Search.IsNullOrEmpty(), x => x.Name.Contains(filter.Search))
                          .WhereIf(!filter.Search.IsNullOrWhiteSpace(), x => x.NationalId.Contains(filter.Search))
                          .WhereIf(!filter.Search.IsNullOrWhiteSpace(), x => x.PhoneNumber.Contains(filter.Search));


            return lstItems.Count();
        }

        public async Task<ObserverDto> GetById(long id)
        {
            var observer = await _observerRepo.FirstOrDefaultAsync(x => x.UserId == id);
            var obj = _mapper.Map<ObserverDto>(observer);
            obj.EmailAddress = UserManager.GetUserById(observer.UserId)?.EmailAddress;
            return obj;
        }
    }
}
