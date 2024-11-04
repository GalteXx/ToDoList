using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ToDoList.Model;
using ToDoList.Service;

namespace ToDoList.ViewModel
{
    internal class TaskPageViewModel : INotifyPropertyChanged
    {
        private ITaskService _service;
        public ObservableCollection<TaskModel> TasksCollection { get; private set; }

        public ICommand AddTaskCommand => new Command<string>(async (taskName) => await OnAddTask(taskName));
        
        public TaskPageViewModel()
        {
            _service = new TaskMongoDBService();
            TasksCollection = [];
            Task.Run(() => GetTasksFromService(_ => true));
        }

        private async Task GetTasksFromService(Expression<Func<TaskModel, bool> >x)
        {
            var tasks = await _service.GetTasksCollectionAsync(x);

            foreach (var taskModel in tasks)
            {
                //should probably supress the notifications and add all tasks in bulk.... meh, whatever
                TasksCollection.Add(taskModel);
            }
            
            return;
        }

        private async Task OnAddTask(string task)
        {
            await _service.InsertTaskAsync(new TaskModel(task));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
