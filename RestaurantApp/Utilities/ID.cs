using RestaurantApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Utilities
{
    public class IdGenerator
    {
        public static int GenerateNextId<T>(List<T> existingEntities) where T : IEntity
        {
            if (existingEntities.Count == 0) return 1;

            // Get all existing IDs in sorted order
            var existingIds = existingEntities.Select(e => e.Id).OrderBy(id => id).ToList();

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
