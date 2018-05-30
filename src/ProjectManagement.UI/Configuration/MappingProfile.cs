using AutoMapper;
using ProjectManagement.BLL.Contracts.Dto;
using ProjectManagement.UI.Models;

namespace ProjectManagement.UI.Configuration
{
    /// <summary>
    /// Mapping profile for <see cref="AutoMapper"/>.
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProjectDto, ProjectFullModel>().ReverseMap();
        }
    }
}
