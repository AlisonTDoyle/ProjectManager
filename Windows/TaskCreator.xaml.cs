using LiveChartsCore.SkiaSharpView.Painting;
using ProjectManager.Pages;
using ProjectManager.Utilities;
using System;
using System.Windows;

namespace ProjectManager.Windows
{
    /// <summary>
    /// Interaction logic for TaskCreator.xaml
    /// </summary>
    public partial class TaskCreator : Window
    {
        private int _projectId;
        private Task _task;
        private ProjectDetails _parentPage;

        public TaskCreator(int projectId, ProjectDetails parent)
        {
            InitializeComponent();
            _projectId = projectId;
            _parentPage = parent;
        }

        public TaskCreator(Task taskToEdit, ProjectDetails parent)
        {
            InitializeComponent();
            _task = taskToEdit;
            _parentPage = parent;
        }



        private void btnAddTask_Click(object sender, RoutedEventArgs e)
        {
            // Check if user is updating an existing task or creating a new one
            if (_task != null)
            {
                UpdateTask();
            } 
            else
            {
                CreateNewTask();
            }
        }

        private void CreateNewTask()
        {
            // Make sure necissary feilds are filled in
            bool namedEntered = String.IsNullOrEmpty(tbxTaskName.Text);

            if (namedEntered == false)
            {
                // Create task based on info provided
                Task newTask = new Task()
                {
                    Name = tbxTaskName.Text,
                    Description = tbxTaskDescription.Text,
                    DueDate = dpTaskDueDate.SelectedDate,
                    Status = 0,
                    ProjectId = _projectId,
                };

                // Add task to project
                DatabaseHandler handler = new DatabaseHandler();
                handler.CreateProjectTask(newTask);

                // Refesh datagrid on project details page
                RefreshProjectDetailsDataGrid();

                // Reset form
                ClearTaskForm();
            }
        }

        private void UpdateTask()
        {
            string name = tbxTaskName.Text;
            string description = tbxTaskDescription.Text;
            DateTime dueDate = (DateTime)dpTaskDueDate.SelectedDate;

            // Update task
            DatabaseHandler handler = new DatabaseHandler();
            handler.UpdateProjectTask(_task.Id, name, description, dueDate);

            // Refesh datagrid on project details page
            RefreshProjectDetailsDataGrid();

            // Reset form
            ClearTaskForm();
        }

        private void ClearTaskForm()
        {
            tbxTaskName.Text = "";
            tbxTaskDescription.Text = "";
            dpTaskDueDate.Text = "";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_task != null)
            {
                tbxTaskName.Text = _task.Name;
                tbxTaskDescription.Text= _task.Description;
                dpTaskDueDate.SelectedDate = _task.DueDate;
            }
        }

        private void RefreshProjectDetailsDataGrid()
        {
            _parentPage.RefreshProjectTasks();
        }
    }
}
