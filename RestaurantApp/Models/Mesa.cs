using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RestaurantApp.Models
{
    public class Mesa : IEntity, INotifyPropertyChanged
    {
        private double _x;
        private double _y;

        public int Id { get; set; }
        public int Numero { get; set; }
        public int Capacidad { get; set; }
        public EstadoMesa Estado { get; set; } = EstadoMesa.Libre;

        public double X
        {
            get => _x;
            set { _x = value; OnPropertyChanged(); }
        }

        public double Y
        {
            get => _y;
            set { _y = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public override string ToString() => $"{Id}|{Numero}|{Capacidad}|{Estado}|{X}|{Y}";

        public static Mesa FromString(string line)
        {
            var parts = line.Split('|');
            return new Mesa
            {
                Id = int.Parse(parts[0]),
                Numero = int.Parse(parts[1]),
                Capacidad = int.Parse(parts[2]),
                Estado = (EstadoMesa)System.Enum.Parse(typeof(EstadoMesa), parts[3]),
                X = double.Parse(parts[4]),
                Y = double.Parse(parts[5])
            };
        }
    }
}
