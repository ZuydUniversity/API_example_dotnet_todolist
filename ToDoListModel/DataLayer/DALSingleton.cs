using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListModel.DataLayer
{
    /// <summary>
    /// Singleton class for the DAL
    /// More info: https://en.wikipedia.org/wiki/Singleton_pattern
    /// </summary>
    public sealed class DALSingleton
    {
        private static IDataAccessLayer instance = null!;
        private static readonly object padlock = new();

        public DALSingleton()
        {
        }

        public static IDataAccessLayer Instance
        {
            get
            {
                lock(padlock)
                {
                    // maak hier eventueel een andere dal aan
                    instance ??= new JsonDAL();
                    return instance;
                }
            }
        }

    }
}
