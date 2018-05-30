using AutoMapper;
using ProjectManagement.BLL.Contracts.Dto;
using ProjectManagement.BLL.Contracts.Services;
using ProjectManagement.DAL.Contracts.Domain;
using ProjectManagement.DAL.Contracts.Repositories;
using System;
using System.Collections.Generic;

namespace ProjectManagement.BLL.Services
{
    /// <summary>
    /// Adapted implementation of <see cref="WrongProjectsService"/>
    /// </summary>
    public class AdaptedWrongProjectsService : IProjectsService
    {
        /// <summary>
        /// Instance of <see cref="WrongProjectsService"/>
        /// </summary>
        private readonly WrongProjectsService _wrongProjectsService;

        /// <summary>
        /// Creates an insatnce of <see cref="AdaptedWrongProjectsService"/>
        /// </summary>
        /// <param name="repository">Instance of <see cref="IRepository{TEntity}"/>.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        public AdaptedWrongProjectsService(IRepository<Project> repository, IMapper mapper)
        {
            _wrongProjectsService = new WrongProjectsService(repository, mapper);
        }

        /// <summary>
        /// Occurs when project was successfully created.
        /// </summary>
        public event EventHandler<EventArgs> Created;

        /// <summary>
        /// Invokes <see cref="Created"/> event in the end.
        /// </summary>
        protected void OnCreated()
        {
            Created?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Creates an item in data source from a given entity. Calls <see cref="OnCreated"/> method in the end.
        /// </summary>
        /// <param name="entityDto">Given entity.</param>
        public void Create(ProjectDto entityDto)
        {
            _wrongProjectsService.CreateProject(entityDto);
            OnCreated();
        }

        /// <summary>
        /// Returns an object of type <see cref="TEntityDto"/> with specified value of key from data source.
        /// </summary>
        /// <param name="id">Key to find an object.</param>
        /// <returns>Returns an object with specified value of key from data source.</returns>
        public ProjectDto Get(int id)
        {
            return _wrongProjectsService.SelectProjectById(id);
        }

        /// <summary>
        /// Returns all objects of type <see cref="TEntityDto"/> from data source.
        /// </summary>
        /// <returns>Returns all objects of type <see cref="TEntityDto"/> from data source.</returns>
        public IEnumerable<ProjectDto> GetAll()
        {
            return _wrongProjectsService.SelectAllProjects();
        }

        /// <summary>
        /// Updates the item in data source with given <paramref name="entityDto"/>.
        /// </summary>
        /// <param name="entityDto">Given entity.</param>
        public void Update(ProjectDto entityDto)
        {
            _wrongProjectsService.ChangeProject(entityDto);
        }

        /// <summary>
        /// Deletes an object with specified value of key in data source.
        /// </summary>
        /// <param name="id">Key to find an object.</param>
        public void Delete(int id)
        {
            _wrongProjectsService.RemoveProject(id);
        }

        /// <summary>
        /// Returns all objects of type <see cref="ProjectDto"/> from data source containing <paramref name="keyword"/> in its properties.
        /// </summary>
        /// <returns>Returns all objects of type <see cref="ProjectDto"/> from data source containing <paramref name="keyword"/> in its properties.</returns>
        public IEnumerable<ProjectDto> GetByKeyword(string keyword)
        {
            return _wrongProjectsService.SelectByKeyword(keyword);
        }
    }
}
