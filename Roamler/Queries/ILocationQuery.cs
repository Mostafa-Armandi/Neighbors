using System.Collections.Generic;
using System.Threading.Tasks;
using Roamler.Models;

namespace Roamler.Queries
{
    public interface ILocationQuery
    {
        Task<List<Neighbor>> GetNearestLocationsAsync(Coordinate @from, int maxDistance, int maxResults);
    }
}