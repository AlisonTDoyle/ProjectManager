using MaterialDesignThemes.Wpf;
using OpenTK;
using ProjectManager.Pages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

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
            SetTheme();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Main.Content = new ProjectDashboard();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Settings();
        }

        private void SetTheme()
        {
            PaletteHelper paletteHelper = new PaletteHelper();
            var theme = paletteHelper.GetTheme();

            string savedTheme = Properties.Settings.Default.theme;

            if (savedTheme == "dark")
            {
                theme.SetBaseTheme(BaseTheme.Dark);
            }
            else if (savedTheme == "light")
            {
                theme.SetBaseTheme(BaseTheme.Light);
            }
            else
            {
                theme.SetBaseTheme(BaseTheme.Inherit);
            }

            paletteHelper.SetTheme(theme);
        }

        private void btnDisplayProjectDashboard_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ProjectDashboard();
        }

        private void Main_Navigated(object sender, NavigationEventArgs e)
        {
            if (Main.Content is Page)
            {
                // If the content is any page (including ProjectDashboard),
                // set the button visibility based on whether it's ProjectDashboard or not.
                if (Main.Content is ProjectDashboard)
                {
                    btnDisplayProjectDashboard.Visibility = Visibility.Hidden;
                }
                else
                {
                    btnDisplayProjectDashboard.Visibility = Visibility.Visible;
                }
            }
            else
            {
                // If the content is not a page, hide the button.
                btnDisplayProjectDashboard.Visibility = Visibility.Hidden;
            }
        }
    }
}
