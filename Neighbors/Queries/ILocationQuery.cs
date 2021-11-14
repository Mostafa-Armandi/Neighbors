using System.Collections.Generic;
using System.Threading.Tasks;
using Neighbors.Models;

namespace Neighbors.Queries
{
    public interface ILocationQuery
    {
        Task<List<Neighbor>> GetNearestLocationsAsync(Coordinate @from, int maxDistance, int maxResults);
    }
}