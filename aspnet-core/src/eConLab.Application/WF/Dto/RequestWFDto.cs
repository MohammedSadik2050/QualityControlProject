using Abp.Application.Services.Dto;
using eConLab.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.WF.Dto
{
    public class RequestWFDto:EntityDto<long>
    {
        public long RequestId { get; set; }
        public long CurrentUserId { get; set; }
        public string ActionName { get; set; }
        public string ActionNotes { get; set; }
        public Entities Entity { get; set; }
    }
}
