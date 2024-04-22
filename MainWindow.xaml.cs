using ProjectManager.Pages;
using System.Windows;

namespace ProjectManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Main.Content = new ProjectDashboard();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Settings();
        }
    }
}
