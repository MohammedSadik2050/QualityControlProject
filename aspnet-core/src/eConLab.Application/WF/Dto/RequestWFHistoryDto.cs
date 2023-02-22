using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.WF.Dto
{
    public class RequestWFHistoryDto : EntityDto<long>
    {
        public long RequestId { get; set; }
        public long CurrentUserId { get; set; }
        public string ActionName { get; set; }
        public string ActionNotes { get; set; }
        public DateTime CreationTime { get; set; }

    }
}
