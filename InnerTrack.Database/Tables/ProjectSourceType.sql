CREATE TABLE [dbo].[ProjectSourceType]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[Name] NVARCHAR(50) NOT NULL,
    [AssemblyName] NVARCHAR(200) NOT NULL DEFAULT(''),
    [FullClassName] NVARCHAR(200) NOT NULL DEFAULT(''),
    [Interval] INT NOT NULL, 
    [LastRun] DATETIME NULL, 
    [NextRun] DATETIME NOT NULL, 
	[Enabled] BIT NOT NULL,
    [CreatedBy] NVARCHAR(100) NOT NULL, 
    [CreatedOn] DATETIME NOT NULL, 
    [UpdatedBy] NVARCHAR(100) NULL, 
    [UpdatedOn] DATETIME NULL
)
