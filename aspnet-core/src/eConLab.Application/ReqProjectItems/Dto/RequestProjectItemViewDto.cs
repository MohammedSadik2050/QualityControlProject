using Abp.Application.Services.Dto;
using eConLab.Enum;
using eConLab.Proj.Dto;
using eConLab.ProjectModels;
using eConLab.Req;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.ReqProjectItems.Dto
{
    public class RequestProjectItemViewDto:EntityDto<long>
    {
        public long RequestId { get; set; }
        public ProjectItemDto RequestProjectItem { get; set; }
        public long ProjectIdItemId { get; set; }
    }
}
