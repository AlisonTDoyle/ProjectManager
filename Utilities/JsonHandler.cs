using Newtonsoft.Json;
using ProjectManager.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MaterialDesignThemes.Wpf.Theme.ToolBar;

namespace ProjectManager.Utilities
{
    internal class JsonHandler
    {
        private string _subjectsJsonPath = "./../../Resources/Data/subjects.json";

        public List<Subject> ReadInSubjects()
        {
            List<Subject> subjects;

            // Read in subjects from file
            using (StreamReader reader = new StreamReader(_subjectsJsonPath))
            {
                string fileContents = reader.ReadToEnd();
                subjects = JsonConvert.DeserializeObject<List<Subject>>(fileContents);
            }

            return subjects;
        }

        public void WriteToSubjectsFile(List<Subject> subjects)
        {
            using (StreamWriter writer = new StreamWriter(_subjectsJsonPath))
            {
                string newFileContents = JsonConvert.SerializeObject(subjects);
                writer.WriteLineAsync(newFileContents);
            }
        }
    }
}
