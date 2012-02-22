CREATE TABLE [dbo].[Project_Feed]
(
	[ProjectId] INT NOT NULL,
	[FeedId] INT NOT NULL, 
    [CreatedBy] NVARCHAR(100) NOT NULL, 
    [CreatedOn] DATETIME NOT NULL, 
    [UpdatedBy] NVARCHAR(100) NULL, 
    [UpdatedOn] DATETIME NULL, 
    CONSTRAINT [PK_Project_Feed] PRIMARY KEY ([FeedId], [ProjectId]), 
    CONSTRAINT [FK_Project_Feed_To_Project] FOREIGN KEY (ProjectId) REFERENCES Project(Id), 
    CONSTRAINT [FK_Project_Feed_To_Feed] FOREIGN KEY (FeedId) REFERENCES Feed(Id)
)
