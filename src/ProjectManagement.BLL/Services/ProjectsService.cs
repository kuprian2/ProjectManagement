using AutoMapper;
using EnsureThat;
using ProjectManagement.BLL.Contracts.Dto;
using ProjectManagement.BLL.Contracts.Services;
using ProjectManagement.BLL.Services.Base;
using ProjectManagement.DAL.Contracts.Domain;
using ProjectManagement.DAL.Contracts.Repositories;
using System.Collections.Generic;


namespace ProjectManagement.BLL.Services
{
    public class ProjectsService : Service<Project, ProjectDto>, IProjectsService
    {
        public ProjectsService(IRepository<Project> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public IEnumerable<ProjectDto> GetByKeyword(string keyword)
        {
            throw new System.NotImplementedException();
        }
    }
}
