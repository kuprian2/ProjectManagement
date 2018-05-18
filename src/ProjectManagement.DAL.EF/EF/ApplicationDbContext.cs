using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagement.DAL.Contracts.Domain;

namespace ProjectManagement.DAL.EF.EF
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(string connectionString)
            :base(connectionString)
        {
        }

        public virtual DbSet<Project> Projects { get; set; }
    }
}
