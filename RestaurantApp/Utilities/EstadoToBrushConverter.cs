using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using RestaurantApp.Models;

namespace RestaurantApp.Converters
{
    public class EstadoToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is EstadoMesa estado)
            {
                return estado == EstadoMesa.Ocupada ? Brushes.Red : Brushes.LightGreen;
            }
            return Brushes.LightGray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotSupportedException();
    }
}