using ProjectManager.Utilities;
using ProjectManager.Windows;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using LiveCharts;

namespace ProjectManager.Pages
{
    /// <summary>
    /// Interaction logic for ProjectDetails.xaml
    /// </summary>
    public partial class ProjectDetails : Page
    {
        private readonly int _projectId;

        public SeriesCollection SeriesCollection { get; set; }

        public ProjectDetails(int projectId)
        {
            InitializeComponent();
            _projectId = projectId;
        }

        private void btnDeleteProject_Click(object sender, RoutedEventArgs e)
        {
            DeleteProject();
        }

        private void DeleteProject()
        {
            // Double check with user they want to delete project
            if (MessageBox.Show("Are you sure you want to delete this project? This action is ireverable", "Error", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
            {
                // Remove project and associated tasks from database
                DatabaseHandler handler = new DatabaseHandler();
                handler.RemoveProject(_projectId);

                // Move user back to dashboard
                MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                mainWindow.Main.Navigate(new ProjectDashboard());
            }
        }

        private void btnNewTask_Click(object sender, RoutedEventArgs e)
        {
            TaskCreator taskCreator = new TaskCreator(_projectId, this);
            taskCreator.Show();
        }

        public void RefreshProjectTasks()
        {
            // Get data from database
            DatabaseHandler handler = new DatabaseHandler();
            List<Task> projectTasks = handler.FetchTasks(_projectId);

            // Repopulate datagrid
            dgProjectTasks.ItemsSource = null;
            dgProjectTasks.ItemsSource = projectTasks;

            // Refresh doughnut chart
            LiveChartsHandler liveChartsHandler = new LiveChartsHandler();
            SeriesCollection = liveChartsHandler.createTaskChartData(projectTasks);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Fetch project details
            DatabaseHandler handler = new DatabaseHandler();
            Project project = (Project)handler.FetchProjectById(_projectId);
            List<Task> projectTasks = handler.FetchTasks(_projectId);

            // Display project details
            tblkProjectName.Text = project.Name;
            tblkSubject.Text = project.Subject;
            DateTime test = (DateTime)project.DueDate;
            string test2 = test.ToString("dd MMMM yyyy");
            tblkDueDate.Text = test2;

            RefreshProjectTasks();

            DataContext = this;
        }

        private void DeleteItem()
        {
            object selectedTask = dgProjectTasks.SelectedItem;

            if (selectedTask != null)
            {
                Task task = selectedTask as Task;

                // Remove item from list
                DatabaseHandler handler = new DatabaseHandler();
                handler.RemoveTask(task.Id);

                // Update creator ui
                RefreshProjectTasks();
            }
            else
            {
                MessageBox.Show("Make sure you have selected an item to delete", "Error");
            }
        }

        private void btnDeleteTask_Click(object sender, RoutedEventArgs e)
        {
            DeleteItem();
        }

        private void btnEditTask_Click(object sender, RoutedEventArgs e)
        {
            OpenTaskEditor();
        }

        private void OpenTaskEditor()
        {
            Task taskToEdit = dgProjectTasks.SelectedItem as Task;

            if (taskToEdit != null)
            {
                TaskCreator taskCreator = new TaskCreator(taskToEdit, this);
                taskCreator.Show();
            }
        }

        private void btnChangeTaskStatus_Click(object sender, RoutedEventArgs e)
        {
            UpdateTaskStatus();
        }

        private void UpdateTaskStatus()
        {
            Task selectedTask = (Task)dgProjectTasks.SelectedItem;

            if (selectedTask != null)
            {
                // Increase status
                int newStatus = 1;
                newStatus += (int)selectedTask.Status;

                // If increased status is valid, update database
                if (newStatus < 3)
                {
                    DatabaseHandler handler = new DatabaseHandler();
                    handler.UpdateProjectTaskStatus(selectedTask.Id, newStatus);

                    RefreshProjectTasks();
                }
            } 
            else
            {
                MessageBox.Show("Unable to update project's status", "Error");
            }
        }
    }
}
