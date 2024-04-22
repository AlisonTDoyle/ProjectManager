using MaterialDesignThemes.Wpf;
using OpenTK;
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
    }
}
