using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Neighbors.Models;

namespace Neighbors.Queries
{
    public class LocationQuery : ILocationQuery
    {
        private readonly string _connectionString;
        public LocationQuery(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
            
            if (string.IsNullOrWhiteSpace(_connectionString))
                throw new ArgumentNullException(nameof(_connectionString));
        }

        public async Task<List<Neighbor>> GetNearestLocationsAsync(Coordinate @from, int maxDistance, int maxResults)
        {
            await using var connection = new SqlConnection(_connectionString);

            var parameters = new
            {
                @from.Latitude,
                @from.Longitude,
                MaxDistance = maxDistance,
                MaxResults = maxResults
            };

            var result = await connection
                .QueryAsync<Neighbor>("usp_NearestNeighbors", parameters, commandType: CommandType.StoredProcedure);
            
            return result.AsList();
        }
    }
}