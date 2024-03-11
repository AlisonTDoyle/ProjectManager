using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectManager.Utilities
{
    internal class DatabaseHandler
    {
        ProjectDBEntities1 db = new ProjectDBEntities1();

        //Projects Table
        public List<Project> FetchProjects()
        {
            var query = from project in db.Projects
                        select project;

            return query.ToList();
        }

        public void CreateProject(Project newProject)
        {
            try
            {
                db.Projects.Add(newProject);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Tasks Table
    }
}
