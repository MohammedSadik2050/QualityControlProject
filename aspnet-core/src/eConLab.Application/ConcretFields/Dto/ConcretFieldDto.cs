using Abp.Application.Services.Dto;
using eConLab.Req;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.AsphaltFields.Dto
{
    public class ConcretFieldDto : EntityDto<long>
    {
        public string SampleNumber { get; set; }
        public DateTime? SamplePreparationDate { get; set; }
        public DateTime SamplePreparationEndDate { get; set; }
        public string CylindersReceivedNo { get; set; }
        public string LandingGear { get; set; }
        public string CastCylindersNo { get; set; }
        public string RecoveredCylindersNo { get; set; }
        public DateTime? LabDeliveryDate { get; set; }

        public string AirTemp { get; set; }
        public string CementQty { get; set; }
        public string ConcreteTemp { get; set; }
        public string ConcreteRank { get; set; }
        public string LandingMM { get; set; }
        public string ConcreteUsing { get; set; }
        public string ConcreteSource { get; set; }
        public string ConcreteQty { get; set; }
        public string TruckNo { get; set; }
        public DateTime? TruckLeftDate { get; set; }
        public DateTime? CastingStartDate { get; set; }
        public DateTime? TruckSiteArrivingDate { get; set; }
        public string Comment { get; set; }
        public long RequestId { get; set; }
        public long RequestInspectionTestId { get; set; }
    }
}
