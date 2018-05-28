using System;
using AutoMapper;
using ProjectManagement.BLL.Contracts.Dto;
using ProjectManagement.BLL.Contracts.Services;
using ProjectManagement.DAL.Contracts.Domain;
using ProjectManagement.DAL.Contracts.Repositories;
using System.Collections.Generic;

namespace ProjectManagement.BLL.Services
{
    public class AdaptedWrongProjectsService : IProjectsService
    {
        private readonly WrongProjectsService _wrongProjectsService;

        public AdaptedWrongProjectsService(IRepository<Project> repository, IMapper mapper)
        {
            _wrongProjectsService = new WrongProjectsService(repository, mapper);
        }

        public event EventHandler<EventArgs> Created;

        protected void OnCreated()
        {
            Created?.Invoke(this, EventArgs.Empty);
        }

        public void Create(ProjectDto entityDto)
        {
            _wrongProjectsService.CreateProject(entityDto);
            OnCreated();
        }

        public ProjectDto Get(int id)
        {
            return _wrongProjectsService.SelectProjectById(id);
        }

        public IEnumerable<ProjectDto> GetAll()
        {
            return _wrongProjectsService.SelectAllProjects();
        }

        public void Update(ProjectDto entityDto)
        {
            _wrongProjectsService.ChangeProject(entityDto);
        }

        public void Delete(int id)
        {
            _wrongProjectsService.RemoveProject(id);
        }

        public IEnumerable<ProjectDto> GetByKeyword(string keyword)
        {
            return _wrongProjectsService.SelectByKeyword(keyword);
        }
    }
}
