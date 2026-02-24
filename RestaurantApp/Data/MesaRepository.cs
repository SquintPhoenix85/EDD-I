using System.Collections.Generic;
using System.IO;
using System.Linq;
using RestaurantApp.Models;

namespace RestaurantApp.Data
{
    public class MesaRepository
    {
        private readonly string _filePath;

        public MesaRepository(string filePath = "Data/mesas.txt")
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

        public List<Mesa> GetAll()
        {
            return File.ReadAllLines(_filePath)
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(Mesa.FromString)
                .ToList();
        }

        public Mesa? GetById(int id) => GetAll().FirstOrDefault(m => m.Id == id);

        public void Save(Mesa mesa)
        {
            var all = GetAll();
            if (mesa.Id == 0) mesa.Id = all.Count > 0 ? all.Max(m => m.Id) + 1 : 1;
            var existing = all.FirstOrDefault(m => m.Id == mesa.Id);
            if (existing != null) all.Remove(existing);
            all.Add(mesa);
            WriteAll(all);
        }

        public void Delete(int id)
        {
            var all = GetAll().Where(m => m.Id != id).ToList();
            WriteAll(all);
        }

        private void WriteAll(List<Mesa> items) =>
            File.WriteAllLines(_filePath, items.Select(m => m.ToString()));
    }
}
