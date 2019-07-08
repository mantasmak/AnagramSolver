CREATE TABLE [dbo].[CachedWords]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [WordId] INT NULL, 
    [AnagramId] INT NULL, 
    CONSTRAINT [FK_CachedWords_Words] FOREIGN KEY (WordId) REFERENCES Words(Id), 
    CONSTRAINT [FK_CachedWords_Anagrams] FOREIGN KEY (AnagramId) REFERENCES Words(Id)
)
