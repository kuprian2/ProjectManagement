using ProjectManagement.BLL.Contracts.Dto;
using ProjectManagement.BLL.Contracts.Services.Base;
using System;
using System.Collections.Generic;

namespace ProjectManagement.BLL.Contracts.Services
{
    public interface IProjectsService : IService<ProjectDto>
    {
        /// <summary>
        /// Returns all objects of type <see cref="ProjectDto"/> from data source containing <paramref name="keyword"/> in its properties.
        /// </summary>
        /// <returns>Returns all objects of type <see cref="ProjectDto"/> from data source containing <paramref name="keyword"/> in its properties.</returns>
        IEnumerable<ProjectDto> GetByKeyword(string keyword);

        /// <summary>
        /// Occurs when project was successfully created.
        /// </summary>
        event EventHandler<EventArgs> Created;
    }
}
