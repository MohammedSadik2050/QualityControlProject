using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.ReqProjectItems.Dto
{
    public class RequestProjectItemDto : EntityDto<long>
    {
        public long RequestId { get; set; }
        public long ProjectItemId { get; set; }
    }
}
