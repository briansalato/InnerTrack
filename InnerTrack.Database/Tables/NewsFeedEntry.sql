CREATE TABLE [dbo].[NewsFeedEntry]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [NewsFeedId] INT NOT NULL, 
    [Title] NVARCHAR(500) NOT NULL, 
    [Message] NVARCHAR(MAX) NULL, 
    [CreatedBy] NVARCHAR(100) NOT NULL, 
    [CreatedOn] DATETIME NOT NULL, 
    [UpdatedBy] NVARCHAR(100) NULL, 
    [UpdatedOn] DATETIME NULL, 
    CONSTRAINT [FK_NewsFeedEntry_To_NewsFeed] FOREIGN KEY (NewsFeedId) REFERENCES NewsFeed(Id)
)
