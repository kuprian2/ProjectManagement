using Autofac;
using AutoMapper;
using ProjectManagement.BLL.Contracts.Services;
using ProjectManagement.UI.Models;
using ProjectManagement.UI.Views;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ProjectManagement.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// AutoFac container.
        /// </summary>
        private readonly ILifetimeScope _lifetimeScope;

        /// <summary>
        /// Any implementation of <see cref="IProjectsService"/>.
        /// </summary>
        private readonly IProjectsService _projectsService;

        /// <summary>
        /// Implementation of <see cref="AutoMapper"/>`s <see cref="IMapper"/>.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for MainWindow.
        /// </summary>
        /// <param name="lifetimeScope">AutoFac container.</param>
        /// <param name="projectsService">Any implementation of <see cref="IProjectsService"/>.</param>
        /// <param name="mapper">Implementation of <see cref="AutoMapper"/>`s <see cref="IMapper"/>.</param>
        public MainWindow(ILifetimeScope lifetimeScope, IProjectsService projectsService, IMapper mapper)
        {
            _lifetimeScope = lifetimeScope;
            _projectsService = projectsService;
            _mapper = mapper;
            InitializeComponent();
        }

        /// <summary>
        /// Event handler for element CreateProjectButton "Click" event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateProjectButton_OnClick(object sender, RoutedEventArgs e)
        {
            var w = _lifetimeScope.Resolve<ProjectCreationWindow>();
            w.ShowDialog();
            RefreshListBox();
        }

        /// <summary>
        /// Event handler for element ProjectsListBox "Loaded" event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProjectsListBox_OnLoaded(object sender, RoutedEventArgs e)
        {
            RefreshListBox();
        }

        /// <summary>
        /// Method, refreshing ProjectsListBox and making it up-to-date.
        /// </summary>
        private void RefreshListBox()
        {
            ProjectsListBox.ItemsSource = null;
            var projectDtos = _projectsService.GetAll();
            ProjectsListBox.ItemsSource =
                _mapper.Map<IEnumerable<ProjectFullModel>>(projectDtos).OrderByDescending(x => x.CreatureDate);
        }
    }
}
