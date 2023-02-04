using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.QCUsers.Dto
{
    public class QCUserPagedAndSortedResultRequestDto : PagedAndSortedResultRequestDto
    {
        public long Id { get; set; }
    }
    public class QCUserFilter
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
