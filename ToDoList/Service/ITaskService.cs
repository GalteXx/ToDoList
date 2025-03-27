using System.Linq.Expressions;
using ToDoList.Model;

namespace ToDoList.Service
{
    public interface ITaskService
    {
        public Task Initialization { get; }

        public Task<IEnumerable<TaskModel>> GetTasksCollectionAsync(Expression<Func<TaskModel, bool>> x);

        public Task InsertTaskAsync(TaskModel task);
    }
}
