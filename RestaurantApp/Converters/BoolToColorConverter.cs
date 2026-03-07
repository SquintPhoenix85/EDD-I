using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace RestaurantApp.Converters
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not bool boolValue || parameter is not string colors)
                return new SolidColorBrush(Colors.Transparent);

            var colorPair = colors.Split('|');
            var colorHex = boolValue ? colorPair[0] : (colorPair.Length > 1 ? colorPair[1] : colorPair[0]);

            return new SolidColorBrush((Color)ColorConverter.ConvertFromString(colorHex));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
