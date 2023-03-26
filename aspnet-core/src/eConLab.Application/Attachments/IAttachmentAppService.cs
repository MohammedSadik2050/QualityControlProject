using Abp.Application.Services;
using eConLab.Attachments.Dto;
using eConLab.Enum;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Attachments
{
    public interface IAttachmentAppService :
        IApplicationService
    {
        Task<AttachmentDto> CreateOrUpdate([FromForm] CreateUpdateAttachment input);
        Task<List<AttachmentDto>> GetAllAttachment(Entities entityType, long entityId);
        Task<bool> Delete(long Id);
    }
}
