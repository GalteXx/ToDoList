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

    public static readonly BindableProperty IsCompleteProperty =
    BindableProperty.Create(nameof(IsComplete), typeof(bool), typeof(TaskView), default(bool));

    public static readonly BindableProperty RemoveCommandProperty =
    BindableProperty.Create(nameof(ICommand), typeof(ICommand), typeof(TaskView), null);

    public string Label
    {
        get => (string)GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

    public Model.TaskStatus IsComplete
    {
        get => (bool)GetValue(IsCompleteProperty) ? Model.TaskStatus.Done : Model.TaskStatus.Pending;
        set => SetValue(IsCompleteProperty, value == Model.TaskStatus.Done);
    }

    public ICommand RemoveCommand
    {
        get => (ICommand)GetValue(RemoveCommandProperty);
        set => SetValue(RemoveCommandProperty, value);
    }

}