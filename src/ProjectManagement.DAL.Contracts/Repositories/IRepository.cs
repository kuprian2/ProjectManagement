using ProjectManagement.DAL.Contracts.Domain.Base;
using System.Collections.Generic;

namespace ProjectManagement.DAL.Contracts.Repositories
{
    public interface IRepository<TEntity> where TEntity: Entity
    {
        TEntity Get(int id);

        IEnumerable<TEntity> GetAll();
        
        void Create(TEntity entity);

        void Update(TEntity entity);

        void Delete(int id);
    }
}
