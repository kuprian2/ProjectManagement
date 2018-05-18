using AutoMapper;
using ProjectManagement.BLL.Contracts.Dto;
using ProjectManagement.UI.Models;

namespace ProjectManagement.UI.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProjectDto, ProjectFullModel>().ReverseMap();
        }
    }
}
