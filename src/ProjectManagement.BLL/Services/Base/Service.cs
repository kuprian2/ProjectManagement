using AutoMapper;
using EnsureThat;
using ProjectManagement.BLL.Contracts.Dto.Base;
using ProjectManagement.BLL.Contracts.Services.Base;
using ProjectManagement.DAL.Contracts.Domain.Base;
using System.Collections.Generic;
using System.Linq;
using ProjectManagement.DAL.Contracts.Repositories;

namespace ProjectManagement.BLL.Services.Base
{
    public abstract class Service<TEntity, TEntityDto> : IService<TEntityDto>
        where TEntityDto : EntityDto
        where TEntity : Entity
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        protected Service(IRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public TEntityDto Get(int id)
        {
            Ensure.Any.IsNotDefault(id);
            var entity = _repository.Get(id);
            return _mapper.Map<TEntityDto>(entity);
        }

        public IEnumerable<TEntityDto> GetAll()
        {
            var entities = _repository.GetAll();
            Ensure.Any.IsNotNull(entities);
            return _mapper.Map<IEnumerable<TEntityDto>>(entities);
        }

        public void Create(TEntityDto entityDto)
        {
            Ensure.Any.IsNotNull(entityDto);
            entityDto.Id = _repository.GetAll().OrderBy(x => x.Id).Last().Id + 1;
            _repository.Create(_mapper.Map<TEntity>(entityDto));
        }

        public void Update(TEntityDto entityDto)
        {
            Ensure.Any.IsNotNull(entityDto);
            _repository.Update(_mapper.Map<TEntity>(entityDto));
        }

        public void Delete(int id)
        {
            Ensure.Any.IsNotDefault(id);
            _repository.Delete(id);
        }
    }
}
