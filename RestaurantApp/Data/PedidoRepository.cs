using System.Collections.Generic;
using System.IO;
using System.Linq;
using RestaurantApp.Models;

namespace RestaurantApp.Data
{
    public class PedidoRepository
    {
        private readonly string _filePath;
        private readonly string _detallesFilePath;

        public PedidoRepository(string filePath = "Data/pedidos.txt", string detallesFilePath = "Data/detalles.txt")
        {
            _filePath = filePath;
            _detallesFilePath = detallesFilePath;
            EnsureFileExists(_filePath);
            EnsureFileExists(_detallesFilePath);
        }

        private void EnsureFileExists(string path)
        {
            var dir = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(dir)) Directory.CreateDirectory(dir);
            if (!File.Exists(path)) File.WriteAllText(path, string.Empty);
        }

        public List<DetallePedido> GetAllDetalles()
        {
            return File.ReadAllLines(_detallesFilePath)
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(DetallePedido.FromString)
                .ToList();
        }

        public List<Pedido> GetAll()
        {
            var detalles = GetAllDetalles();
            return File.ReadAllLines(_filePath)
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(l => {
                    var p = Pedido.FromString(l);
                    p.Detalles = detalles.Where(d => d.PedidoId == p.Id).ToList();
                    return p;
                }).ToList();
        }

        public Pedido? GetById(int id) => GetAll().FirstOrDefault(p => p.Id == id);

        public void Save(Pedido pedido)
        {
            var all = GetAll();
            if (pedido.Id == 0) pedido.Id = all.Count > 0 ? all.Max(p => p.Id) + 1 : 1;
            var existing = all.FirstOrDefault(p => p.Id == pedido.Id);
            if (existing != null) all.Remove(existing);
            all.Add(pedido);
            WriteAll(all);
            SaveDetalles(pedido);
        }

        private void SaveDetalles(Pedido pedido)
        {
            var allDetalles = GetAllDetalles().Where(d => d.PedidoId != pedido.Id).ToList();
            int nextId = allDetalles.Count > 0 ? allDetalles.Max(d => d.Id) + 1 : 1;
            foreach (var d in pedido.Detalles)
            {
                if (d.Id == 0) { d.Id = nextId++; d.PedidoId = pedido.Id; }
                allDetalles.Add(d);
            }
            File.WriteAllLines(_detallesFilePath, allDetalles.Select(d => d.ToString()));
        }

        private void WriteAll(List<Pedido> items) =>
            File.WriteAllLines(_filePath, items.Select(p => p.ToString()));
    }
}
