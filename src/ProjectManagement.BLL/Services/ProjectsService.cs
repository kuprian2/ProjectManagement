using AutoMapper;
using EnsureThat;
using ProjectManagement.BLL.Contracts.Dto;
using ProjectManagement.BLL.Contracts.Services;
using ProjectManagement.BLL.Services.Base;
using ProjectManagement.DAL.Contracts.Domain;
using ProjectManagement.DAL.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManagement.BLL.Services
{
    /// <summary>
    /// Implementation of <see cref="IProjectsService"/>
    /// </summary>
    public class ProjectsService : Service<Project, ProjectDto>, IProjectsService
    {
        /// <summary>
        /// Creates an insatnce of <see cref="ProjectsService"/>
        /// </summary>
        /// <param name="repository">Instance of <see cref="IRepository{Project}"/>.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        public ProjectsService(IRepository<Project> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        /// <summary>
        /// Returns all objects of type <see cref="ProjectDto"/> from data source containing <paramref name="keyword"/> in its properties.
        /// </summary>
        /// <returns>Returns all objects of type <see cref="ProjectDto"/> from data source containing <paramref name="keyword"/> in its properties.</returns>
        /// <exception cref="ArgumentException"></exception>
        public IEnumerable<ProjectDto> GetByKeyword(string keyword)
        {
            Ensure.String.IsNotNullOrWhiteSpace(keyword);
            var lowerKeyword = keyword.ToLower();
            return GetAll().Where(x =>
                x.Information.ToLower().Contains(lowerKeyword) || x.Name.ToLower().Contains(lowerKeyword) ||
                x.ShortInformation.ToLower().Contains(lowerKeyword))
                .ToList();
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
        /// <exception cref="NullReferenceException"></exception>
        public override void Create(ProjectDto entityDto)
        {
            base.Create(entityDto);
            OnCreated();
        }
    }
}
