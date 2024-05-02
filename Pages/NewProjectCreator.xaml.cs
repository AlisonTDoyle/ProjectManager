using ProjectManager.Classes;
using ProjectManager.Utilities;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ProjectManager.Pages
{
    /// <summary>
    /// Interaction logic for NewProjectCreator.xaml
    /// </summary>
    public partial class NewProjectCreator : Page
    {
        private List<Task> _projectTasks = new List<Task>();
        private List<Subject> _subjects = new List<Subject>();

        public NewProjectCreator()
        {
            InitializeComponent();
        }

        private void btnAddTask_Click(object sender, RoutedEventArgs e)
        {
            AddTaskToDataGridView();
        }

        private void btnUpdateTask_Click(object sender, RoutedEventArgs e)
        {
            int selectedTaskIndex = dgProjectTasks.SelectedIndex != null ? dgProjectTasks.SelectedIndex : -1;
            UpdateTask(selectedTaskIndex);
        }

        private void btnDeleteTask_Click(object sender, RoutedEventArgs e)
        {
            DeleteTask();
        }

        private void dgProjectTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgProjectTasks.SelectedItem != null)
            {
                PopulateTaskFormWithTaskData();
            }
        }

        private void btnCreateProject_Click(object sender, RoutedEventArgs e)
        {
            CreateProject();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateSubjectsList();
        }

        #region Methods
        private void AddTaskToDataGridView()
        {
            // Make sure necessary fields are filled in
            bool namedEntered = String.IsNullOrEmpty(tbxTaskName.Text);

            if (namedEntered == false)
            {
                // Create task based on info provided
                Task newTask = new Task()
                {
                    Name = tbxTaskName.Text,
                    Description = tbxTaskDescription.Text,
                    DueDate = dpTaskDueDate.SelectedDate,
                    Status = 0
                };

                // Add task to list + refresh dgv
                _projectTasks.Add(newTask);
                RefreshDatagrid();

                // Reset form
                ClearTaskForm();
            }
            else
            {
                MessageBox.Show("Make sure task has at least a name before adding it to project", "Error");
            }
        }

        private void RefreshDatagrid()
        {
            dgProjectTasks.ItemsSource = null;
            dgProjectTasks.ItemsSource = _projectTasks;
        }

        private void ClearTaskForm()
        {
            tbxTaskName.Text = "";
            tbxTaskDescription.Text = "";
            dpTaskDueDate.Text = "";
        }

        private void PopulateTaskFormWithTaskData()
        {
            Task selectedTask = dgProjectTasks.SelectedItem as Task;

            if (selectedTask != null)
            {
                tbxTaskName.Text = selectedTask.Name;
                tbxTaskDescription.Text = selectedTask.Description;
                dpTaskDueDate.SelectedDate = selectedTask.DueDate;
            }
        }

        private void DeleteTask()
        {
            int selectedItemIndex = dgProjectTasks.SelectedIndex != null ? dgProjectTasks.SelectedIndex : -1;

            if (selectedItemIndex != -1)
            {
                // Remove item from list
                _projectTasks.RemoveAt(selectedItemIndex);

                // Update creator ui
                RefreshDatagrid();
                ClearTaskForm();
            }
            else
            {
                MessageBox.Show("Make sure you have selected an item to delete", "Error");
            }
        }

        private void UpdateTask(int selectedTaskIndex)
        {
            if ((selectedTaskIndex != -1) && (string.IsNullOrEmpty(tbxTaskName.Text) == false))
            {
                _projectTasks[selectedTaskIndex].Name = tbxTaskName.Text;
                _projectTasks[selectedTaskIndex].Description = tbxTaskDescription.Text;
                _projectTasks[selectedTaskIndex].DueDate = dpTaskDueDate.SelectedDate;

                RefreshDatagrid();
                ClearTaskForm();
            }
            else if (selectedTaskIndex == -1)
            {
                MessageBox.Show("Make sure you have selected an item to update", "Error");
            }
            else
            {
                MessageBox.Show("Make sure necessary feilds are filled in", "Error");
            }
        }

        private void CreateProject()
        {
            // Initialising project values
            string projectName = tbxProjectName.Text;
            string subject = cbxSubject.Text;
            DateTime dueDate = dpProjectDueDate.SelectedDate != null ? (DateTime)dpProjectDueDate.SelectedDate : new DateTime(2000, 01, 01);

            // Make sure necissary feilds are filled in
            if ((!String.IsNullOrEmpty(projectName)) && (!String.IsNullOrEmpty(subject)))
            {
                // Create project object
                Project newProject = new Project()
                {
                    Name = projectName,
                    Subject = subject,
                    DueDate = dueDate
                };

                // Add project and its tasks to database
                DatabaseHandler handler = new DatabaseHandler();
                handler.CreateProject(newProject, _projectTasks);
            }
            else
            {
                MessageBox.Show("Make sure project name and subject are filled out", "Error");
            }
        }

        private void PopulateSubjectsList()
        {
            JsonHandler jsonHandler = new JsonHandler();
            _subjects = jsonHandler.ReadInSubjects();

            cbxSubject.ItemsSource = null;
            cbxSubject.ItemsSource = _subjects;
        }
        #endregion
    }
}
