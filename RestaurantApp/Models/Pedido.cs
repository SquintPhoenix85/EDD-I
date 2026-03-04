using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantApp.Models
{
    public class Pedido : IEntity
    {
        public int Id { get; set; }
        public int MesaId { get; set; }
        public int MesaNumero { get; set; }
        public DateTime FechaHora { get; set; } = DateTime.Now;
        public EstadoPedido Estado { get; set; } = EstadoPedido.Abierto;
        public List<DetallePedido> Detalles { get; set; } = new();
        public decimal Total => Detalles.Sum(d => d.Subtotal);

        public override string ToString() =>
            $"{Id}|{MesaId}|{MesaNumero}|{FechaHora:yyyy-MM-ddTHH:mm:ss}|{Estado}";

        public static Pedido FromString(string line)
        {
            var parts = line.Split('|');
            return new Pedido
            {
                Id = int.Parse(parts[0]),
                MesaId = int.Parse(parts[1]),
                MesaNumero = int.Parse(parts[2]),
                FechaHora = DateTime.Parse(parts[3]),
                Estado = (EstadoPedido)System.Enum.Parse(typeof(EstadoPedido), parts[4])
            };
        }
    }
}
