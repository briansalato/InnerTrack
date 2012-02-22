CREATE TABLE [dbo].[User_Project]
(
	[UserId] INT NOT NULL , 
    [ProjectId] INT NOT NULL, 
    [CreatedBy] NVARCHAR(100) NOT NULL, 
    [CreatedOn] DATETIME NOT NULL, 
    [UpdatedBy] NVARCHAR(100) NULL, 
    [UpdatedOn] DATETIME NULL, 
    PRIMARY KEY ([ProjectId], [UserId]), 
    CONSTRAINT [FK_User_Project_To_User] FOREIGN KEY (UserId) REFERENCES [User](Id), 
    CONSTRAINT [FK_User_Project_To_Project] FOREIGN KEY (ProjectId) REFERENCES Project(Id)
)
