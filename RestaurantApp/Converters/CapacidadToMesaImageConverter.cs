using System.Globalization;
using System.Windows.Data;
using RestaurantApp.Utilities;

namespace RestaurantApp.Converters
{
    public class CapacidadToMesaImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int capacidad)
            {
                return MesaImageHelper.GetImagePathByCapacidad(capacidad);
            }
            return MesaImageHelper.GetDefaultImage();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
