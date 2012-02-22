CREATE TABLE [dbo].[NewsFeed]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CreatedBy] NVARCHAR(100) NOT NULL, 
    [CreatedOn] DATETIME NOT NULL, 
    [UpdatedBy] NVARCHAR(100) NULL, 
    [UpdatedOn] DATETIME NULL
)
