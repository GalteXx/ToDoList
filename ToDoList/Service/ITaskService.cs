using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Model;

namespace ToDoList.Service
{
    internal interface ITaskService
    {
        public Task<IEnumerable<TaskModel>> GetTasksCollectionAsync(Func<TaskModel, bool> x);
    }
}
