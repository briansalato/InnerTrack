CREATE TABLE [dbo].[ProjectSourceEntry]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ProjectSourceId] INT NOT NULL, 
	[SourceEntryId] INT NOT NULL,
	[Title] NVARCHAR(1000) NOT NULL,
	[Message] NVARCHAR(MAX) NULL,
    [CreatedBy] NVARCHAR(100) NOT NULL, 
    [CreatedOn] DATETIME NOT NULL, 
    [UpdatedBy] NVARCHAR(100) NULL, 
    [UpdatedOn] DATETIME NULL, 
    CONSTRAINT [FK_ProjectSourceEntry_To_ProjectSource] FOREIGN KEY (ProjectSourceId) REFERENCES ProjectSource(Id))