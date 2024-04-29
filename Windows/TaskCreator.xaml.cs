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
    /// Interaction logic for TaskCreator.xaml
    /// </summary>
    public partial class TaskCreator : Window
    {
        private int _projectId;

        public TaskCreator(int projectId)
        {
            InitializeComponent();
            _projectId = projectId;
        }

        private void btnAddTask_Click(object sender, RoutedEventArgs e)
        {
            CreateNewTask();
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

                // Refresh dgv

                // Reset form
                ClearTaskForm();
            }
        }

        private void ClearTaskForm()
        {
            tbxTaskName.Text = "";
            tbxTaskDescription.Text = "";
            dpTaskDueDate.Text = "";
        }
    }
}
