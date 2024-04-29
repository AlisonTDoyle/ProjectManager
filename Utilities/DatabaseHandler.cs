﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectManager.Utilities
{
    internal class DatabaseHandler
    {
        ProjectDBEntities db = new ProjectDBEntities();

        #region Project table methods
        public List<Project> FetchProjects()
        {
            var query = from project in db.Projects
                        select project;

            return query.ToList();
        }

        public Project FetchProjectById(int id)
        {
            var query = from project in db.Projects
                        where project.Id == id
                        select project;

            return query.FirstOrDefault();
        }

        public void RemoveProject(int projectId)
        {
            // Find project to remove
            var projectToDelete = (from project in db.Projects
                                   where project.Id == projectId
                                   select project).FirstOrDefault();


            // Remove associated tasks
            var query = from task in db.Tasks
                        where task.ProjectId == projectToDelete.Id
                        select task;

            List<Task> projectTasks = query.ToList();

            for (int i = 0; i < projectTasks.Count; i++)
            {
                RemoveTask(projectTasks[i].Id);
            }

            // Remove selected project
            db.Projects.Remove(projectToDelete);

            db.SaveChanges();
        }

        public void CreateProject(Project project, List<Task> projectTasks)
        {
            // Create project
            db.Projects.Add(project);

            db.SaveChanges();

            for (int i = 0; i < projectTasks.Count; i++)
            {
                projectTasks[i].ProjectId = project.Id;
                CreateProjectTask(projectTasks[i]);
            }
        }

        public List<Project> FetchProjectsByName(string name)
        {
            List<Project> projectsReturned = new List<Project>();

            if (!string.IsNullOrEmpty(name))
            {
                var query = from project in db.Projects
                            where project.Name.Contains(name)
                            select project;

                projectsReturned = query.ToList();
            }
            else
            {
                projectsReturned = FetchProjects();
            }

            return projectsReturned;
        }

        public List<Project> FetchProjectsBySubject(string subject)
        {
            var query = from project in db.Projects
                        where project.Subject == subject
                        select project;

            return query.ToList();
        }

        public List<string> FetchSubjects()
        {
            List<string> subjects = new List<string>();

            // Get all projects
            var projects = FetchProjects();

            // Go through each project and add it's subject if it isnt already in the list
            foreach (var project in projects)
            {
                if (!subjects.Contains(project.Subject))
                {
                    subjects.Add(project.Subject);
                }
            }

            return subjects;
        }
        #endregion

        #region Task table methods
        public List<Task> FetchTasks(int projectId)
        {
            var query = from task in db.Tasks
                        where task.ProjectId == projectId
                        select task;

            return query.ToList();
        }

        public void RemoveTask(int taskId)
        {
            // Find task to remove
            var taskToDelete = (from task in db.Tasks
                                where task.Id == taskId
                                select task).FirstOrDefault();

            // Remove selected task
            db.Tasks.Remove(taskToDelete);

            db.SaveChanges();
        }

        public void CreateProjectTask(Task newTask)
        {
            db.Tasks.Add(newTask);
            db.SaveChanges();
        }
        #endregion
    }
}
