using Abp.Application.Services.Dto;
using eConLab.Enum;
using eConLab.Observers.Dto;
using eConLab.Proj.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Requests.Dto
{
    public class RequestViewDto:EntityDto<long>
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public string Code { get; set; }
        public DateTime InspectionDate { get; set; }
        public string Description { get; set; }
        public long ProjectId { get; set; }       
        public long? ObserverId { get; set; }       
        public string ObserverName { get; set; }       
        public string DistrictName { get; set; }
        public string PhomeNumberSiteResponsibleOne { get; set; }
        public string PhomeNumberSiteResponsibleTwo { get; set; }
        public MainRequestTypes MainRequestType { get; set; }
        public HasSamples HasSample { get; set; }
        public string Geometry { get; set; }

        public ProjectDto Project { get; set; }
   
    }
}
