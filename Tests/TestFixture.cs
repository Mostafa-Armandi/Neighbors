using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Microsoft.Data.SqlClient;
using Neighbors.Models;
using ThrowawayDb;

namespace Tests
{
    public class TestFixture : IDisposable
    {
        private readonly ThrowawayDatabase _database;
        
        public string ConnectionString => _database.ConnectionString;
        public TestFixture()
        {
            _database = ThrowawayDatabase.FromLocalInstance("localhost");

            Console.WriteLine($"Created database {_database.Name}");
            
            ExecuteBatchNonQuery(File.ReadAllText("Scripts/1-InitializeDatabase.sql"), _database.ConnectionString);
        }
        
        private void ExecuteBatchNonQuery(string sql, string connectionString) {
            string sqlBatch = string.Empty;
            var connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(string.Empty, connection);
            connection.Open();
            sql += "\nGO";   
            try {
                foreach (string line in sql.Split(new string[2] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries)) {
                    if (line.ToUpperInvariant().Trim() == "GO") {
                        cmd.CommandText = sqlBatch;
                        if (!string.IsNullOrWhiteSpace(sqlBatch))
                            cmd.ExecuteNonQuery();
                        sqlBatch = string.Empty;
                    } else {
                        sqlBatch += line + "\n";
                    }
                }            
            } finally {
                connection.Close();
            }
        }

        public void InsertLocations(IEnumerable<Location> locations)
        {
            using var connection = new SqlConnection(_database.ConnectionString);

            foreach (var location in locations)
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                
                SqlCommand command = new SqlCommand(string.Empty, connection);

                var commandText =
                    $"INSERT INTO Locations(Address,Coordinate) VALUES (@address,geography::Point(@lat,@long,4326))";
                
                command.CommandText = commandText;

                command.Parameters.AddWithValue("@address", location.Address);
                command.Parameters.AddWithValue("@lat", location.Coordinate.Latitude);
                command.Parameters.AddWithValue("@long", location.Coordinate.Longitude);

                command.ExecuteNonQuery();
            }
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}