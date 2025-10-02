using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ToDoListModel.Models;

namespace ToDoListModel.DataLayer
{
    /// <summary>
    /// Data access layer storing data in a json file
    /// </summary>
    public class JsonDAL : IDataAccessLayer
    {
        /// <summary>
        /// In memory list for the tasks
        /// </summary>
        List<ToDoTask> tasks = new();
        /// <summary>
        /// Name of the json file
        /// </summary>
        readonly string tasksFileName = "todotasks.json";

        /// <summary>
        /// Constructor
        /// </summary>
        public JsonDAL()
        {
            this.GetFromFile();
            if (tasks.Count == 0)
            {
                // create dummydata
                this.CreateDummyData();
            }
        }

        /// <summary>
        /// Get the data from file
        /// </summary>
        private void GetFromFile()
        {
            try
            {
                string file = File.ReadAllText(tasksFileName);
                if (!string.IsNullOrEmpty(file))
                {
                    tasks = JsonSerializer.Deserialize<List<ToDoTask>>(file);
                }
                else
                { 
                    tasks = new();
                }
            }
            catch (Exception)
            {
                tasks?.Clear();
            }
        }

        /// <summary>
        /// Save the data to file
        /// </summary>
        private void SaveToFile()
        {
            File.WriteAllText(tasksFileName, JsonSerializer.Serialize(tasks));

        }

        /// <summary>
        /// Create dummy data
        /// </summary>
        private void CreateDummyData()
        {
            CreateToDoTask(new ToDoTask("Boodschappen doen"));
            CreateToDoTask(new ToDoTask("Terras vegen"));
            CreateToDoTask(new ToDoTask("Vaatwasser uitruimen"));
            CreateToDoTask(new ToDoTask("Hond uitlaten"));
            CreateToDoTask(new ToDoTask("Bier drinken"));            
        }

        #region Implemented interface methods (for comments see interface)

        public ToDoTask CreateToDoTask(ToDoTask toDoTask)
        {
            // create ids
            int maxId = tasks.Count == 0 ? 1 : tasks.Max(l => l.Id) + 1;
            toDoTask.Id = maxId++;

            tasks.Add(toDoTask);
            this.SaveToFile();
            return toDoTask;
        }

        public bool DeleteToDoTask(ToDoTask ToDoTask)
        {
            try
            {
                tasks.Remove(ToDoTask);
                this.SaveToFile();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ToDoTask? ReadToDoTask(int id)
        {
            return tasks.Find(x => x.Id == id);
        }

        public List<ToDoTask> ReadToDoTasks()
        {
            return tasks;
        }

        public ToDoTask UpdateToDoTask(ToDoTask toDoTask)
        {
            // easiest is remove and add again
            var toDelete = tasks.Find(x => x.Id == toDoTask.Id);
            if (toDelete != null)
            {
                tasks.Remove(toDelete);
                tasks.Add(toDoTask);
            }
            this.SaveToFile();
            return toDoTask;            
        }

        #endregion
    }
}
