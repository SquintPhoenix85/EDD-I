using System.Globalization;
using System.Windows.Data;

namespace RestaurantApp.Utilities
{
    public class PlatoImageConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not string nombrePlato)
                return "pack://application:,,,/Resources/comida.png";

            var nombreLower = nombrePlato.ToLower().Trim();

            return nombreLower switch
            {
                "agua" => "pack://application:,,,/Resources/agua.png",
                "capuchino" => "pack://application:,,,/Resources/capuchino.png",
                "cruasan" => "pack://application:,,,/Resources/cruasan.png",
                "espresso" or "expresso" => "pack://application:,,,/Resources/expresso.png",
                "torta de chocolate" or "tortadechocolate" or "torta chocolate" => "pack://application:,,,/Resources/tortadechocolate.png",
                _ => "pack://application:,,,/Resources/comida.png"
            };
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
