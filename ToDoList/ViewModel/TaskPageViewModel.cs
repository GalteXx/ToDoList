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
        public ICommand SearchTaskCommand => new Command<string>(async (filter) => await OnSearchTask(filter));       
        public TaskPageViewModel() { }

        public static async Task<TaskPageViewModel> CreateAsync(ServiceFactory serviceFactory)
        {
            var vm = new TaskPageViewModel();
            await vm.InitalizeAsync(serviceFactory);
            return vm;
        }

        private async Task InitalizeAsync(ServiceFactory serviceFactory)
        {
            _service = await serviceFactory.GetOrCreateService();
            TasksCollection = [];
            await _service.Initialization;
            await GetTasksFromService(_ => true);
        }

        private async Task GetTasksFromService(Expression<Func<TaskModel, bool> >x)
        {
            var tasks = await _service.GetTasksCollectionAsync(x);

            //should probably supress the notifications and add all tasks in bulk.... meh, whatever
            TasksCollection.Clear();
            foreach (var taskModel in tasks)
            {
                
                TasksCollection.Add(taskModel);
            }
            
            return;
        }

        private async Task OnAddTask(string task)
        {
            await _service.InsertTaskAsync(new TaskModel(task));
        }

        private async Task OnSearchTask(string filter)
        {
            await GetTasksFromService(task => task.Name.Contains(filter, StringComparison.CurrentCultureIgnoreCase));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
