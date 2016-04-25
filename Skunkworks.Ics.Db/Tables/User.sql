CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[FirstName] NVARCHAR(50) NULL,
	[LastName] NVARCHAR(50) NULL,
    [UserName] NVARCHAR(50) NOT NULL, 
    [PasswordHash] NVARCHAR(100) NULL, 
    [SecurityStamp] NVARCHAR(36) NULL, 
    [Email] NVARCHAR(50) NOT NULL, 
    [EmailConfirmed] BIT NULL, 
    [PhoneNumber] NVARCHAR(10) NULL, 
    [PhoneNumberConfirmed] BIT NULL, 
    [AccessFailedCount] INT NULL, 
    [LockoutEnabled] BIT NULL, 
    [LockoutEndDateUtc] DATETIME NULL, 
    [TwoFactorEnabled] BIT NULL
)
