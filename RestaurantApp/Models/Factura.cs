using System;

namespace RestaurantApp.Models
{
    public class Factura : IEntity
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public int MesaNumero { get; set; }
        public DateTime FechaHora { get; set; } = DateTime.Now;
        public decimal Subtotal { get; set; }
        public decimal Propina { get; set; }
        public decimal Total => Subtotal + Propina;

        public override string ToString() =>
            $"{Id}|{PedidoId}|{MesaNumero}|{FechaHora:yyyy-MM-ddTHH:mm:ss}|{Subtotal.ToString(System.Globalization.CultureInfo.InvariantCulture)}|{Propina.ToString(System.Globalization.CultureInfo.InvariantCulture)}";

        public static Factura FromString(string line)
        {
            var parts = line.Split('|');
            return new Factura
            {
                Id = int.Parse(parts[0]),
                PedidoId = int.Parse(parts[1]),
                MesaNumero = int.Parse(parts[2]),
                FechaHora = DateTime.Parse(parts[3]),
                Subtotal = decimal.Parse(parts[4], System.Globalization.CultureInfo.InvariantCulture),
                Propina = decimal.Parse(parts[5], System.Globalization.CultureInfo.InvariantCulture)
            };
        }
    }
}
