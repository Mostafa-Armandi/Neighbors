# Nearest Neighbor search system

This repo contains an implementation for an otimzed Nearest Neighbor locations search system.  
It heavily relies on [SQL SERVER Spatial Data](https://docs.microsoft.com/en-us/sql/relational-databases/spatial/spatial-data-sql-server) and utilizes [Spatial Indexes](https://docs.microsoft.com/en-us/sql/relational-databases/spatial/create-modify-and-drop-spatial-indexes) for fast searches based on [Bounding box](https://aboutsqlserver.com/2013/09/03/optimizing-sql-server-spatial-queries-with-bounding-box/)  concept.

The distance calculation between locations on SQL Server is done using `STDistance` function which returns the shortest LineString between two geography types.

The underlying algorithm for `STDistance` seems to be the [Vincenty’s inverse solution](http://www.movable-type.co.uk/scripts/latlong-vincenty.html) which is slightly different from the well-know [Harvesine](http://www.movable-type.co.uk/scripts/latlong.html)  approach. They might shows slightly different results on some *Latitude/Longitudes* thoutgh.

A proof checking reference for distance calculation using the implemented approach could be found [here](https://geodesyapps.ga.gov.au/vincenty-inverse).


# Architecture
This is an ASP.NET  *Web Api* application which uses *Dapper* which is a micro-orm for data access which shows a better performance comapring with other ORMs (eg. EF Core).

SQL Server 2019 is used as the database and distance calculation is done using a user-definded SP named `usp_NearestNeighbors`.

## How to get started

**Database** 
Follow either of following ways:

 1. Restore a backup from `Database/Neighbors.bak` .
		 This a SQL Server 2019 backup file with pre loaded data fro thesting purposes.
		 
 2. Execute 2 following file from the path `Tests/Scripts` respectively:
    ```
    0-CreateDatabase.sql
    1-InitializeDatabase.sql
    ```
  
   
**Running the project**

Use Visual studio or Rider to run the project. Swagger is there to send a request to the `GetNearestLocations` endpoint.
