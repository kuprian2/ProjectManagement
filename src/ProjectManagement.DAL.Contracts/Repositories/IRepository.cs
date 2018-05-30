using ProjectManagement.DAL.Contracts.Domain.Base;
using System.Collections.Generic;

namespace ProjectManagement.DAL.Contracts.Repositories
{
    /// <summary>
    /// Generic interface for repository pattern.
    /// </summary>
    /// <typeparam name="TEntity">Successor of <see cref="Entity"/>.</typeparam>
    public interface IRepository<TEntity> where TEntity: Entity
    {
        /// <summary>
        /// Returns an object of type <see cref="TEntity"/> with specified value of key from data source.
        /// </summary>
        /// <param name="id">Key to find an object.</param>
        /// <returns>Returns an object with specified value of key from data source.</returns>
        TEntity Get(int id);

        /// <summary>
        /// Returns all objects of type <see cref="TEntity"/> from data source.
        /// </summary>
        /// <returns>Returns all objects of type <see cref="TEntity"/> from data source.</returns>
        IEnumerable<TEntity> GetAll();
        
        /// <summary>
        /// Creates an item in data source from a given entity.
        /// </summary>
        /// <param name="entity">Given entity.</param>
        void Create(TEntity entity);

        /// <summary>
        /// Updates the item in data source with given <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity">Given entity.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Deletes an object with specified value of key in data source.
        /// </summary>
        /// <param name="id">Key to find an object.</param>
        void Delete(int id);
    }
}
