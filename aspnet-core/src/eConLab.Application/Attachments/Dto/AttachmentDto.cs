﻿using Abp.Application.Services.Dto;
using eConLab.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Attachments.Dto
{
    public class AttachmentDto : EntityDto<long>
    {
        public long EntityId { get; set; }
        public string FilePath { get; set; }
        public string FileURL { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public Entities Entity { get; set; }
    }
}
