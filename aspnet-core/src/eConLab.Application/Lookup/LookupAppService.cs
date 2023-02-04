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
using eConLab.Lookup.Dto;
using eConLab.ProjectModels;

namespace eConLab.Lookup
{
    public class LookupAppService :
        eConLabAppServiceBase,
       ILookupAppService
    {
        private readonly IRepository<Agency, long> _agencyRepository;
        private readonly IRepository<AgencyType, long> _agencyTypeRepo;
        private readonly IRepository<QCUser, long> _qcUserRepo;
        private readonly IRepository<Project, long> _projectRepo;
        private readonly IMapper _mapper;
        public LookupAppService(
            IMapper mapper,
            IRepository<Agency, long> agencyRepository,
            IRepository<AgencyType, long> agencyTypeRepo,
            IRepository<QCUser, long> qcUserRepo,
            IRepository<Project, long> projectRepo
            )

        {
            _mapper = mapper;
            _agencyRepository = agencyRepository;
            _agencyTypeRepo = agencyTypeRepo;
            _qcUserRepo = qcUserRepo;
            _projectRepo= projectRepo;
        }

        public async Task<List<DropdownListDto>> AgencyList()
        {
           return  _agencyRepository.GetAll().Select(d=> new DropdownListDto { Id=d.Id,Name=d.Name}).ToList();
           
        }

        public async Task<List<DropdownListDto>> AgencyTypeList()
        {
            return _agencyTypeRepo.GetAll().Select(d => new DropdownListDto { Id = d.Id, Name = d.Name }).ToList();
        }

        public async Task<List<DropdownListDto>> ConsultantList()
        {
            return _qcUserRepo.GetAll().Where(d=>d.UserTypes==UserTypes.Consultant).Select(d => new DropdownListDto { Id = d.Id, Name = d.Name }).ToList();
        }
        public async Task<List<DropdownListDto>> ContractorList()
        {
            return _qcUserRepo.GetAll().Where(d => d.UserTypes == UserTypes.Contractor).Select(d => new DropdownListDto { Id = d.Id, Name = d.Name }).ToList();
        }

        public async Task<List<DropdownListDto>> SupervisingQualityList()
        {
            return _qcUserRepo.GetAll().Where(d => d.UserTypes == UserTypes.SupervisingQuality).Select(d => new DropdownListDto { Id = d.Id, Name = d.Name }).ToList();
        }

        public async Task<List<DropdownListDto>> SupervisingEngineerList()
        {
            return _qcUserRepo.GetAll().Where(d => d.UserTypes == UserTypes.SupervisingEngineer).Select(d => new DropdownListDto { Id = d.Id, Name = d.Name }).ToList();
        }

        public async Task<List<DropdownListDto>> LabProjectManagerList()
        {
            return _qcUserRepo.GetAll().Where(d => d.UserTypes == UserTypes.LabProjectManager).Select(d => new DropdownListDto { Id = d.Id, Name = d.Name }).ToList();
        }

        public async Task<List<DropdownListDto>> LabProjecList()
        {
            return _projectRepo.GetAll().Select(d => new DropdownListDto { Id = d.Id, Name = d.Name }).ToList();
        }


    }
}
