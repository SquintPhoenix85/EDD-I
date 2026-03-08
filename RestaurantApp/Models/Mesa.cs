namespace RestaurantApp.Models
{
    public class Mesa : IEntity
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public int Capacidad { get; set; }
        public EstadoMesa Estado { get; set; } = EstadoMesa.Libre;

        // Canvas data, depronto deberia estar en otro archivo, pero por ahora lo dejo aqui
        public int X { get; set; }
        public int Y { get; set; }

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
                X = int.Parse(parts[4]),
                Y = int.Parse(parts[5])
            };
        }
    }
}
