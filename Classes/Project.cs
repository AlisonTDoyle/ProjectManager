using ProjectManager.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager
{
    public partial class Project
    {
        private double _progress;

        public double Progress
        {
            get
            {
                return CalculateProgress();
            }
            set
            {
                // Need public setter or else an error is thrown when a user clicks on progress cell in project dashboard
                _progress = value;
            }
        }

        private double CalculateProgress()
        {
            // Fetch tasks
            DatabaseHandler handler = new DatabaseHandler();
            List<Task> tasks = handler.FetchTasks(Id);

            // Count number completed
            double tasksCompleted = 0;
            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].Status == 2)
                {
                    tasksCompleted++;
                }
                else if (tasks[i].Status == 1)
                {
                    tasksCompleted += 0.5;
                }
            }

            // Calculate and return % complete
            double completePercentage = (tasksCompleted / tasks.Count) * 100;

            return completePercentage;
        }
    }
}
