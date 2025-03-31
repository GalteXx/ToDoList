using ToDoList.Service;
using ToDoList.ViewModel;

namespace ToDoList
{
    public partial class MainPage : ContentPage
    {
        public MainPage(ServiceFactory serviceFactory)
        {
            InitializeComponent();
            InitalizeViewModelAsync(serviceFactory);
        }

        private async void InitalizeViewModelAsync(ServiceFactory serviceFactory)
        {
            BindingContext = await TaskPageViewModel.CreateAsync(serviceFactory);
        }

        private async void AddTaskButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}
