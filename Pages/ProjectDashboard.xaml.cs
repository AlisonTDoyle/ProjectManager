using ProjectManager.Classes;
using ProjectManager.Utilities;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ProjectManager.Pages
{
    /// <summary>
    /// Interaction logic for ProjectDashboard.xaml
    /// </summary>
    public partial class ProjectDashboard : Page
    {
        private List<string> _subjects = new List<string>();

        public ProjectDashboard()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DatabaseHandler handler = new DatabaseHandler();
            List<Project> projects = handler.FetchProjects();

            PopulateProjects(projects);
            PopulateSubjectsList();

            tbxProjectSearchBox.Text = "Enter project name...";
        }

        private void dgProjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.Main.Navigate(new ProjectDetails((int)dgProjects.SelectedValue));
        }

        private void cbSubjectSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DatabaseHandler handler = new DatabaseHandler();
            List<Project> projects = new List<Project>();

            //MessageBox.Show(cbSubjectSelector.SelectedValue.ToString());
            if (cbSubjectSelector.SelectedValue.ToString() != null)
            {
                if (cbSubjectSelector.SelectedValue.ToString() == "All Subjects")
                {
                    projects = handler.FetchProjects();
                }
                else
                {
                    projects = handler.FetchProjectsBySubject(cbSubjectSelector.SelectedValue.ToString());
                }

                PopulateProjects(projects);
            }
        }

        private void btnCreateNewProject_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.Main.Navigate(new NewProjectCreator());
        }

        private void tbxProjectSearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            DatabaseHandler handler = new DatabaseHandler();
            List<Project> projects = new List<Project>();
            string projectName = tbxProjectSearchBox.Text;

            if ((projectName == "All Subjects") || (tbxProjectSearchBox.Text == "Enter project name..."))
            {
                projects = handler.FetchProjects();
            }
            else
            {
                projects = handler.FetchProjectsByName(projectName);
            }

            PopulateProjects(projects);
        }

        private void PopulateProjects(List<Project> projects)
        {
            dgProjects.ItemsSource = null;
            dgProjects.ItemsSource = projects;
        }

        private void PopulateSubjectsList()
        {
            JsonHandler jsonHandler = new JsonHandler();
            List<Subject> subjects = jsonHandler.ReadInSubjects();

            // Create list of subjects
            _subjects.Add("All Subjects");
            for (int i = 0; i < subjects.Count; i++)
            {
                _subjects.Add(subjects[i].Name);
            }

            cbSubjectSelector.ItemsSource = null;
            cbSubjectSelector.ItemsSource = _subjects;
        }

        private void tbxProjectSearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(tbxProjectSearchBox.Text)) 
            {
                tbxProjectSearchBox.Text = "Enter project name...";
            }
        }

        private void tbxProjectSearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbxProjectSearchBox.Text == "Enter project name...")
            {
                tbxProjectSearchBox.Text = "";
            }
        }
    }
}
