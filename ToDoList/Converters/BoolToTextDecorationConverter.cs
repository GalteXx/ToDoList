using System.Globalization;

namespace ToDoList.Converters
{
    internal class BoolToTextDecorationConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value != null && (bool)value ? TextDecorations.Strikethrough : TextDecorations.None;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
