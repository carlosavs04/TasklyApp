using System;
using Microsoft.Maui.Controls;

namespace Taskly_App.Converters
{
    public class BoolToTextDecorationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // Si la tarea está completada, devolver tachado, de lo contrario, no
            return (value is bool isCompleted && isCompleted) ? TextDecorations.Strikethrough : TextDecorations.None;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
