using MaterialDesignThemes.Wpf;
using ProjectManager.Classes;
using ProjectManager.Utilities;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ProjectManager.Pages
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        private List<Subject> _subjects = new List<Subject>();

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
                UpdateSelectedThemeInSettings("light");
            } 
            else
            {
                theme.SetBaseTheme(BaseTheme.Dark);
                UpdateSelectedThemeInSettings("dark");
            }

            paletteHelper.SetTheme(theme);
        }

        private void UpdateSelectedThemeInSettings(string theme)
        {
            Properties.Settings.Default.theme = theme;
            Properties.Settings.Default.Save();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChangeTheme();
        }

        private void PopulateSubjectsList()
        {
            JsonHandler jsonHandler = new JsonHandler();
            _subjects = jsonHandler.ReadInSubjects();

            lbxSubjects.ItemsSource = null;
            lbxSubjects.ItemsSource = _subjects; 
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateSubjectsList();
        }

        private void UpdateWithNewSubjects()
        {
            if (!String.IsNullOrEmpty(tbxSubjectName.Text))
            {
                // Create subject object
                Subject newSubject = new Subject()
                {
                    Name = tbxSubjectName.Text,
                    Color = "#77777"
                };

                // Append to list
                _subjects.Add(newSubject);

                // Update file
                JsonHandler jsonHandler = new JsonHandler();
                jsonHandler.WriteToSubjectsFile(_subjects);

                // Refresh listbox
                PopulateSubjectsList();
            }
            else
            {
                MessageBox.Show("Please enter a name for the subject to add it to the list", "Error");
            }
        }

        private void btnAddSubject_Click(object sender, RoutedEventArgs e)
        {
            UpdateWithNewSubjects();
        }

        private void RemoveSubject()
        {
            int? subjectIndex = lbxSubjects.SelectedIndex;

            if ((subjectIndex != null) && (subjectIndex >= 0))
            {
                // Remove subject
                _subjects.RemoveAt((int)subjectIndex);

                // Update file
                JsonHandler jsonHandler = new JsonHandler();
                jsonHandler.WriteToSubjectsFile(_subjects);

                // Refresh listbox
                PopulateSubjectsList();
            } 
            else
            {
                MessageBox.Show("Select a subject to delete", "Error");
            }
        }

        private void btnDeleteSubject_Click(object sender, RoutedEventArgs e)
        {
            RemoveSubject();
        }
    }
}
