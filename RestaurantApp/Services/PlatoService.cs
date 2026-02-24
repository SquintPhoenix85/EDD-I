using System.Collections.Generic;
using RestaurantApp.Data;
using RestaurantApp.Models;

namespace RestaurantApp.Services
{
    public class PlatoService
    {
        private readonly PlatoRepository _repo;
        public PlatoService(PlatoRepository? repo = null) => _repo = repo ?? new PlatoRepository();

        public List<Plato> ObtenerTodos() => _repo.GetAll();

        public void Agregar(string nombre, decimal precio)
        {
            if (string.IsNullOrWhiteSpace(nombre)) throw new System.ArgumentException("El nombre no puede estar vacío.");
            if (precio <= 0) throw new System.ArgumentException("El precio debe ser mayor que 0.");
            _repo.Save(new Plato { Nombre = nombre, Precio = precio, Disponible = true });
        }

        public void Actualizar(Plato plato)
        {
            if (string.IsNullOrWhiteSpace(plato.Nombre)) throw new System.ArgumentException("El nombre no puede estar vacío.");
            if (plato.Precio <= 0) throw new System.ArgumentException("El precio debe ser mayor que 0.");
            _repo.Save(plato);
        }

        public void Eliminar(int id) => _repo.Delete(id);

        public void CambiarDisponibilidad(int id, bool disponible)
        {
            var plato = _repo.GetById(id) ?? throw new System.Exception("Plato no encontrado.");
            plato.Disponible = disponible;
            _repo.Save(plato);
        }
    }
}
