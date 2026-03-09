using RestaurantApp.Models;
using RestaurantApp.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace RestaurantApp.Views
{
    public partial class MesasView : UserControl
    {
        public MesasView() => InitializeComponent();

        private bool _isDragging;
        private Mesa? _draggedMesa;
        private Point _clickPos;

        private void Mesa_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var element = sender as FrameworkElement;
            _draggedMesa = element?.DataContext as Mesa;
            if (_draggedMesa != null)
            {
                _isDragging = true;
                _clickPos = e.GetPosition(element);
                element.CaptureMouse(); // Esto hace que todos los eventos del mouse se mande al elemento en especifico

                var vm = (MesasViewModel)this.DataContext;
                vm.SelectedMesa = _draggedMesa;
            }
        }

        private void Mesa_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging && _draggedMesa != null)
            {
                var element = sender as FrameworkElement;
                var container = VisualTreeHelper.GetParent(element) as UIElement;
                var canvas = VisualTreeHelper.GetParent(container) as Canvas;

                if (canvas != null)
                {
                    Point currentMousePos = e.GetPosition(canvas);
                    // Para que no se salga del canvas
                    double newX = Math.Clamp(currentMousePos.X - _clickPos.X, 0, canvas.ActualWidth - element.ActualWidth);
                    double newY = Math.Clamp(currentMousePos.Y - _clickPos.Y, 0, canvas.ActualHeight - element.ActualHeight);
                    _draggedMesa.X = newX;
                    _draggedMesa.Y = newY;
                }
            }
        }

        private void Mesa_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_isDragging)
            {
                _isDragging = false;
                (sender as FrameworkElement)?.ReleaseMouseCapture();

                var vm = (MesasViewModel)this.DataContext;
                vm.GuardarCommand.Execute(null);
            }
        }
    }
}
