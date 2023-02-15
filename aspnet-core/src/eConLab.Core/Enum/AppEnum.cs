using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Enum
{

    public enum LookupTypes
    {
        Muncipilty = 1,
    }

    public enum InspectionTestTypes
    {
        Concret = 1,
        Soil,
        Asphalt
    }

    public enum MainRequestTypes
    {
        Inspection = 1,
    }

    public enum HasSamples
    {
        Yes = 1,
        No = 2,
    }

    public static class ActionNames
    {
        public const string Approve = "Approve";
        public const string Rejected = "Rejected";
        public const string Pending = "Pending";
    }
}
