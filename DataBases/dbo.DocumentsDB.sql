CREATE TABLE [dbo].[DocumentsDB]
(
	[title] NCHAR(50) NOT NULL PRIMARY KEY, 
    [personID] INT NULL, 
    [description] TEXT NULL, 
    [price] INT NULL, 
    [isBesteller] INT NULL, 
    [timeOfChekOut] TIME NULL, 
    [docType] INT NULL
)
