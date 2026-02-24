using System.Collections.ObjectModel;
using RestaurantApp.Models;
using RestaurantApp.Services;

namespace RestaurantApp.ViewModels
{
    public class FacturasViewModel : BaseViewModel
    {
        private readonly FacturaService _service;
        public ObservableCollection<Factura> Facturas { get; } = new();

        public FacturasViewModel(FacturaService? service = null)
        {
            _service = service ?? new FacturaService();
            Cargar();
        }

        public void Cargar()
        {
            Facturas.Clear();
            foreach (var f in _service.ObtenerTodas()) Facturas.Add(f);
        }
    }
}
