using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Utilities
{
    internal class LiveChartsHandler
    {
        // Constructers
        public LiveChartsHandler() { }

        // Methods
        public SeriesCollection createTaskChartData(List<Task> projectTasks)
        {
            SeriesCollection series;

            int tasksNotStarted = 0;
            int tasksInProgress = 0;
            int tasksCompleted = 0;

            // Count no of tasks in each category
            for (int i = 0; i < projectTasks.Count; i++)
            {
                if (projectTasks[i].Status == 0)
                {
                    tasksNotStarted++;
                } 
                else if (projectTasks[i].Status == 1) 
                {
                    tasksInProgress++;
                } 
                else
                {
                    tasksCompleted++;
                }
            }

            series = new SeriesCollection()
            {
                 new PieSeries
                {
                    Title = "Not Started",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(tasksNotStarted) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "In Progress",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(tasksInProgress) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Completed",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(tasksCompleted) },
                    DataLabels = true
                }
            };

            return series;
        }
    }
}
