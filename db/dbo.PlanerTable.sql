CREATE TABLE [dbo].[PlanerTable] (
    [Id]           INT          IDENTITY (1,1) NOT NULL,
    [Uhrzeit]      VARCHAR (10) NULL,
    [Tätigkeit]    VARCHAR (50) NULL,
    [Beschreibung] VARCHAR (50) NULL,
    [Erledigt]     VARCHAR (10)   NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

