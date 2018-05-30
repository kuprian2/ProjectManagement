using ProjectManagement.DAL.Contracts.Domain;
using ProjectManagement.DAL.Contracts.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ProjectManagement.DAL.EF.Repositories
{
    /// <summary>
    /// Implementation of <see cref="IRepository{TEntity}"/> parametrized with <see cref="Project"/> using Entity Framework 6.
    /// </summary>
    public class ProjectsRepository : IRepository<Project>
    {
        protected readonly DbSet<Project> DbSet;
        protected readonly DbContext DbContext;

        public ProjectsRepository(DbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = dbContext.Set<Project>();
        }

        /// <summary>
        /// Returns an object of type <see cref="Project"/> with specified value of key from data source.
        /// </summary>
        /// <param name="id">Key to find an object.</param>
        /// <returns>Returns an object with specified value of key from data source.</returns>
        public Project Get(int id)
        {
            return DbSet.Find(id);
        }

        /// <summary>
        /// Returns all objects of type <see cref="Project"/> from data source.
        /// </summary>
        /// <returns>Returns all objects of type <see cref="Project"/> from data source.</returns>
        public IEnumerable<Project> GetAll()
        {
            return DbSet.AsEnumerable();
        }

        /// <summary>
        /// Creates the item in data source with given <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity">Given entity.</param>
        public void Create(Project entity)
        {
            DbSet.Add(entity);
            DbContext.SaveChanges();
        }

        /// <summary>
        /// Updates the item in data source with given <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity">Given entity.</param>
        public void Update(Project entity)
        {
            DbSet.Attach(entity);
            DbContext.SaveChanges();
        }

        /// <summary>
        /// Returns an object with specified value of key in data source.
        /// </summary>
        /// <param name="id">Key to find an object.</param>
        public void Delete(int id)
        {
            var entity = DbSet.Find(id);

            if (entity != null)
            {
                DbSet.Remove(entity);
            }
            DbContext.SaveChanges();
        }
    }
}
