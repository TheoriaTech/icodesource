﻿CREATE TABLE [dbo].[Snippet]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserName] NVARCHAR(50) NOT NULL, 
    [Title] NVARCHAR(50) NOT NULL, 
    [Language] NVARCHAR(50) NOT NULL, 
    [Code] NVARCHAR(MAX) NOT NULL
)