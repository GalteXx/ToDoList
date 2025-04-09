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

        //It appears I dont need it. Oh well..
        public ICommand AddTaskCommand => new Command<string>(async (taskName) => await OnAddTask(taskName));
        public ICommand SearchTaskCommand => new Command<string>(async (filter) => await OnSearchTask(filter));
        public ICommand AddTaskButtonCommand => new Command<Page>(async (page) => await OnAddNewTask(page));
        public ICommand RemoveTaskCommand => new Command<TaskModel>(async (task) => await OnRemoveTask(task));
        public ICommand UpdateTaskCommand => new Command<TaskModel>(async (task) => await OnUpdateTask(task));
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

        private async Task OnRemoveTask(TaskModel task)
        {
            await _service.RemoveTaskAsync(task);
            TasksCollection.Remove(task);
        }

        private async Task GetTasksFromService(Expression<Func<TaskModel, bool> >x)
        {
            var tasks = await _service.GetTasksCollectionAsync(x);

            //should probably supress the notifications and add all tasks in bulk.... Oh, well...
            TasksCollection.Clear();
            foreach (var taskModel in tasks)
            {
                TasksCollection.Add(taskModel);
            }
            return;
        }

        private async Task OnAddTask(string taskName)
        {
            if (string.IsNullOrWhiteSpace(taskName))
                return;
            TaskModel task = new(taskName);
            //Not the best practice. Oh well...
            TasksCollection.Add(task);
            await _service.InsertTaskAsync(task);
        }

        private async Task OnUpdateTask(TaskModel task)
        {
            await _service.UpdateTaskAsync(task);
        }

        private async Task OnAddNewTask(Page mainPage)
        {
            string taskName = await mainPage.DisplayPromptAsync("Add new Task", "", "Add", "Cancel", "Task Name");
            await OnAddTask(taskName);
        }

        private async Task OnSearchTask(string filter)
        {
            Expression<Func<TaskModel, bool>> filterExpression;
            filterExpression = string.IsNullOrEmpty(filter) ? _ => true :
                task => task.Name.Contains(filter, StringComparison.CurrentCultureIgnoreCase);
            await GetTasksFromService(filterExpression);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
