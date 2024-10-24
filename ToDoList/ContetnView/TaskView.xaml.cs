namespace ToDoList.ContetnView;

public partial class TaskView : ContentView
{
	public TaskView()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty Label =
    BindableProperty.Create(nameof(LabelText), typeof(string), typeof(TaskView), default(string));

    public static readonly BindableProperty IsComplete =
    BindableProperty.Create(nameof(TaskStatus), typeof(bool), typeof(TaskView), default(bool));

    public string LabelText
    {
        get => (string)GetValue(Label);
        set => SetValue(Label, value);
    }

    public Model.TaskStatus TaskStatus
    {
        get => (bool)GetValue(IsComplete) ? Model.TaskStatus.Done : Model.TaskStatus.Pending;
        set => SetValue(IsComplete, value == Model.TaskStatus.Done);
    }
}