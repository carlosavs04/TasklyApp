using System;
using System.Globalization;

namespace Taskly_App.Converters
{
    public class TruncateTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string text)
            {
                int maxLength = 25;
                return text.Length > maxLength ? text.Substring(0, maxLength) + "..." : text;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
