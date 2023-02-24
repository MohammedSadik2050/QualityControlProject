using Abp.Authorization;
using Abp.Domain.Repositories;
using AutoMapper;
using eConLab.Authorization;
using eConLab.Proj.Dto;
using eConLab.ProjectModels;
using eConLab.Req;
using eConLab.ReqProjectItems.Dto;
using eConLab.RequestTests.Dto;
using eConLab.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.ReqProjectItems
{

    [AbpAuthorize]
    public class RequestProjectItemAppService :
        eConLabAppServiceBase,
       IRequestProjectItemAppService
    {
        private readonly IRepository<RequestProjectItem, long> _requestProjectItemRepository;
        private readonly IMapper _mapper;
        public RequestProjectItemAppService(IMapper mapper,
            IRepository<RequestProjectItem, long> equestProjectItemRepo
            )

        {
            _mapper = mapper;
            _requestProjectItemRepository = equestProjectItemRepo;
        }

        
        public async Task<RequestProjectItemDto> CreateOrUpdate(CreateOrUpdateRequestProjectItemDto input)
        {

            await _requestProjectItemRepository.InsertOrUpdateAsync(_mapper.Map<RequestProjectItem>(input));
            await CurrentUnitOfWork.SaveChangesAsync();

            return _mapper.Map<RequestProjectItemDto>(input);
        }


        public async Task<RequestProjectItemDto> Get(long id)
        {
            var obje = await _requestProjectItemRepository.FirstOrDefaultAsync(x => x.Id == id);

            var obj = _mapper.Map<RequestProjectItemDto>(obje);
            return obj;
        }


        public async Task<List<RequestProjectItemViewDto>> GetAll(long requestId)
        {

            return _requestProjectItemRepository.GetAllIncluding(s => s.ProjectItem).Where(s => s.RequestId == requestId)
                    .Select(s => new RequestProjectItemViewDto
                    {
                        Id = s.Id,
                       RequestProjectItem = _mapper.Map<ProjectItemDto>(s.ProjectItem),
                        ProjectIdItemId=s.ProjectItemId,
                    }).ToList();

        }

        //[AbpAuthorize(PermissionNames.Pages_Manage_InspectionTest)]
        public async Task<bool> Delete(long Id)
        {
            await _requestProjectItemRepository.DeleteAsync(Id);
            return true;
        }
    }
}
