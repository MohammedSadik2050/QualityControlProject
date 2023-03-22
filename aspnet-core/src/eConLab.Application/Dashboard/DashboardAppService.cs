using Abp.Domain.Repositories;
using AutoMapper;
using eConLab.Account;
using eConLab.Authorization.Accounts;
using eConLab.Dashboard.Dto;
using eConLab.Proj;
using eConLab.ProjectModels;
using eConLab.QCUsers;
using eConLab.Req;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Dashboard
{
    

        public class DashboardAppService : eConLabAppServiceBase, IDashboardAppService
    {
            private readonly IRepository<QCUser, long> _qcUsersRepo;
        private readonly IRepository<Project, long> _projectRepo;
        private readonly IRepository<Request, long> _requestRepo;
            private readonly IMapper _mapper;

        public DashboardAppService(
            IMapper mapper,
            IRepository<QCUser, long> qcUsersRepo,
            IRepository<Project, long> projectRepo,
            IRepository<Request, long> requestRepo)
        {
            _qcUsersRepo = qcUsersRepo;
            _projectRepo = projectRepo;
            _requestRepo = requestRepo;
            _mapper = mapper;
        }

         public async Task<StatisticsDto> GetSystemStatisticsDashboard()
        {
            var result = new StatisticsDto();


            var lstUsers = _qcUsersRepo.GetAll().ToList();
            result.StackholderDto.TotalConsultantUsers = lstUsers.Where(d => d.UserTypes == UserTypes.Consultant).Count();
            result.StackholderDto.TotalContractorUsers = lstUsers.Where(d => d.UserTypes == UserTypes.Contractor).Count();

            var lstProjects = _projectRepo.GetAll().ToList();
            result.ProjectStatisticsDto.TotalProjectApproved = lstProjects.Where(d => d.IsActive ==true).Count();
            result.ProjectStatisticsDto.TotalProjectUnderReview = lstProjects.Where(d => d.IsActive == false).Count();

            var lstRequests = _requestRepo.GetAll().ToList();
            
            result.RequestStatisticsDto.TotalRequestApproved = lstRequests.Where(d => d.Status == Enum.RequestStatus.ApprovedBySupervisingQuality || d.Status == Enum.RequestStatus.ApprovedByConsultant).Count();
            result.RequestStatisticsDto.TotalRequestPending = lstRequests.Where(d => d.Status != Enum.RequestStatus.ApprovedBySupervisingQuality && d.Status != Enum.RequestStatus.ApprovedByConsultant).Count();
            return result;
        }
    }
}
