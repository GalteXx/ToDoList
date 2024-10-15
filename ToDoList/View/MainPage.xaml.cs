using ToDoList.ViewModel;

namespace ToDoList
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            BindingContext = new TaskPageViewModel();
            InitializeComponent();

        }

    }

}
