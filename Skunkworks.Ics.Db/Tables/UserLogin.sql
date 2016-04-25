CREATE TABLE [dbo].[UserLogin]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NULL, 
    [LoginProvider] NVARCHAR(100) NULL, 
    [ProviderKey] NVARCHAR(100) NULL
)
