using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using RestaurantApp.Models;
using RestaurantApp.Services;

namespace RestaurantApp.ViewModels
{
    public class MesasViewModel : BaseViewModel
    {
        private readonly MesaService _service;
        private Mesa? _selectedMesa;
        private string _numero = string.Empty;
        private string _capacidad = string.Empty;
        private string _mensaje = string.Empty;
        private bool _editMode;

        public ObservableCollection<Mesa> Mesas { get; } = new();

        public Mesa? SelectedMesa
        {
            get => _selectedMesa;
            set
            {
                SetProperty(ref _selectedMesa, value);
                if (value != null)
                {
                    Numero = value.Numero.ToString();
                    Capacidad = value.Capacidad.ToString();
                    EditMode = true;
                }
            }
        }

        public string Numero { get => _numero; set => SetProperty(ref _numero, value); }
        public string Capacidad { get => _capacidad; set => SetProperty(ref _capacidad, value); }
        public string Mensaje { get => _mensaje; set => SetProperty(ref _mensaje, value); }
        public bool EditMode { get => _editMode; set => SetProperty(ref _editMode, value); }

        public ICommand GuardarCommand { get; }
        public ICommand EliminarCommand { get; }
        public ICommand NuevoCommand { get; }

        public MesasViewModel(MesaService? service = null)
        {
            _service = service ?? new MesaService();
            GuardarCommand = new RelayCommand(Guardar);
            EliminarCommand = new RelayCommand(Eliminar);
            NuevoCommand = new RelayCommand(Nuevo);
            CargarMesas();
        }

        private void CargarMesas()
        {
            Mesas.Clear();
            foreach (var m in _service.ObtenerTodas()) Mesas.Add(m);
        }

        private void Guardar(object? _)
        {
            try
            {
                if (!int.TryParse(Numero, out var num)) throw new ArgumentException("Número inválido.");
                if (!int.TryParse(Capacidad, out var cap)) throw new ArgumentException("Capacidad inválida.");

                if (EditMode && SelectedMesa != null)
                {
                    SelectedMesa.Numero = num;
                    SelectedMesa.Capacidad = cap;
                    _service.Actualizar(SelectedMesa);
                    Mensaje = "Mesa actualizada.";
                }
                else
                {
                    _service.Agregar(num, cap);
                    Mensaje = "Mesa agregada.";
                }
                Nuevo(null);
                CargarMesas();
            }
            catch (Exception ex) { Mensaje = $"Error: {ex.Message}"; }
        }

        private void Eliminar(object? _)
        {
            if (SelectedMesa == null) { Mensaje = "Seleccione una mesa."; return; }
            try
            {
                _service.Eliminar(SelectedMesa.Id);
                Mensaje = "Mesa eliminada.";
                Nuevo(null);
                CargarMesas();
            }
            catch (Exception ex) { Mensaje = $"Error: {ex.Message}"; }
        }

        private void Nuevo(object? _)
        {
            _selectedMesa = null;
            OnPropertyChanged(nameof(SelectedMesa));
            Numero = string.Empty;
            Capacidad = string.Empty;
            EditMode = false;
            Mensaje = string.Empty;
        }
    }
}
