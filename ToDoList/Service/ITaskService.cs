using System.Linq.Expressions;
using ToDoList.Model;

namespace ToDoList.Service
{
    internal interface ITaskService
    {
        public Task<IEnumerable<TaskModel>> GetTasksCollectionAsync(Expression<Func<TaskModel, bool>> x);

        public Task InsertTaskAsync(TaskModel task);
    }
}
