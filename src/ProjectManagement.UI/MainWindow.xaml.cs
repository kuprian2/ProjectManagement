using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Autofac;
using AutoMapper;
using ProjectManagement.BLL.Contracts.Services;
using ProjectManagement.UI.Models;
using ProjectManagement.UI.Views;

namespace ProjectManagement.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ILifetimeScope _lifetimeScope;
        private readonly IProjectsService _projectsService;
        private readonly IMapper _mapper;

        public MainWindow(ILifetimeScope lifetimeScope, IProjectsService projectsService, IMapper mapper)
        {
            _lifetimeScope = lifetimeScope;
            _projectsService = projectsService;
            _mapper = mapper;
            InitializeComponent();
        }

        private void CreateProjectButton_OnClick(object sender, RoutedEventArgs e)
        {
            var w = _lifetimeScope.Resolve<ProjectCreationWindow>();
            w.ShowDialog();
            RefreshListBox();
        }

        private void ProjectsListBox_OnLoaded(object sender, RoutedEventArgs e)
        {
            RefreshListBox();
        }

        private void RefreshListBox()
        {
            ProjectsListBox.ItemsSource = null;
            var projectDtos = _projectsService.GetAll();
            ProjectsListBox.ItemsSource =
                _mapper.Map<IEnumerable<ProjectFullModel>>(projectDtos).OrderByDescending(x => x.CreatureDate);
        }
    }
}
