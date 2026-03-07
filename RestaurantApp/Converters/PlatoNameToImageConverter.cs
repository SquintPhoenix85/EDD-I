using System.Globalization;
using System.Windows.Data;
using RestaurantApp.Utilities;

namespace RestaurantApp.Converters
{
    public class PlatoNameToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string platoNombre)
            {
                return PlatoImageHelper.GetImagePathByName(platoNombre);
            }

            return PlatoImageHelper.GetDefaultImage();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
