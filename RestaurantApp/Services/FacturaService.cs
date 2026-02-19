using System;
using System.Collections.Generic;
using RestaurantApp.Data;
using RestaurantApp.Models;

namespace RestaurantApp.Services
{
    public class FacturaService
    {
        private readonly FacturaRepository _facturaRepo;
        private readonly PedidoRepository _pedidoRepo;

        public FacturaService(FacturaRepository? facturaRepo = null, PedidoRepository? pedidoRepo = null)
        {
            _facturaRepo = facturaRepo ?? new FacturaRepository();
            _pedidoRepo = pedidoRepo ?? new PedidoRepository();
        }

        public Factura GenerarFactura(int pedidoId, decimal propinaPorcentaje = 10)
        {
            var pedido = _pedidoRepo.GetById(pedidoId) ?? throw new Exception("Pedido no encontrado.");
            if (pedido.Estado != EstadoPedido.Cerrado) throw new Exception("El pedido debe estar cerrado para generar factura.");
            var subtotal = pedido.Total;
            var propina = subtotal * propinaPorcentaje / 100;
            var factura = new Factura
            {
                PedidoId = pedidoId,
                MesaNumero = pedido.MesaNumero,
                Subtotal = subtotal,
                Propina = propina
            };
            _facturaRepo.Save(factura);
            return factura;
        }

        public List<Factura> ObtenerTodas() => _facturaRepo.GetAll();
    }
}
