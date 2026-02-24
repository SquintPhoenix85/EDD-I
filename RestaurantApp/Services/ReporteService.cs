using System;
using System.Collections.Generic;
using System.Linq;
using RestaurantApp.Data;
using RestaurantApp.Models;

namespace RestaurantApp.Services
{
    public class ReporteVentas
    {
        public DateTime Fecha { get; set; }
        public int NumeroFacturas { get; set; }
        public decimal TotalVentas { get; set; }
    }

    public class TopPlato
    {
        public string NombrePlato { get; set; } = string.Empty;
        public int CantidadVendida { get; set; }
        public decimal TotalGenerado { get; set; }
    }

    public class ReporteService
    {
        private readonly FacturaRepository _facturaRepo;
        private readonly PedidoRepository _pedidoRepo;

        public ReporteService(FacturaRepository? facturaRepo = null, PedidoRepository? pedidoRepo = null)
        {
            _facturaRepo = facturaRepo ?? new FacturaRepository();
            _pedidoRepo = pedidoRepo ?? new PedidoRepository();
        }

        public List<ReporteVentas> VentasPorRangoDeFechas(DateTime inicio, DateTime fin)
        {
            var facturas = _facturaRepo.GetAll()
                .Where(f => f.FechaHora.Date >= inicio.Date && f.FechaHora.Date <= fin.Date)
                .ToList();

            return facturas
                .GroupBy(f => f.FechaHora.Date)
                .Select(g => new ReporteVentas
                {
                    Fecha = g.Key,
                    NumeroFacturas = g.Count(),
                    TotalVentas = g.Sum(f => f.Total)
                })
                .OrderBy(r => r.Fecha)
                .ToList();
        }

        public List<TopPlato> Top5PlatosMasVendidos()
        {
            var pedidos = _pedidoRepo.GetAll()
                .Where(p => p.Estado == EstadoPedido.Cerrado)
                .ToList();

            return pedidos
                .SelectMany(p => p.Detalles)
                .GroupBy(d => d.NombrePlato)
                .Select(g => new TopPlato
                {
                    NombrePlato = g.Key,
                    CantidadVendida = g.Sum(d => d.Cantidad),
                    TotalGenerado = g.Sum(d => d.Subtotal)
                })
                .OrderByDescending(t => t.CantidadVendida)
                .Take(5)
                .ToList();
        }
    }
}
