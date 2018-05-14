using AutoMapper;
using EnsureThat;
using ProjectManagement.BLL.Contracts.Dto.Base;
using ProjectManagement.BLL.Contracts.Services.Base;
using ProjectManagement.DAL.Contracts.Domain.Base;
using System.Collections.Generic;
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
            throw new System.NotImplementedException();
        }

        public IEnumerable<TEntityDto> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void Create(TEntityDto entityDto)
        {
            throw new System.NotImplementedException();
        }

        public void Update(TEntityDto entityDto)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
