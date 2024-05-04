using ProjectManager.Classes;
using ProjectManager.Pages;
using ProjectManager.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjectManager.Windows
{
    /// <summary>
    /// Interaction logic for ProjectDetailsManager.xaml
    /// </summary>
    public partial class ProjectDetailsManager : Window
    {
        private int _projectId;
        private ProjectDetails _parentPage;

        public ProjectDetailsManager(int projectId, ProjectDetails parent)
        {
            InitializeComponent();

            _projectId = projectId;
            _parentPage = parent;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateComboBoxWithSubjects();
            PopulateForm();
        }

        private void btnUpdateProject_Click(object sender, RoutedEventArgs e)
        {
            UpdateProject();    
        }

        private void PopulateComboBoxWithSubjects()
        {
            // Get subjects from file
            JsonHandler jsonHandler = new JsonHandler();
            List<Subject> subjects = jsonHandler.ReadInSubjects();

            List<string> subjectNames = new List<string>();
            for (int i = 0; i < subjects.Count; i++)
            {
                subjectNames.Add(subjects[i].Name);
            }

            // Populate combobox
            cbSubject.ItemsSource = null;
            cbSubject.ItemsSource = subjectNames;
        }

        private void PopulateForm()
        {
            // Get project info
            DatabaseHandler databaseHandler = new DatabaseHandler();
            Project project = databaseHandler.FetchProjectById(_projectId);

            // Put info into fields
            tbxName.Text = project.Name;
            cbSubject.Text = project.Subject;
            dpDueDate.SelectedDate = project.DueDate;
        }

        private void UpdateProject()
        {
            DatabaseHandler databaseHandler = new DatabaseHandler();
            string name = tbxName.Text;
            string subject = cbSubject.Text;
            DateTime? date = dpDueDate.SelectedDate;

            if ((!String.IsNullOrEmpty(name)) && (!String.IsNullOrEmpty(subject)) && (date != null))
            {
                databaseHandler.UpdateProject(_projectId, name, subject, (DateTime)date);

                // Refresh datagrid in parent
                _parentPage.PopulateProjectDetails();
            }
            else
            {
                MessageBox.Show("Make sure a name, subject and due day are entered");
            }
        }
    }
}
