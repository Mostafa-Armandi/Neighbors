using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Roamler.Models;
using Roamler.Queries;
using Xunit;

namespace Tests.Queries
{
    public class LocationQueryTests
    {
        const int LatitudeWidth = 111000;

        [Fact]
        private void GetNearestLocationsAsync_ShouldFindNearLocation()
        {
            // Arrange

            using var testFixture = new TestFixture();

            testFixture.InsertLocations(new[]
            {
                new Location {Address = "add1", Coordinate = new Coordinate(0.99f, 0f)}
            });

            var query = new LocationQuery(CreateConfiguration(testFixture.ConnectionString));

            // Act
            var coordinate = new Coordinate(0, 0);
            var result = query.GetNearestLocationsAsync(coordinate, LatitudeWidth, 1).Result;

            // Assert
            Assert.Single(result);
        }

        [Fact]
        private void GetNearestLocationsAsync_ShouldReturnOnlyInBoundaryLocations()
        {
            // Arrange
            using var testFixture = new TestFixture();

            testFixture.InsertLocations(new[]
            {
                new Location {Address = "add1", Coordinate = new Coordinate(0.5f, 0.5f)},
                new Location {Address = "add2", Coordinate = new Coordinate(0.6f, 0.6f)},
                new Location {Address = "add3", Coordinate = new Coordinate(1.2f, 1.2f)}
            });

            var query = new LocationQuery(CreateConfiguration(testFixture.ConnectionString));

            // Act
            var coordinate = new Coordinate(0, 0);
            var result = query.GetNearestLocationsAsync(coordinate, LatitudeWidth, 20).Result;

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        private void GetNearestLocationsAsync_ShouldCalculateDistanceCorrectly()
        {
            // Arrange
            using var testFixture = new TestFixture();
            const int rounder = 1;
            const int thisDistance = 4475703; // ref: https://geodesyapps.ga.gov.au/vincenty-inverse

            testFixture.InsertLocations(new[]
            {
                new Location {Address = "add1", Coordinate = new Coordinate(80f, 120f)},
            });

            var query = new LocationQuery(CreateConfiguration(testFixture.ConnectionString));

            // Act
            var coordinate = new Coordinate(40, 130);
            var result = query.GetNearestLocationsAsync(coordinate, thisDistance + rounder, 1).Result;

            // Assert
            Assert.Single(result, n => (int) Math.Round(n.Distance) == thisDistance);
        }


        private IConfiguration CreateConfiguration(string connectionString)
        {
            var inMemorySettings = new Dictionary<string, string>
            {
                {"ConnectionStrings:DefaultConnection", connectionString},
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            return configuration;
        }
    }
}