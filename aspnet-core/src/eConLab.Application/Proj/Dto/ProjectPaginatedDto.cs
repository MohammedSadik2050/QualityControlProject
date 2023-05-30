﻿using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Proj.Dto
{
    public class ProjectPaginatedDto : PagedAndSortedResultRequestDto
    {
        public string Search { get; set; }
        public int? StatusId { get; set; }
        public int? AgencyTypeId { get; set; }
        public int? AgencyId { get; set; }
        public long? DepartmentId { get; set; }
        public long? ContractorId { get; set; }
    }
    public class ProjectFilter
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
