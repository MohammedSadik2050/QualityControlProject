using AutoMapper;
using eConLab.Attachment;
using eConLab.QCUsers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Attachments.Dto
{
    public class AttachmentProfile : Profile
    {
        public AttachmentProfile()
        {
            CreateMap<eConLab.Attachment.Attachments, AttachmentDto>().ReverseMap();
            CreateMap<eConLab.Attachment.Attachments, CreateUpdateAttachment>().ReverseMap();
            CreateMap< AttachmentDto, CreateUpdateAttachment >().ReverseMap();
        }
    }
}
