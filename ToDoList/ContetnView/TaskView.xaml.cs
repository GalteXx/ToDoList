using System.Windows.Input;
using ToDoList.Model;

namespace ToDoList.ContetnView;

public partial class TaskView : ContentView
{
    public TaskView()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty LabelProperty =
    BindableProperty.Create(nameof(Label), typeof(string), typeof(TaskView), default(string));

    public static readonly BindableProperty TaskStatusProperty =
    BindableProperty.Create(nameof(TaskStatus), typeof(Model.TaskStatus), typeof(TaskView), default(Model.TaskStatus));

    public static readonly BindableProperty RemoveCommandProperty =
    BindableProperty.Create(nameof(ICommand), typeof(ICommand), typeof(TaskView), null);

    public static readonly BindableProperty StatusUpdateCommandProperty =
    BindableProperty.Create(nameof(ICommand), typeof(ICommand), typeof(TaskView), null);

    public string Label
    {
        get => (string)GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

    public Model.TaskStatus TaskStatus
    {
        get => (Model.TaskStatus)GetValue(TaskStatusProperty);
        set => SetValue(TaskStatusProperty, value);
    }

    public ICommand RemoveCommand
    {
        get => (ICommand)GetValue(RemoveCommandProperty);
        set => SetValue(RemoveCommandProperty, value);
    }

    public ICommand StatusUpdateCommand
    {
        get => (ICommand)GetValue(StatusUpdateCommandProperty);
        set => SetValue(StatusUpdateCommandProperty, value);
    }

    private void StatusBoxChanged(object sender, CheckedChangedEventArgs e)
    {
        if (StatusUpdateCommand != null && StatusUpdateCommand.CanExecute(BindingContext as TaskModel))
            StatusUpdateCommand.Execute(BindingContext as TaskModel);
    }
}