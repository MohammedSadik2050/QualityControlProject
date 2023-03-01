using AutoMapper;
using eConLab.Account;
using eConLab.Departments;
using eConLab.QCUsers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Departments.Dto
{
 
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {

            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<CreateUpdateDepartmentDto, Department>().ReverseMap();
            CreateMap<CreateUpdateDepartmentDto, DepartmentDto>().ReverseMap();
        }
    }
}
