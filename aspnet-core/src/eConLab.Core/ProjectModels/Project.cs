using Abp.Domain.Entities.Auditing;
using eConLab.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Spatial;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.ProjectModels
{
    public class Project : FullAuditedEntity<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ContractNumber { get; set; } 
        public DateTime StartDate { get; set; } 
        public DateTime CompletedDate { get; set; } 
        public DateTime SiteDelivedDate { get; set; }
        public long? AgencyTypeId { get; set; }
        public long? AgencyId { get; set; } 
        public long? DepartmentId { get; set; } 
        public long SupervisingEngineerId { get; set; } 
        public long ConsultantId { get; set; } 
        public long ContractorId { get; set; } 

        //public long LabProjectManagerId { get; set; } 
        public long SupervisingQualityId { get; set; } 
        public string GeometryLocations { get; set; }
        //public bool ApprovedByConsultant { get; set; } = false;
        //public bool ApprovedBySupervising { get; set; } = false;
        //public bool ApprovedByLabProjectManager { get; set; } = false;
        public ProjectStatus Status { get; set; } = 0;
        public bool IsActive { get; set; } = false;

    }
}
