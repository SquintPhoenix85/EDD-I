using System.Collections.Generic;
using System.IO;
using System.Linq;
using RestaurantApp.Models;

namespace RestaurantApp.Data
{
    public class FacturaRepository
    {
        private readonly string _filePath;

        public FacturaRepository(string filePath = "Data/facturas.txt")
        {
            _filePath = filePath;
            EnsureFileExists();
        }

        private void EnsureFileExists()
        {
            var dir = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(dir)) Directory.CreateDirectory(dir);
            if (!File.Exists(_filePath)) File.WriteAllText(_filePath, string.Empty);
        }

        public List<Factura> GetAll()
        {
            return File.ReadAllLines(_filePath)
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(Factura.FromString)
                .ToList();
        }

        public void Save(Factura factura)
        {
            var all = GetAll();
            if (factura.Id == 0) factura.Id = all.Count > 0 ? all.Max(f => f.Id) + 1 : 1;
            var existing = all.FirstOrDefault(f => f.Id == factura.Id);
            if (existing != null) all.Remove(existing);
            all.Add(factura);
            WriteAll(all);
        }

        private void WriteAll(List<Factura> items) =>
            File.WriteAllLines(_filePath, items.Select(f => f.ToString()));
    }
}
