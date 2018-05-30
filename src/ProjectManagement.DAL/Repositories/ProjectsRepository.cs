using ProjectManagement.DAL.Contracts.Domain;
using ProjectManagement.DAL.Contracts.Exceptions;
using ProjectManagement.DAL.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ProjectManagement.DAL.Repositories
{
    /// <summary>
    /// Implementation of <see cref="IRepository{TEntity}"/> parametrized with <see cref="Project"/> using ADO.NET.
    /// </summary>
    public class ProjectsRepository : IRepository<Project>
    {
        private readonly SqlConnection _connection;

        /// <summary>
        /// Creates an instance of <see cref="ProjectsRepository"/> using specific connection string.
        /// </summary>
        /// <param name="connectionString"></param>
        public ProjectsRepository(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        /// <summary>
        /// Creates an instance of <see cref="ProjectsRepository"/> using default connection string.
        /// </summary>
        public ProjectsRepository() : this(@"Data Source=.\SQLEXPRESS;Initial Catalog=ProjectManagement;Integrated Security=True")
        {
        }

        /// <summary>
        /// Creates the item in data source with given <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity">Given entity.</param>
        /// <exception cref="DalException"></exception>
        public void Create(Project entity)
        {
            try
            {
                _connection.Open();

                const string sqlExpression =
                    "INSERT INTO Projects (Id, Name, ShortInformation, Information, CreatureDate)" +
                    "VALUES (@id, @name, @shortInfo, @info, @creatureDate)";
                var command = new SqlCommand(sqlExpression, _connection);
                var parameters = new[]
                {
                    new SqlParameter("@id", entity.Id),
                    new SqlParameter("@name", entity.Name),
                    new SqlParameter("@shortInfo", entity.ShortInformation),
                    new SqlParameter("@info", entity.Information),
                    new SqlParameter("@creatureDate", entity.CreatureDate)
                };
                command.Parameters.AddRange(parameters);

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new DalException("Exception on a Data Access Layer. See inner exception.", e);
            }
            finally
            {
                _connection.Close();
            }
        }

        /// <summary>
        /// Returns an object with specified value of key in data source.
        /// </summary>
        /// <param name="id">Key to find an object.</param>
        /// <exception cref="DalException"></exception>
        public void Delete(int id)
        {
            try
            {
                _connection.Open();

                const string sqlExpression = "DELETE FROM Projects WHERE Id = @id";
                var command = new SqlCommand(sqlExpression, _connection);
                var idParameter = new SqlParameter("@id", id);
                command.Parameters.Add(idParameter);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new DalException("Exception on a Data Access Layer. See inner exception.", e);
            }
            finally
            {
                _connection.Close();
            }
        }

        /// <summary>
        /// Returns an object of type <see cref="Project"/> with specified value of key from data source.
        /// </summary>
        /// <param name="id">Key to find an object.</param>
        /// <returns>Returns an object with specified value of key from data source.</returns>
        /// <exception cref="DalException"></exception>
        public Project Get(int id)
        {
            try
            {
                _connection.Open();

                const string sqlExpression =
                    "SELECT Id, Name, ShortInformation, Information, CreatureDate FROM Projects WHERE Id = @id;";
                var command = new SqlCommand(sqlExpression, _connection);
                var idParameter = new SqlParameter("@id", id);
                command.Parameters.Add(idParameter);

                var reader = command.ExecuteReader();

                if (!reader.Read()) return null;

                var project = new Project
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    ShortInformation = reader.GetString(2),
                    Information = reader.GetString(3),
                    CreatureDate = reader.GetDateTime(4)
                };

                return project;
            }
            catch (Exception e)
            {
                throw new DalException("Exception on a Data Access Layer. See inner exception.", e);
            }
            finally
            {
                _connection.Close();
            }
        }

        /// <summary>
        /// Returns all objects of type <see cref="Project"/> from data source.
        /// </summary>
        /// <returns>Returns all objects of type <see cref="Project"/> from data source.</returns>
        /// <exception cref="DalException"></exception>
        public IEnumerable<Project> GetAll()
        {
            try
            {
                var projects = new List<Project>();

                _connection.Open();

                const string sqlExpression = "SELECT Id, Name, ShortInformation, Information, CreatureDate FROM Projects";
                var command = new SqlCommand(sqlExpression, _connection);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    projects.Add(new Project
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        ShortInformation = reader.GetString(2),
                        Information = reader.GetString(3),
                        CreatureDate = reader.GetDateTime(4)
                    });
                }

                return projects;
            }
            catch (Exception e)
            {
                throw new DalException("Exception on a Data Access Layer. See inner exception.", e);
            }
            finally
            {
                _connection.Close();
            }
        }

        /// <summary>
        /// Updates the item in data source with given <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity">Given entity.</param>
        /// <exception cref="DalException"></exception>
        public void Update(Project entity)
        {
            try
            {
                _connection.Open();

                const string sqlExpression =
                    "UPDATE Projects" +
                    "SET Name = @name, ShortInformation = @shortInfo, Information = @info, CreatureDate = @date" +
                    "WHERE Id = @id";
                var command = new SqlCommand(sqlExpression, _connection);
                var parameters = new[]
                {
                    new SqlParameter("@id", entity.Id),
                    new SqlParameter("@name", entity.Name),
                    new SqlParameter("@shortInfo", entity.ShortInformation),
                    new SqlParameter("@info", entity.Information),
                    new SqlParameter("@creatureDate", entity.CreatureDate)
                };
                command.Parameters.AddRange(parameters);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new DalException("Exception on a Data Access Layer. See inner exception.", e);
            }
            finally
            {
                _connection.Close();
            }
        }
    }
}
