namespace RestaurantApp.Models
{
    public class Mesa
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public int Capacidad { get; set; }
        public EstadoMesa Estado { get; set; } = EstadoMesa.Libre;

        public override string ToString() => $"{Id}|{Numero}|{Capacidad}|{Estado}";

        public static Mesa FromString(string line)
        {
            var parts = line.Split('|');
            return new Mesa
            {
                Id = int.Parse(parts[0]),
                Numero = int.Parse(parts[1]),
                Capacidad = int.Parse(parts[2]),
                Estado = (EstadoMesa)System.Enum.Parse(typeof(EstadoMesa), parts[3])
            };
        }
    }
}
