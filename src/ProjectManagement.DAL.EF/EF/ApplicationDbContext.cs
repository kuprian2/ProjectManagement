using ProjectManagement.DAL.Contracts.Domain;
using System.Data.Entity;

namespace ProjectManagement.DAL.EF.EF
{
    /// <summary>
    /// Type of <see cref="DbContext"/> for current model.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(string connectionString)
            :base(connectionString)
        {
        }

        public virtual DbSet<Project> Projects { get; set; }
    }
}
