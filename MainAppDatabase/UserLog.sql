CREATE TABLE [dbo].[UserLog]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY ,
	[UserIp] NVARCHAR(15) NULL, 
    [WordId] INT NULL, 
    [SearchTime] INT NULL, 
    CONSTRAINT [FK_UserLog_Words] FOREIGN KEY (WordId) REFERENCES Words(Id)
)
