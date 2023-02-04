﻿using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Agencies.Dto
{
    public class AgencyPaginatedDto : PagedAndSortedResultRequestDto
    {
        public long Id { get; set; }
    }
    public class AgencyFilter
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
