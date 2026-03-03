using System.Windows;
using RestaurantApp.ViewModels;

namespace RestaurantApp
{
    public partial class MainWindow : Window
    {
        public MainWindow() {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void TabControl_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (DataContext is MainViewModel vm && e.AddedItems.Count > 0)
            {
                var selectedTab = e.AddedItems[0] as System.Windows.Controls.TabItem;
                switch (selectedTab?.Header.ToString())
                {
                    case "🍽️ Platos":
                        vm.PlatosVM.RefreshData();
                        break;
                    case "🪑 Mesas":
                        vm.MesasVM.RefreshData();
                        break;
                    case "📋 Pedidos":
                        vm.PedidosVM.RefreshData();
                        break;
                    case "🧾 Facturas":
                        vm.FacturasVM.RefreshData();
                        break;
                    case "📊 Reportes":
                        break;
                }
            }
        }
    }
}