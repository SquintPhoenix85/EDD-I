using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace RestaurantApp.Converters
{
    public class MesaStateToStyleConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 3) 
                return "MesaDisponibleStyle";

            string estado = values[0]?.ToString() ?? "Disponible";
            object selectedMesa = values[1];
            object currentMesa = values[2];

            // Si es la mesa seleccionada, aplica estilo seleccionado
            if (selectedMesa == currentMesa)
                return Application.Current.Resources["MesaSeleccionadaStyle"] as Style;

            // Si está ocupada
            if (estado.Equals("Ocupada", StringComparison.OrdinalIgnoreCase))
                return Application.Current.Resources["MesaOcupadaStyle"] as Style;

            // Por defecto disponible
            return Application.Current.Resources["MesaDisponibleStyle"] as Style;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
