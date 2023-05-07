﻿using System;
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
        Soil = 2,
        Asphalt = 3,
        Visual = 4,
        Tile = 5,
        MixingWater = 6
    }

    public enum TestForms
    {
        RC2 = 1,
        MC1 = 2,
        AsphaltT310 = 3,
        Concrete = 4
    }

    public enum RequestStatus
    {
        Pending = 1,
        Submitted = 2,
        ApprovedByConsultant = 3,
        ApprovedByLabProjectManager = 4,
        AssignedToObserver = 6,
        Rejected = 5,
        RequestCanceled = 7,
        AssignedtoMaterialEngineering = 8,
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

    public enum ProjectStatus
    {
        NotCompleted = -1,
        Pending = 0,
        ApprovedByConsultantRevstion = 1,
        ApprovedBySupervising = 2,
        ApprovedByLabProjectManager = 3,
        Rejected = 4
    }

    public enum Entities
    {
        Request = 1,
        Project = 2,
    }
}
