using System.Collections.Generic;
using RestaurantApp.Data;
using RestaurantApp.Models;

namespace RestaurantApp.Services
{
    public class MesaService
    {
        private readonly MesaRepository _repo;
        public MesaService(MesaRepository? repo = null) => _repo = repo ?? new MesaRepository();

        public List<Mesa> ObtenerTodas() => _repo.GetAll();

        public void Agregar(int numero, int capacidad)
        {
            if (numero <= 0) throw new System.ArgumentException("El número de mesa debe ser mayor que 0.");
            if (capacidad <= 0) throw new System.ArgumentException("La capacidad debe ser mayor que 0.");
            var all = _repo.GetAll();
            if (all.Exists(m => m.Numero == numero)) throw new System.Exception($"Ya existe una mesa con el número {numero}.");
            _repo.Save(new Mesa { Numero = numero, Capacidad = capacidad, Estado = EstadoMesa.Libre });
        }

        public void Actualizar(Mesa mesa)
        {
            if (mesa.Numero <= 0) throw new System.ArgumentException("El número de mesa debe ser mayor que 0.");
            if (mesa.Capacidad <= 0) throw new System.ArgumentException("La capacidad debe ser mayor que 0.");
            _repo.Save(mesa);
        }

        public void Eliminar(int id) => _repo.Delete(id);

        public void CambiarEstado(int id, EstadoMesa estado)
        {
            var mesa = _repo.GetById(id) ?? throw new System.Exception("Mesa no encontrada.");
            mesa.Estado = estado;
            _repo.Save(mesa);
        }
    }
}
