namespace RestaurantApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public PlatosViewModel PlatosVM { get; } = new();
        public MesasViewModel MesasVM { get; } = new();
        public PedidosViewModel PedidosVM { get; } = new();
        public FacturasViewModel FacturasVM { get; } = new();
        public ReportesViewModel ReportesVM { get; } = new();
    }
}
