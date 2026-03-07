using System.Globalization;
using System.Windows.Data;

namespace RestaurantApp.Converters
{
    public class BoolToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not bool boolValue || parameter is not string texts)
                return "N/A";

            var textPair = texts.Split('|');
            return boolValue ? textPair[0] : (textPair.Length > 1 ? textPair[1] : textPair[0]);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
