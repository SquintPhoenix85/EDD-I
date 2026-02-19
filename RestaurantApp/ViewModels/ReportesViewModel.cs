using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using RestaurantApp.Services;

namespace RestaurantApp.ViewModels
{
    public class ReportesViewModel : BaseViewModel
    {
        private readonly ReporteService _service;
        private DateTime _fechaInicio = DateTime.Today.AddDays(-30);
        private DateTime _fechaFin = DateTime.Today;
        private string _mensaje = string.Empty;

        public ObservableCollection<ReporteVentas> Ventas { get; } = new();
        public ObservableCollection<TopPlato> TopPlatos { get; } = new();

        public DateTime FechaInicio { get => _fechaInicio; set => SetProperty(ref _fechaInicio, value); }
        public DateTime FechaFin { get => _fechaFin; set => SetProperty(ref _fechaFin, value); }
        public string Mensaje { get => _mensaje; set => SetProperty(ref _mensaje, value); }

        public ICommand GenerarVentasCommand { get; }
        public ICommand GenerarTopPlatosCommand { get; }

        public ReportesViewModel(ReporteService? service = null)
        {
            _service = service ?? new ReporteService();
            GenerarVentasCommand = new RelayCommand(GenerarVentas);
            GenerarTopPlatosCommand = new RelayCommand(GenerarTopPlatos);
        }

        private void GenerarVentas(object? _)
        {
            try
            {
                Ventas.Clear();
                foreach (var v in _service.VentasPorRangoDeFechas(FechaInicio, FechaFin)) Ventas.Add(v);
                Mensaje = $"Reporte generado: {Ventas.Count} días con ventas.";
            }
            catch (Exception ex) { Mensaje = $"Error: {ex.Message}"; }
        }

        private void GenerarTopPlatos(object? _)
        {
            try
            {
                TopPlatos.Clear();
                foreach (var t in _service.Top5PlatosMasVendidos()) TopPlatos.Add(t);
                Mensaje = $"Top platos generado: {TopPlatos.Count} platos.";
            }
            catch (Exception ex) { Mensaje = $"Error: {ex.Message}"; }
        }
    }
}
