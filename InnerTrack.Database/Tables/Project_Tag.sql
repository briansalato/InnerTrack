CREATE TABLE [dbo].[Project_Tag]
(
	[ProjectId] INT NOT NULL , 
    [TagId] INT NOT NULL, 
    [CreatedBy] NVARCHAR(100) NOT NULL, 
    [CreatedOn] DATETIME NOT NULL, 
    [UpdatedBy] NVARCHAR(100) NULL, 
    [UpdatedOn] DATETIME NULL, 
    PRIMARY KEY ([TagId], [ProjectId]), 
    CONSTRAINT [FK_Project_Tag_To_Project] FOREIGN KEY (ProjectId) REFERENCES Project(Id), 
    CONSTRAINT [FK_Project_Tag_To_Tag] FOREIGN KEY (TagId) REFERENCES Tag(Id)
)
