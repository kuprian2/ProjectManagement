using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ProjectManagement.DAL.Contracts.Domain;
using ProjectManagement.DAL.Contracts.Repositories;

namespace ProjectManagement.DAL.EF.Repositories
{
    public class ProjectsRepository : IRepository<Project>
    {
        protected readonly DbSet<Project> DbSet;
        protected readonly DbContext DbContext;

        public ProjectsRepository(DbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = dbContext.Set<Project>();
        }

        public Project Get(int id)
        {
            return DbSet.Find(id);
        }

        public IEnumerable<Project> GetAll()
        {
            return DbSet.AsEnumerable();
        }

        public void Create(Project entity)
        {
            DbSet.Add(entity);
            DbContext.SaveChanges();
        }

        public void Update(Project entity)
        {
            DbSet.Attach(entity);
            DbContext.SaveChanges();
        }

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
