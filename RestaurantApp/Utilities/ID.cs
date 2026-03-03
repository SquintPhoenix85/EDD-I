using RestaurantApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Utilities
{
    public class ID
    {
        public static int GenerateNextId(List<Mesa> existingMesas)
        {
            if (existingMesas.Count == 0) return 1;

            // Get all existing IDs in sorted order
            var existingIds = existingMesas.Select(m => m.Id).OrderBy(id => id).ToList();

            // Find the first gap in the sequence
            int expectedId = 1;
            foreach (var id in existingIds)
            {
                if (id > expectedId)
                {
                    // Found a gap, use it
                    return expectedId;
                }
                expectedId = id + 1;
            }

            // No gaps found, return the next sequential ID
            return expectedId;
        }

        public static int GenerateNextId(List<Plato> existingPlato)
        {
            if (existingPlato.Count == 0) return 1;

            // Get all existing IDs in sorted order
            var existingIds = existingPlato.Select(m => m.Id).OrderBy(id => id).ToList();

            // Find the first gap in the sequence
            int expectedId = 1;
            foreach (var id in existingIds)
            {
                if (id > expectedId)
                {
                    // Found a gap, use it
                    return expectedId;
                }
                expectedId = id + 1;
            }

            // No gaps found, return the next sequential ID
            return expectedId;
        }

        public static int GenerateNextId(List<Pedido> existingPedido)
        {
            if (existingPedido.Count == 0) return 1;

            // Get all existing IDs in sorted order
            var existingIds = existingPedido.Select(m => m.Id).OrderBy(id => id).ToList();

            // Find the first gap in the sequence
            int expectedId = 1;
            foreach (var id in existingIds)
            {
                if (id > expectedId)
                {
                    // Found a gap, use it
                    return expectedId;
                }
                expectedId = id + 1;
            }

            // No gaps found, return the next sequential ID
            return expectedId;
        }

        public static int GenerateNextId(List<Factura> existingFactura)
        {
            if (existingFactura.Count == 0) return 1;

            // Get all existing IDs in sorted order
            var existingIds = existingFactura.Select(m => m.Id).OrderBy(id => id).ToList();

            // Find the first gap in the sequence
            int expectedId = 1;
            foreach (var id in existingIds)
            {
                if (id > expectedId)
                {
                    // Found a gap, use it
                    return expectedId;
                }
                expectedId = id + 1;
            }

            // No gaps found, return the next sequential ID
            return expectedId;
        }
    }
}
