namespace RestaurantApp.Models
{
    public class DetallePedido
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public int PlatoId { get; set; }
        public string NombrePlato { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal => Cantidad * PrecioUnitario;

        public override string ToString() =>
            $"{Id}|{PedidoId}|{PlatoId}|{NombrePlato}|{Cantidad}|{PrecioUnitario.ToString(System.Globalization.CultureInfo.InvariantCulture)}";

        public static DetallePedido FromString(string line)
        {
            var parts = line.Split('|');
            return new DetallePedido
            {
                Id = int.Parse(parts[0]),
                PedidoId = int.Parse(parts[1]),
                PlatoId = int.Parse(parts[2]),
                NombrePlato = parts[3],
                Cantidad = int.Parse(parts[4]),
                PrecioUnitario = decimal.Parse(parts[5], System.Globalization.CultureInfo.InvariantCulture)
            };
        }
    }
}
