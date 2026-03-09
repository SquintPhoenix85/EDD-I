using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using RestaurantApp.Models;
using RestaurantApp.Services;

namespace RestaurantApp.ViewModels
{
    public class PlatosViewModel : BaseViewModel
    {
        private readonly PlatoService _service;
        private Plato? _selectedPlato;
        private string _nombre = string.Empty;
        private string _precio = string.Empty;
        private string _mensaje = string.Empty;
        private bool _editMode;

        public ObservableCollection<Plato> Platos { get; } = new();

        public Plato? SelectedPlato
        {
            get => _selectedPlato;
            set
            {
                SetProperty(ref _selectedPlato, value);
                if (value != null)
                {
                    Nombre = value.Nombre;
                    Precio = value.Precio.ToString("F2");
                    EditMode = true;
                }
            }
        }

        public string Nombre { get => _nombre; set => SetProperty(ref _nombre, value); }
        public string Precio { get => _precio; set => SetProperty(ref _precio, value); }
        public string Mensaje { get => _mensaje; set => SetProperty(ref _mensaje, value); }
        public bool EditMode { get => _editMode; set => SetProperty(ref _editMode, value); }

        public ICommand GuardarCommand { get; }
        public ICommand EliminarCommand { get; }
        public ICommand NuevoCommand { get; }
        public ICommand ToggleDisponibilidadCommand { get; }
        public ICommand SelectPlatoCommand { get; }

        public PlatosViewModel(PlatoService? service = null)
        {
            _service = service ?? new PlatoService();
            GuardarCommand = new RelayCommand(Guardar);
            EliminarCommand = new RelayCommand(Eliminar);
            NuevoCommand = new RelayCommand(Nuevo);
            ToggleDisponibilidadCommand = new RelayCommand(ToggleDisponibilidad);
            SelectPlatoCommand = new RelayCommand(SelectPlato);
            CargarPlatos();
        }

        private void CargarPlatos()
        {
            Platos.Clear();
            foreach (var p in _service.ObtenerTodos()) Platos.Add(p);
        }

        public void RefreshData()
        {
            this.CargarPlatos();
        }

        private void Guardar(object? _)
        {
            try
            {
                if (!decimal.TryParse(Precio, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out var precio))
                    throw new ArgumentException("Precio inválido.");

                if (EditMode && SelectedPlato != null)
                {
                    SelectedPlato.Nombre = Nombre;
                    SelectedPlato.Precio = precio;
                    _service.Actualizar(SelectedPlato);
                    Mensaje = "Plato actualizado correctamente.";
                }
                else
                {
                    _service.Agregar(Nombre, precio);
                    Mensaje = "Plato agregado correctamente.";
                }
                Nuevo(null);
                CargarPlatos();
            }
            catch (Exception ex) { Mensaje = $"Error: {ex.Message}"; }
        }

        private void Eliminar(object? _)
        {
            if (SelectedPlato == null) { Mensaje = "Seleccione un plato."; return; }
            try
            {
                _service.Eliminar(SelectedPlato.Id);
                Mensaje = "Plato eliminado.";
                Nuevo(null);
                CargarPlatos();
            }
            catch (Exception ex) { Mensaje = $"Error: {ex.Message}"; }
        }

        private void ToggleDisponibilidad(object? _)
        {
            if (SelectedPlato == null) { Mensaje = "Seleccione un plato."; return; }
            try
            {
                int platoId = SelectedPlato.Id;
                _service.CambiarDisponibilidad(platoId, !SelectedPlato.Disponible);
                Mensaje = "Disponibilidad actualizada.";
                CargarPlatos();

                // Actualizar el SelectedPlato con el objeto recargado
                SelectedPlato = Platos.FirstOrDefault(p => p.Id == platoId);
            }
            catch (Exception ex) { Mensaje = $"Error: {ex.Message}"; }
        }

        private void Nuevo(object? _)
        {
            _selectedPlato = null;
            OnPropertyChanged(nameof(SelectedPlato));
            Nombre = string.Empty;
            Precio = string.Empty;
            EditMode = false;
            Mensaje = string.Empty;
        }

        private void SelectPlato(object? parameter)
        {
            if (parameter is Plato plato)
            {
                SelectedPlato = plato;
            }
        }
    }
}
