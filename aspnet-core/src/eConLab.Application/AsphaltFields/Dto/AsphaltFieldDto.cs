using Abp.Application.Services.Dto;
using eConLab.Req;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.AsphaltFields.Dto
{
    public class AsphaltFieldDto : EntityDto<long>
    {
        public string LabDensity { get; set; }
        public string Moisture { get; set; }
        public string LayerThickness { get; set; }

        public string PointNo1 { get; set; } = "1";
        public string PointNo2 { get; set; } = "2";
        public string PointNo3 { get; set; } = "3";
        public string PointNo4 { get; set; } = "4";
        public string PointNo5 { get; set; } = "5";
        public string PointNo6 { get; set; } = "6";
        public string CompactionRation1 { get; set; }
        public string CompactionRation2 { get; set; }
        public string CompactionRation3 { get; set; }
        public string CompactionRation4 { get; set; }
        public string CompactionRation5 { get; set; }
        public string CompactionRation6 { get; set; }
        public string Moisture1 { get; set; }
        public string Moisture2 { get; set; }
        public string Moisture3 { get; set; }
        public string Moisture4 { get; set; }
        public string Moisture5 { get; set; }
        public string Moisture6 { get; set; }

        public string LayerType1 { get; set; }
        public string LayerType2 { get; set; }
        public string LayerType3 { get; set; }
        public string LayerType4 { get; set; }
        public string LayerType5 { get; set; }
        public string LayerType6 { get; set; }
        public string Remark1 { get; set; }
        public string Remark2 { get; set; }
        public string Remark3 { get; set; }
        public string Remark4 { get; set; }
        public string Remark5 { get; set; }
        public string Remark6 { get; set; }

        public long RequestId { get; set; } 

        public long RequestInspectionTestId { get; set; } 
    }
}
