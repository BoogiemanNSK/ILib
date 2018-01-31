CREATE TABLE [dbo].[DocumentsDB] (
    [PersonID]          INT  DEFAULT ((0)) NOT NULL,
    [Title]        TEXT NOT NULL,
    [Description]      TEXT NOT NULL,
    [Price] TEXT NOT NULL,
    [IsBesteller]    INT  NOT NULL,
    [TimeOfCheckOut] INT  NULL,
	[BookType] INT NOT NULL,
);
