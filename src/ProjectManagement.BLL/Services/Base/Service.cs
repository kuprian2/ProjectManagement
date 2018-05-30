using System;
using AutoMapper;
using EnsureThat;
using ProjectManagement.BLL.Contracts.Dto.Base;
using ProjectManagement.BLL.Contracts.Services.Base;
using ProjectManagement.DAL.Contracts.Domain.Base;
using ProjectManagement.DAL.Contracts.Repositories;
using System.Collections.Generic;

namespace ProjectManagement.BLL.Services.Base
{
    /// <summary>
    /// Implementation of <see cref="IService{TEntityDto}"/> in current model.
    /// </summary>
    /// <typeparam name="TEntity">Successor of <see cref="Entity"/></typeparam>
    /// <typeparam name="TEntityDto">Successor of <see cref="EntityDto"/>.</typeparam>
    public abstract class Service<TEntity, TEntityDto> : IService<TEntityDto>
        where TEntityDto : EntityDto
        where TEntity : Entity
    {
        /// <summary>
        /// Instance of <see cref="IRepository{TEntity}"/>.
        /// </summary>
        private readonly IRepository<TEntity> _repository;

        /// <summary>
        /// Instance of <see cref="IMapper"/>.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Creates an insatnce of <see cref="Service{TEntity,TEntityDto}"/>
        /// </summary>
        /// <param name="repository">Instance of <see cref="IRepository{TEntity}"/>.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        protected Service(IRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns an object of type <see cref="TEntityDto"/> with specified value of key from data source.
        /// </summary>
        /// <param name="id">Key to find an object.</param>
        /// <returns>Returns an object with specified value of key from data source.</returns>
        /// <exception cref="ArgumentException"></exception>
        public TEntityDto Get(int id)
        {
            Ensure.Any.IsNotDefault(id);
            var entity = _repository.Get(id);
            return _mapper.Map<TEntityDto>(entity);
        }

        /// <summary>
        /// Returns all objects of type <see cref="TEntityDto"/> from data source.
        /// </summary>
        /// <returns>Returns all objects of type <see cref="TEntityDto"/> from data source.</returns>
        /// <exception cref="NullReferenceException"></exception>
        public IEnumerable<TEntityDto> GetAll()
        {
            var entities = _repository.GetAll();
            Ensure.Any.IsNotNull(entities);
            return _mapper.Map<IEnumerable<TEntityDto>>(entities);
        }

        /// <summary>
        /// Creates an item in data source from a given entity.
        /// </summary>
        /// <param name="entityDto">Given entity.</param>
        /// <exception cref="NullReferenceException"></exception>
        public virtual void Create(TEntityDto entityDto)
        {
            Ensure.Any.IsNotNull(entityDto);
            _repository.Create(_mapper.Map<TEntity>(entityDto));
        }

        /// <summary>
        /// Updates the item in data source with given <paramref name="entityDto"/>.
        /// </summary>
        /// <param name="entityDto">Given entity.</param>
        /// <exception cref="NullReferenceException"></exception>
        public void Update(TEntityDto entityDto)
        {
            Ensure.Any.IsNotNull(entityDto);
            _repository.Update(_mapper.Map<TEntity>(entityDto));
        }

        /// <summary>
        /// Deletes an object with specified value of key in data source.
        /// </summary>
        /// <param name="id">Key to find an object.</param>
        /// <exception cref="ArgumentException"></exception>
        public void Delete(int id)
        {
            Ensure.Any.IsNotDefault(id);
            _repository.Delete(id);
        }
    }
}
