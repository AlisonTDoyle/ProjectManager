using ProjectManager.Utilities;
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
            var projects = handler.FetchProjects();

            dgProjects.ItemsSource = projects;
        }

        private void dgProjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainWindow main = Application.Current.MainWindow as MainWindow;
            main.Main.Navigate(new ProjectDetails());
        }
    }
}
