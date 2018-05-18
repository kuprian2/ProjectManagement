using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Autofac;
using ProjectManagement.BLL.Contracts.Services;
using ProjectManagement.BLL.Services;
using ProjectManagement.DAL.Contracts.Domain;
using ProjectManagement.DAL.Contracts.Repositories;
using ProjectManagement.DAL.EF.EF;
using ProjectManagement.DAL.EF.Repositories;
using ProjectManagement.UI.Views;

namespace ProjectManagement.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var builder = new ContainerBuilder();
            var container = ConfigureServices(builder);

            var mainWindow = container.Resolve<MainWindow>();
            mainWindow.Show();
        }

        private static IContainer ConfigureServices(ContainerBuilder builder)
        {
            builder.UseAutoMapper();

            var conn = ConfigurationManager.ConnectionStrings["ProjectManagementConnection"].ConnectionString;
            //builder.RegisterType<ProjectsRepository>().As<IRepository<Project>>().WithParameter("connectionString", conn);
            builder.RegisterType<ProjectsRepository>().As<IRepository<Project>>();
            builder.RegisterType<ApplicationDbContext>().As<DbContext>().WithParameter("connectionString", conn);
            builder.RegisterType<ProjectsService>().As<IProjectsService>();

            builder.RegisterType<ProjectCreationWindow>();

            builder.RegisterType<MainWindow>();

            return builder.Build();
        }
    }
}
