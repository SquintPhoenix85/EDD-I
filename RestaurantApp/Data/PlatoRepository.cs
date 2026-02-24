using System.Collections.Generic;
using System.IO;
using System.Linq;
using RestaurantApp.Models;

namespace RestaurantApp.Data
{
    public class PlatoRepository
    {
        private readonly string _filePath;

        public PlatoRepository(string filePath = "Data/platos.txt")
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

        public List<Plato> GetAll()
        {
            return File.ReadAllLines(_filePath)
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(Plato.FromString)
                .ToList();
        }

        public Plato? GetById(int id) => GetAll().FirstOrDefault(p => p.Id == id);

        public void Save(Plato plato)
        {
            var all = GetAll();
            if (plato.Id == 0) plato.Id = all.Count > 0 ? all.Max(p => p.Id) + 1 : 1;
            var existing = all.FirstOrDefault(p => p.Id == plato.Id);
            if (existing != null) all.Remove(existing);
            all.Add(plato);
            WriteAll(all);
        }

        public void Delete(int id)
        {
            var all = GetAll().Where(p => p.Id != id).ToList();
            WriteAll(all);
        }

        private void WriteAll(List<Plato> items) =>
            File.WriteAllLines(_filePath, items.Select(p => p.ToString()));
    }
}
