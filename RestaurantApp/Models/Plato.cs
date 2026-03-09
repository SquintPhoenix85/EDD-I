using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RestaurantApp.Models
{
    public class Plato : IEntity, INotifyPropertyChanged
    {
        private int _id;
        private string _nombre = string.Empty;
        private decimal _precio;
        private bool _disponible = true;

        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string Nombre
        {
            get => _nombre;
            set => SetProperty(ref _nombre, value);
        }

        public decimal Precio
        {
            get => _precio;
            set => SetProperty(ref _precio, value);
        }

        public bool Disponible
        {
            get => _disponible;
            set => SetProperty(ref _disponible, value);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void SetProperty<T>(ref T backingField, T value, [CallerMemberName] string propertyName = "")
        {
            if (!EqualityComparer<T>.Default.Equals(backingField, value))
            {
                backingField = value;
                OnPropertyChanged(propertyName);
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString() => $"{Id}|{Nombre}|{Precio.ToString(System.Globalization.CultureInfo.InvariantCulture)}|{Disponible}";

        public static Plato FromString(string line)
        {
            var parts = line.Split('|');
            return new Plato
            {
                Id = int.Parse(parts[0]),
                Nombre = parts[1],
                Precio = decimal.Parse(parts[2], System.Globalization.CultureInfo.InvariantCulture),
                Disponible = bool.Parse(parts[3])
            };
        }
    }
}

