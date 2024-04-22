﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;

namespace ProjectManager.Pages
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void ChangeTheme()
        {
            PaletteHelper paletteHelper = new PaletteHelper();
            var theme = paletteHelper.GetTheme();

            var currentTheme = theme.GetBaseTheme();

            if (currentTheme == BaseTheme.Dark)
            {
                theme.SetBaseTheme(BaseTheme.Light);
            } 
            else
            {
                theme.SetBaseTheme(BaseTheme.Dark);
            }

            paletteHelper.SetTheme(theme);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChangeTheme();
        }
    }
}
