using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using RestaurantApp.Models;
using RestaurantApp.Services;

namespace RestaurantApp.ViewModels
{
    public class PedidosViewModel : BaseViewModel
    {
        private readonly PedidoService _pedidoService;
        private readonly PlatoService _platoService;
        private readonly MesaService _mesaService;
        private readonly FacturaService _facturaService;

        private Pedido? _selectedPedido;
        private Mesa? _selectedMesa;
        private Plato? _selectedPlato;
        private string _cantidad = "1";
        private string _mensaje = string.Empty;
        private string _propina = "10";

        public ObservableCollection<Pedido> Pedidos { get; } = new();
        public ObservableCollection<Mesa> Mesas { get; } = new();
        public ObservableCollection<Plato> Platos { get; } = new();
        public ObservableCollection<DetallePedido> Detalles { get; } = new();

        public Pedido? SelectedPedido
        {
            get => _selectedPedido;
            set { SetProperty(ref _selectedPedido, value); ActualizarDetalles(); }
        }
        public Mesa? SelectedMesa { get => _selectedMesa; set => SetProperty(ref _selectedMesa, value); }
        public Plato? SelectedPlato { get => _selectedPlato; set => SetProperty(ref _selectedPlato, value); }
        public string Cantidad { get => _cantidad; set => SetProperty(ref _cantidad, value); }
        public string Mensaje { get => _mensaje; set => SetProperty(ref _mensaje, value); }
        public string Propina { get => _propina; set => SetProperty(ref _propina, value); }

        public ICommand CrearPedidoCommand { get; }
        public ICommand AgregarDetalleCommand { get; }
        public ICommand CerrarPedidoCommand { get; }
        public ICommand CancelarPedidoCommand { get; }
        public ICommand GenerarFacturaCommand { get; }

        public PedidosViewModel(PedidoService? pedidoService = null, PlatoService? platoService = null,
            MesaService? mesaService = null, FacturaService? facturaService = null)
        {
            _pedidoService = pedidoService ?? new PedidoService();
            _platoService = platoService ?? new PlatoService();
            _mesaService = mesaService ?? new MesaService();
            _facturaService = facturaService ?? new FacturaService();
            CrearPedidoCommand = new RelayCommand(CrearPedido);
            AgregarDetalleCommand = new RelayCommand(AgregarDetalle);
            CerrarPedidoCommand = new RelayCommand(CerrarPedido);
            CancelarPedidoCommand = new RelayCommand(CancelarPedido);
            GenerarFacturaCommand = new RelayCommand(GenerarFactura);
            Cargar();
        }

        private void Cargar()
        {
            Pedidos.Clear();
            foreach (var p in _pedidoService.ObtenerTodos()) Pedidos.Add(p);
            Mesas.Clear();
            foreach (var m in _mesaService.ObtenerTodas()) Mesas.Add(m);
            Platos.Clear();
            foreach (var p in _platoService.ObtenerTodos()) Platos.Add(p);
        }

        public void RefreshData() => Cargar();

        private void ActualizarDetalles()
        {
            Detalles.Clear();
            if (SelectedPedido != null)
                foreach (var d in SelectedPedido.Detalles) Detalles.Add(d);
        }

        private void CrearPedido(object? _)
        {
            if (SelectedMesa == null) { Mensaje = "Seleccione una mesa."; return; }
            try
            {
                var pedido = _pedidoService.CrearPedido(SelectedMesa.Id);
                Mensaje = $"Pedido #{pedido.Id} creado para Mesa {pedido.MesaNumero}.";
                Cargar();
            }
            catch (Exception ex) { Mensaje = $"Error: {ex.Message}"; }
        }

        private void AgregarDetalle(object? _)
        {
            if (SelectedPedido == null) { Mensaje = "Seleccione un pedido."; return; }
            if (SelectedPlato == null) { Mensaje = "Seleccione un plato."; return; }
            if (!int.TryParse(Cantidad, out var cant) || cant <= 0) { Mensaje = "Cantidad inválida."; return; }
            try
            {
                var pedidoId = SelectedPedido.Id;
                _pedidoService.AgregarDetalle(pedidoId, SelectedPlato.Id, cant);
                Mensaje = "Detalle agregado.";
                Cargar();
                SelectedPedido = Pedidos.FirstOrDefault(p => p.Id == pedidoId);
            }
            catch (Exception ex) { Mensaje = $"Error: {ex.Message}"; }
        }

        private void CerrarPedido(object? _)
        {
            if (SelectedPedido == null) { Mensaje = "Seleccione un pedido."; return; }
            try
            {
                _pedidoService.CerrarPedido(SelectedPedido.Id);
                Mensaje = "Pedido cerrado.";
                Cargar();
            }
            catch (Exception ex) { Mensaje = $"Error: {ex.Message}"; }
        }

        private void CancelarPedido(object? _)
        {
            if (SelectedPedido == null) { Mensaje = "Seleccione un pedido."; return; }
            try
            {
                _pedidoService.CancelarPedido(SelectedPedido.Id);
                Mensaje = "Pedido cancelado.";
                Cargar();
            }
            catch (Exception ex) { Mensaje = $"Error: {ex.Message}"; }
        }

        private void GenerarFactura(object? _)
        {
            if (SelectedPedido == null) { Mensaje = "Seleccione un pedido."; return; }
            if (!decimal.TryParse(Propina, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out var propina))
            { Mensaje = "Propina inválida."; return; }
            try
            {
                var factura = _facturaService.GenerarFactura(SelectedPedido.Id, propina);
                Mensaje = $"Factura #{factura.Id} generada. Total: {factura.Total:C}";
                Cargar();
            }
            catch (Exception ex) { Mensaje = $"Error: {ex.Message}"; }
        }
    }
}
