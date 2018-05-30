using Autofac;
using ProjectManagement.BLL.Contracts.Services;
using ProjectManagement.BLL.Services;
using ProjectManagement.DAL.Contracts.Domain;
using ProjectManagement.DAL.Contracts.Repositories;
using ProjectManagement.DAL.EF.EF;
using ProjectManagement.DAL.EF.Repositories;
using ProjectManagement.UI.Views;
using System.Configuration;
using System.Data.Entity;
using System.Windows;

namespace ProjectManagement.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Method called on Application start.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var builder = new ContainerBuilder();
            var container = ConfigureServices(builder);

            var mainWindow = container.Resolve<MainWindow>();
            mainWindow.Show();
        }

        /// <summary>
        /// Method to build <see cref="Autofac"/> container.
        /// </summary>
        /// <param name="builder"><see cref="Autofac"/>`s container builder.</param>
        /// <returns>Builded container.</returns>
        private static IContainer ConfigureServices(ContainerBuilder builder)
        {
            builder.UseAutoMapper();

            var conn = ConfigurationManager.ConnectionStrings["ProjectManagementConnection"].ConnectionString;
            //builder.RegisterType<ProjectsRepository>().As<IRepository<Project>>().WithParameter("connectionString", conn);
            builder.RegisterType<ProjectsRepository>().As<IRepository<Project>>();
            builder.RegisterType<ApplicationDbContext>().As<DbContext>().WithParameter("connectionString", conn);
            builder.RegisterType<AdaptedWrongProjectsService>().As<IProjectsService>();

            builder.RegisterType<ProjectCreationWindow>();

            builder.RegisterType<MainWindow>();

            return builder.Build();
        }
    }
}
