﻿using Abp.Domain.Entities.Auditing;
using eConLab.Enum;
using eConLab.ProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Req
{
    
    public class Request 
        : AuditedEntity<long>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime InspectionDate { get; set; }
        public string Description { get; set; }
        public long ProjectId { get; set; }
        public string DistrictName { get; set; }
        public string PhomeNumberSiteResponsibleOne { get; set; }
        public string PhomeNumberSiteResponsibleTwo { get; set; }
        public  MainRequestTypes MainRequestType { get; set; }
        public InspectionTestTypes TestType { get; set; }
        public RequestStatus Status { get; set; }
        public HasSamples HasSample { get; set; }
        public string Geometry { get; set; }
        public Project Project { get; set; }
    }
}