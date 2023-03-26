using eConLab.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Attachments.Dto
{
    public class CreateUpdateAttachment
    {
        public long EntityId { get; set; }
        public string FilePath { get; set; }
        public string FileURL { get; set; }
        public IFormFile File { get; set; }
        public string Description { get; set; }
        public Entities Entity { get; set; }
    }
}
