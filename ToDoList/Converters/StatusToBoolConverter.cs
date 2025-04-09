using System.Globalization;

namespace ToDoList.Converters
{
    internal class StatusToBoolConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value != null && (Model.TaskStatus)value == Model.TaskStatus.Done;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null || !(bool)value)
                return Model.TaskStatus.Pending;

            return Model.TaskStatus.Done;
        }
    }
}
