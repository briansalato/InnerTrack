CREATE TABLE [dbo].[ProjectSource]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [ProjectSourceTypeId] INT NOT NULL,  
    [Configuration] NVARCHAR(MAX) NULL, 
    [CreatedBy] NVARCHAR(100) NOT NULL, 
    [CreatedOn] DATETIME NOT NULL, 
    [UpdatedBy] NVARCHAR(100) NULL, 
    [UpdatedOn] DATETIME NULL, 
    CONSTRAINT [FK_ProjectSource_To_ProjectSourceType] FOREIGN KEY (ProjectSourceTypeId) REFERENCES [ProjectSourceType](Id)
)
