using Abp.Application.Services;
using eConLab.Dashboard.Dto;
using eConLab.QCUsers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Dashboard
{
    public interface IDashboardAppService : IApplicationService
    {
        Task<StatisticsDto> GetSystemStatisticsDashboard();
    }
}
