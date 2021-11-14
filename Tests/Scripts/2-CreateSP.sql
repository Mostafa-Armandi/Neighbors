CREATE PROCEDURE [dbo].[usp_NearestNeighbors]
(
	@Latitude float,
	@Longitude float,
	@MaxDistance int,
	@MaxResults int
)
AS

DECLARE @g GEOGRAPHY = geography::Point(@Latitude, @Longitude, 4326)

SELECT  [Address] , Coordinate.Lat Latitude, Coordinate.Long Longitude, Coordinate.STDistance(@g) Distance
FROM [Locations] l
    INNER JOIN
    (
    SELECT TOP(@MaxResults)  Id
    FROM [Locations]
    WHERE  Coordinate.STDistance(@g) IS NOT NULL AND Coordinate.STDistance(@g) <= @MaxDistance
    ORDER BY Coordinate.STDistance(@g)
    ) d ON d.Id = l.Id
