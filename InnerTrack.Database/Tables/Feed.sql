CREATE TABLE [dbo].[Feed]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[FeedTypeId] INT NOT NULL,
    [CreatedBy] NVARCHAR(100) NOT NULL, 
    [CreatedOn] DATETIME NOT NULL, 
    [UpdatedBy] NCHAR(10) NULL, 
    [UpdatedOn] DATETIME NULL, 
    CONSTRAINT [FK_Feed_To_FeedType] FOREIGN KEY (FeedTypeId) REFERENCES FeedType(Id)
)
