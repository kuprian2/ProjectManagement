using System;
using System.Globalization;
using System.Windows;
using System.Windows.Documents;
using ProjectManagement.BLL.Contracts.Dto;
using ProjectManagement.BLL.Contracts.Services;

namespace ProjectManagement.UI.Views
{
    /// <summary>
    /// Interaction logic for ProjectCreationWindow.xaml
    /// </summary>
    public partial class ProjectCreationWindow : Window
    {
        private readonly IProjectsService _projectsService;

        public ProjectCreationWindow(IProjectsService projectsService)
        {
            _projectsService = projectsService;
            InitializeComponent();
        }

        private void CreatureDateTextBox_OnLoaded(object sender, RoutedEventArgs e)
        {
            CreatureDateTextBox.Text = DateTime.Now.ToString(CultureInfo.CurrentCulture);
        }

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
