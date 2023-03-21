using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Dashboard.Dto
{
    public class StatisticsDto
    {
        public StatisticsDto()
        {
            ProjectStatisticsDto=new ProjectStatisticsDto();
            StackholderDto = new StackholderDto();
            RequestStatisticsDto = new RequestStatisticsDto();
        }
        public StackholderDto StackholderDto { get; set; }
        public ProjectStatisticsDto ProjectStatisticsDto { get; set; }
        public RequestStatisticsDto RequestStatisticsDto { get; set; }
      

    }

    public class StackholderDto
    {
        public int TotalContractorUsers { get; set; }
        public int TotalConsultantUsers { get; set; }
    }

    public class ProjectStatisticsDto
    {
        public int TotalProjectApproved { get; set; }
        public int TotalProjectUnderReview { get; set; }

    }

    public class RequestStatisticsDto
    {
        public int TotalRequestApproved { get; set; }
        public int TotalRequestPending { get; set; }

    }
}
