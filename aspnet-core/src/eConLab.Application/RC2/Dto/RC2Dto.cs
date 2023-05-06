using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.RC2.Dto
{
    public class RC2Dto : EntityDto<long>
    {
        public string TestStationA { get; set; }
        public string TestStationB { get; set; }
        public string TestStationC { get; set; }
        public string TrayWeightWhithoutAsphaltA { get; set; }
        public string TrayWeightWhithoutAsphaltB { get; set; }
        public string TrayWeightWhithoutAsphaltC { get; set; }
        public string TrayWeightWhithAsphaltA { get; set; }
        public string TrayWeightWhithAsphaltB { get; set; }
        public string TrayWeightWhithAsphaltC { get; set; }
        public string AreaOfTrayA { get; set; }
        public string AreaOfTrayB { get; set; }
        public string AreaOfTrayC { get; set; }
        public string RequiredRate { get; set; }

        public long RequestId { get; set; }

        public long RequestInspectionTestId { get; set; }
    }
}
