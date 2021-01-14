CREATE TABLE [dbo].[PlanerTable]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Uhrzeit] VARCHAR(50) NULL, 
    [Tätigkeit] VARCHAR(50) NULL, 
    [Beschreibung] VARCHAR(50) NULL, 
    [Erledigt] NCHAR(10) NULL
)
