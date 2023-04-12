using Abp.Domain.Entities.Auditing;
using eConLab.Enum;
using eConLab.Observers;
using eConLab.ProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Req
{
    
    public class Request 
        : FullAuditedEntity<long>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime InspectionDate { get; set; }
        public string Description { get; set; }
        public long ProjectId { get; set; }
        public long? ObserverId { get; set; }
        public string DistrictName { get; set; }
        public string PhomeNumberSiteResponsibleOne { get; set; }
        public string PhomeNumberSiteResponsibleTwo { get; set; }
        public  MainRequestTypes MainRequestType { get; set; }
        public InspectionTestTypes TestType { get; set; }
        public RequestStatus Status { get; set; }
        public HasSamples HasSample { get; set; }
        public string Geometry { get; set; }
        public Project Project { get; set; }
        public Observer Observer { get; set; }
        public int Hours { get; set; } = 0;
        public int Min { get; set; } = 0;
    }
}
