using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ToDoList.Model;
using ToDoList.Service;

namespace ToDoList.ViewModel
{
    internal class TaskPageViewModel
    {
        private ITaskService _service;
        public ObservableCollection<TaskModel> TasksCollection { get; private set; }
        public TaskPageViewModel()
        {
            _service = new TaskMongoDBService();
            TasksCollection = [];
            Task.Run(() => GetTasksFromService(_ => true));
        }

        private async Task GetTasksFromService(Func<TaskModel, bool> x)
        {
            var tasks = await _service.GetTasksCollectionAsync(x);

            foreach (var taskModel in tasks)
            {
                //should probably supress the notifications and add all tasks in bulk.... meh, whatever
                TasksCollection.Add(taskModel);
            }
            
            return;
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
