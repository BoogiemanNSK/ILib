CREATE TABLE [dbo].[Table]
(
	[ID] INT NOT NULL PRIMARY KEY DEFAULT 0, 
    [Name] NCHAR(10) NOT NULL, 
    [Adress] NCHAR(10) NOT NULL, 
    [PhoneNumber] NCHAR(10) NOT NULL, 
    [LCNumber] INT NOT NULL, 
    [CheckBookID] INT NOT NULL, 
    [UserType] NCHAR(10) NOT NULL
)
