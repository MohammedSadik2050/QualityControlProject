using AutoMapper;
using eConLab.Account;
using eConLab.Agencies.Dto;
using eConLab.ProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eConLab.Proj.Dto
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectDto>().ReverseMap();
            CreateMap<ProjectItem, ProjectItemDto>().ReverseMap();
        }
    }
}
