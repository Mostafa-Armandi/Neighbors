SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Locations](
    [Address] [nvarchar](512) NULL,
    [Coordinate] [geography] NOT NULL,
    [Id] [bigint] IDENTITY(1,1) NOT NULL,
    CONSTRAINT [PK_Locations] PRIMARY KEY CLUSTERED
(
[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    GO

    SET ARITHABORT ON
    SET CONCAT_NULL_YIELDS_NULL ON
    SET QUOTED_IDENTIFIER ON
    SET ANSI_NULLS ON
    SET ANSI_PADDING ON
    SET ANSI_WARNINGS ON
    SET NUMERIC_ROUNDABORT OFF
    GO

GO

CREATE SPATIAL INDEX [IX_Coordinate_Spatial] ON [dbo].[Locations]
(
	[Coordinate]
)USING  GEOGRAPHY_GRID 
WITH (GRIDS =(LEVEL_1 = LOW,LEVEL_2 = LOW,LEVEL_3 = LOW,LEVEL_4 = LOW), 
CELLS_PER_OBJECT = 16, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

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