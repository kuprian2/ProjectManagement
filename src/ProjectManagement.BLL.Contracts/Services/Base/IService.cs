using ProjectManagement.BLL.Contracts.Dto.Base;
using System.Collections.Generic;

namespace ProjectManagement.BLL.Contracts.Services.Base
{
    /// <summary>
    /// Generic interface of service in current model.
    /// </summary>
    /// <typeparam name="TEntityDto">Successor of <see cref="EntityDto"/>.</typeparam>
    public interface IService<TEntityDto> where TEntityDto : EntityDto
    {
        /// <summary>
        /// Returns an object of type <see cref="TEntityDto"/> with specified value of key from data source.
        /// </summary>
        /// <param name="id">Key to find an object.</param>
        /// <returns>Returns an object with specified value of key from data source.</returns>
        TEntityDto Get(int id);

        /// <summary>
        /// Returns all objects of type <see cref="TEntityDto"/> from data source.
        /// </summary>
        /// <returns>Returns all objects of type <see cref="TEntityDto"/> from data source.</returns>
        IEnumerable<TEntityDto> GetAll();

        /// <summary>
        /// Creates an item in data source from a given entity.
        /// </summary>
        /// <param name="entityDto">Given entity.</param>
        void Create(TEntityDto entityDto);

        /// <summary>
        /// Updates the item in data source with given <paramref name="entityDto"/>.
        /// </summary>
        /// <param name="entityDto">Given entity.</param>
        void Update(TEntityDto entityDto);

        /// <summary>
        /// Deletes an object with specified value of key in data source.
        /// </summary>
        /// <param name="id">Key to find an object.</param>
        void Delete(int id);
    }
}
