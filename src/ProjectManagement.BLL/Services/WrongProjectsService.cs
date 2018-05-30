using AutoMapper;
using EnsureThat;
using ProjectManagement.BLL.Contracts.Dto;
using ProjectManagement.DAL.Contracts.Domain;
using ProjectManagement.DAL.Contracts.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManagement.BLL.Services
{
    /// <summary>
    /// Incorrectly made projects service.
    /// </summary>
    public class WrongProjectsService
    {
        /// <summary>
        /// Instance of <see cref="IRepository{TEntity}"/>.
        /// </summary>
        private readonly IRepository<Project> _repository;

        /// <summary>
        /// Instance of <see cref="IMapper"/>.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Creates an insatnce of <see cref="WrongProjectsService"/>
        /// </summary>
        /// <param name="repository">Instance of <see cref="IRepository{TEntity}"/>.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        public WrongProjectsService(IRepository<Project> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns an object of type <see cref="TEntityDto"/> with specified value of key from data source.
        /// </summary>
        /// <param name="id">Key to find an object.</param>
        /// <returns>Returns an object with specified value of key from data source.</returns>
        public ProjectDto SelectProjectById(int id)
        {
            Ensure.Any.IsNotDefault(id);
            var entity = _repository.Get(id);
            return _mapper.Map<ProjectDto>(entity);
        }

        /// <summary>
        /// Returns all objects of type <see cref="TEntityDto"/> from data source.
        /// </summary>
        /// <returns>Returns all objects of type <see cref="TEntityDto"/> from data source.</returns>
        public IEnumerable<ProjectDto> SelectAllProjects()
        {
            var entities = _repository.GetAll();
            Ensure.Any.IsNotNull(entities);
            return _mapper.Map<IEnumerable<ProjectDto>>(entities);
        }

        /// <summary>
        /// Creates an item in data source from a given entity.
        /// </summary>
        /// <param name="entityDto">Given entity.</param>
        public void CreateProject(ProjectDto entityDto)
        {
            Ensure.Any.IsNotNull(entityDto);
            _repository.Create(_mapper.Map<Project>(entityDto));
        }

        /// <summary>
        /// Updates the item in data source with given <paramref name="entityDto"/>.
        /// </summary>
        /// <param name="entityDto">Given entity.</param>
        public void ChangeProject(ProjectDto entityDto)
        {
            Ensure.Any.IsNotNull(entityDto);
            _repository.Update(_mapper.Map<Project>(entityDto));
        }

        /// <summary>
        /// Deletes an object with specified value of key in data source.
        /// </summary>
        /// <param name="id">Key to find an object.</param>
        public void RemoveProject(int id)
        {
            Ensure.Any.IsNotDefault(id);
            _repository.Delete(id);
        }

        /// <summary>
        /// Returns all objects of type <see cref="ProjectDto"/> from data source containing <paramref name="keyword"/> in its properties.
        /// </summary>
        /// <returns>Returns all objects of type <see cref="ProjectDto"/> from data source containing <paramref name="keyword"/> in its properties.</returns>
        public IEnumerable<ProjectDto> SelectByKeyword(string keyword)
        {
            Ensure.String.IsNotNullOrWhiteSpace(keyword);
            var lowerKeyword = keyword.ToLower();
            return SelectAllProjects().Where(x =>
                    x.Information.ToLower().Contains(lowerKeyword) || x.Name.ToLower().Contains(lowerKeyword) ||
                    x.ShortInformation.ToLower().Contains(lowerKeyword))
                .ToList();
        }
    }
}
