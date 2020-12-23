CREATE TABLE [dbo].[PasswordTable]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(50) NOT NULL, 
    [Password] VARCHAR(50) NOT NULL, 
    [RegisterPassword] INT NOT NULL
)
