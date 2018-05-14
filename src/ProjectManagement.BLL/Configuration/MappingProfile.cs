using AutoMapper;
using ProjectManagement.BLL.Contracts.Dto;
using ProjectManagement.DAL.Contracts.Domain;

namespace ProjectManagement.BLL.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Project, ProjectDto>().ReverseMap();
        }
    }
}
