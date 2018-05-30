using ProjectManagement.BLL.Contracts.Dto;
using ProjectManagement.BLL.Contracts.Services;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Documents;

namespace ProjectManagement.UI.Views
{
    /// <summary>
    /// Interaction logic for ProjectCreationWindow.xaml
    /// </summary>
    public partial class ProjectCreationWindow : Window
    {
        /// <summary>
        /// Any implementation of <see cref="IProjectsService"/>.
        /// </summary>
        private readonly IProjectsService _projectsService;

        /// <summary>
        /// Constructor for ProjectCreationWindow.
        /// </summary>
        /// <param name="projectsService">Any implementation of <see cref="IProjectsService"/>.</param>
        public ProjectCreationWindow(IProjectsService projectsService)
        {
            _projectsService = projectsService;
            _projectsService.Created += (sender, args) => MessageBox.Show("New project added!");
            InitializeComponent();
        }

        /// <summary>
        /// Event handler for element CreatureDateTextBox "Loaded" event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreatureDateTextBox_OnLoaded(object sender, RoutedEventArgs e)
        {
            CreatureDateTextBox.Text = DateTime.Now.ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Event handler for element CreateButton "Click" event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CreatureDateTextBox.Text) ||
                string.IsNullOrWhiteSpace(ShortInfoTextBox.Text) ||
                string.IsNullOrWhiteSpace(ProjectNameTextBox.Text))
            {
                MessageBox.Show("Fields 'Creature date', 'Short info' and 'Project name' should not be empty.");
                return;
            }

            if (!DateTime.TryParse(CreatureDateTextBox.Text, out var creatureDate))
            {
                MessageBox.Show("Incorrect creature date.");
                return;
            }

            _projectsService.Create(new ProjectDto
            {
                ShortInformation = ShortInfoTextBox.Text,
                CreatureDate = creatureDate,
                Name = ProjectNameTextBox.Text,
                Information = new TextRange(InformationRichTextBox.Document.ContentStart,
                    InformationRichTextBox.Document.ContentEnd).Text
            });

            this.Close();
        }
    }
}
