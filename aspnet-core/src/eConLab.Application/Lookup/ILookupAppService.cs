using eConLab.Lookup.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Lookup
{
    public interface  ILookupAppService
    {
        Task<List<DropdownListDto>> AgencyList();
        Task<List<DropdownListDto>> AgencyTypeList();
        Task<List<DropdownListDto>> ConsultantList();
        Task<List<DropdownListDto>> ContractorList();
        Task<List<DropdownListDto>> SupervisingQualityList();
        Task<List<DropdownListDto>> SupervisingEngineerList();
        Task<List<DropdownListDto>> LabProjectManagerList();
        Task<List<DropdownListDto>> LabProjecList();
        Task<List<DropdownListDto>> InspectionTestTypes();

        Task<List<DropdownListDto>> MainRequestTypes();

        Task<List<DropdownListDto>> RequestsStatus();
    }
}
