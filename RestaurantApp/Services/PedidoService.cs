using System;
using System.Collections.Generic;
using System.Linq;
using RestaurantApp.Data;
using RestaurantApp.Models;

namespace RestaurantApp.Services
{
    public class PedidoService
    {
        private readonly PedidoRepository _pedidoRepo;
        private readonly MesaRepository _mesaRepo;
        private readonly PlatoRepository _platoRepo;

        public PedidoService(PedidoRepository? pedidoRepo = null, MesaRepository? mesaRepo = null, PlatoRepository? platoRepo = null)
        {
            _pedidoRepo = pedidoRepo ?? new PedidoRepository();
            _mesaRepo = mesaRepo ?? new MesaRepository();
            _platoRepo = platoRepo ?? new PlatoRepository();
        }

        public Pedido CrearPedido(int mesaId)
        {
            var mesa = _mesaRepo.GetById(mesaId) ?? throw new Exception("Mesa no encontrada.");
            if (mesa.Estado != EstadoMesa.Libre) throw new Exception("La mesa no está libre.");
            var pedido = new Pedido { MesaId = mesaId, MesaNumero = mesa.Numero, Estado = EstadoPedido.Abierto };
            _pedidoRepo.Save(pedido);
            mesa.Estado = EstadoMesa.Ocupada;
            _mesaRepo.Save(mesa);
            return pedido;
        }

        public void AgregarDetalle(int pedidoId, int platoId, int cantidad)
        {
            if (cantidad <= 0) throw new ArgumentException("La cantidad debe ser mayor que 0.");
            var pedido = _pedidoRepo.GetById(pedidoId) ?? throw new Exception("Pedido no encontrado.");
            if (pedido.Estado != EstadoPedido.Abierto) throw new Exception("El pedido no está abierto.");
            var plato = _platoRepo.GetById(platoId) ?? throw new Exception("Plato no encontrado.");
            if (!plato.Disponible) throw new Exception("El plato no está disponible.");
            var detalle = new DetallePedido
            {
                PedidoId = pedidoId,
                PlatoId = platoId,
                NombrePlato = plato.Nombre,
                Cantidad = cantidad,
                PrecioUnitario = plato.Precio
            };
            pedido.Detalles.Add(detalle);
            _pedidoRepo.Save(pedido);
        }

        public void CerrarPedido(int pedidoId)
        {
            var pedido = _pedidoRepo.GetById(pedidoId) ?? throw new Exception("Pedido no encontrado.");
            if (pedido.Estado != EstadoPedido.Abierto) throw new Exception("El pedido no está abierto.");
            pedido.Estado = EstadoPedido.Cerrado;
            _pedidoRepo.Save(pedido);
            var mesa = _mesaRepo.GetById(pedido.MesaId);
            if (mesa != null) { mesa.Estado = EstadoMesa.Libre; _mesaRepo.Save(mesa); }
        }

        public void CancelarPedido(int pedidoId)
        {
            var pedido = _pedidoRepo.GetById(pedidoId) ?? throw new Exception("Pedido no encontrado.");
            if (pedido.Estado != EstadoPedido.Abierto) throw new Exception("Solo se pueden cancelar pedidos abiertos.");
            pedido.Estado = EstadoPedido.Cancelado;
            _pedidoRepo.Save(pedido);
            var mesa = _mesaRepo.GetById(pedido.MesaId);
            if (mesa != null) { mesa.Estado = EstadoMesa.Libre; _mesaRepo.Save(mesa); }
        }

        public List<Pedido> ObtenerTodos() => _pedidoRepo.GetAll();
    }
}
