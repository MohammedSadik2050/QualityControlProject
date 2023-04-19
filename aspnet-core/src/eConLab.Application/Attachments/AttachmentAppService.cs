using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using AutoMapper;
using eConLab.Account;
using eConLab.Agencies.Dto;
using eConLab.Attachment;
using eConLab.Attachments.Dto;
using eConLab.Enum;
using eConLab.QCUsers.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Attachments
{
    public class AttachmentAppService :
         eConLabAppServiceBase,
        IAttachmentAppService
    {
        private readonly IRepository<eConLab.Attachment.Attachments, long> _attachmentsRepository;
        private readonly IMapper _mapper;
        public AttachmentAppService(IMapper mapper, IRepository<eConLab.Attachment.Attachments, long> attachmentsRepository)

        {
            _mapper = mapper;
            _attachmentsRepository = attachmentsRepository;
        }

        public async Task<AttachmentDto> CreateOrUpdate([FromForm] CreateUpdateAttachment input)
        {
            string staticPath = Path.Combine(Directory.GetCurrentDirectory(), "Attachments/" + input.Entity.ToString() + "/" + input.EntityId);

            bool exists = System.IO.Directory.Exists(staticPath);

            if (!exists)
                System.IO.Directory.CreateDirectory(staticPath);

            var uniqueFileName = GetUniqueFileName(input.File.FileName);
            var filePath = Path.Combine(staticPath, uniqueFileName);
            input.File.CopyTo(new FileStream(filePath, FileMode.Create));

            input.FilePath = filePath;
            input.FileURL = "/Attachments/" + input.Entity.ToString() + "/" + input.EntityId + "/" + uniqueFileName;
            input.FileName = input.File.FileName;
            await _attachmentsRepository.InsertOrUpdateAsync(_mapper.Map<eConLab.Attachment.Attachments>(input));
            await CurrentUnitOfWork.SaveChangesAsync();

            return _mapper.Map<AttachmentDto>(input);
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + DateTime.Now.ToString("dd-MM-yyyy hh-mm")
                      + Path.GetExtension(fileName);
        }
        public async Task<List<AttachmentDto>> GetAllAttachment(Entities entityType, long entityId)
        {
            var query = _attachmentsRepository.GetAll().Where(s => s.Entity == entityType && s.EntityId == entityId).ToList();
            return _mapper.Map<List<AttachmentDto>>(query);
        }


        public async Task<bool> Delete(long Id)
        {
            await _attachmentsRepository.DeleteAsync(Id);
            return true;
        }
    }
}
