using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListModel.DataLayer;

namespace ToDoListModel.Models
{
    /// <summary>
    /// Class representing a todo task
    /// </summary>
    public class ToDoTask
    {

        #region Properties

        /// <summary>
        /// Id of the task
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Description of the task
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Responsible person for the task. Empty if no person assigned
        /// </summary>
        public string? AssignedName { get; set; }
        /// <summary>
        /// Timestamp of task creation
        /// </summary>
        public DateTime DateTimeCreated { get; set; }
        /// <summary>
        /// Timestamp of task finished. Empty is task still open
        /// </summary>
        public DateTime? DateTimeFinished { get; set; }

        #endregion

        #region behaviour

        /// <summary>
        /// Constructor of the task
        /// </summary>        
        /// <param name="description">Description of the task</param>
        public ToDoTask(string description)
        {
            Description = description;
            AssignedName = string.Empty;
            DateTimeCreated = DateTime.Now;
            DateTimeFinished = null;
        }

        /// <summary>
        /// Assign a person to the task
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool AssignPerson(string name)
        {
            if (DateTimeFinished == null)
            {
                AssignedName = name;
                this.Update();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Finish the task, aka set an end datetime
        /// </summary>
        public void FinishTask()
        {
            DateTimeFinished = DateTime.Now;
            this.Update();
        }

        #endregion

        #region Data access

        /// <summary>
        /// Persist this task in datalayer
        /// </summary>
        public void Create()
        {
            // controles vinden hier plaats
            if (Id != 0)
            {
                throw new Exception("Kan geen taak aanmaken als de taak al een id heeft (taak bestaat al?)");
            }

            IDataAccessLayer dal = DALSingleton.Instance; 
            var result = dal.CreateToDoTask(this);
            this.Id = result.Id;
        }

        /// <summary>
        /// Read a specific task from the datalayer
        /// </summary>
        /// <param name="id">The id of the specific task</param>
        /// <returns>The specific task</returns>
        public static ToDoTask? Read(int id)
        {
            IDataAccessLayer dal = DALSingleton.Instance;
            return dal.ReadToDoTask(id);
        }

        /// <summary>
        /// Read all tasks from the datalayer
        /// </summary>
        /// <returns>List with all tasks</returns>
        public static List<ToDoTask> ReadAll()
        {
            IDataAccessLayer dal = DALSingleton.Instance;
            return dal.ReadToDoTasks();
        }

        /// <summary>
        /// Persist the changes on the task in the datalayer (update)
        /// This method is private so it can only be called from this class by another method. You force the user to use the code you wrote to update a task!
        /// </summary>
        /// <returns>The updated task</returns>
        private ToDoTask Update()
        {
            // controles vinden hier plaats
            if (Id == 0)
            {
                throw new Exception("Kan geen taak updaten zonder id. Wellicht moet de taak eerst aangemaakt worden?");
            }

            IDataAccessLayer dal = DALSingleton.Instance;
            return dal.UpdateToDoTask(this);
        }

        /// <summary>
        /// Delete this task from the datalayer
        /// </summary>
        public void Delete()
        {
            // controles vinden hier plaats, bv voorwaarden of ik mag deleten
            if (this.DateTimeFinished == null)
            {
                throw new Exception("Alleen afgeronde taken mogen verwijderd worden!");
            }

            IDataAccessLayer dal = DALSingleton.Instance;
            dal.DeleteToDoTask(this);
        }

        #endregion
    }
}
