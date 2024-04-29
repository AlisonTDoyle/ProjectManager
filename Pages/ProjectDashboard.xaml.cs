using ProjectManager.Utilities;
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

        public ProjectDashboard()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DatabaseHandler handler = new DatabaseHandler();
            List<Project> projects = handler.FetchProjects();

            // Populate subjects list (incl. option to show all subjects)
            List<string> subjects = new List<string>() { "All Subjects" };

            List<string> databaseSubjects = handler.FetchSubjects();

            foreach (string subject in databaseSubjects)
            {
                subjects.Add(subject);
            }

            PopulateProjects(projects);
            PopulateSelector(subjects);
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
                projects = handler.FetchProjectsBySubject(cbSubjectSelector.SelectedValue.ToString());

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

            if (projectName == "All Subjects")
            {
                MessageBox.Show("test");
                projects = handler.FetchProjects();
                MessageBox.Show(projects.Count.ToString());
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

        private void PopulateSelector(List<string> subjects)
        {
            cbSubjectSelector.ItemsSource = null;
            cbSubjectSelector.ItemsSource = subjects;
        }
    }
}
