CREATE TABLE [dbo].[Table]
(
    [prodId] INT NOT NULL, 
    [prodName] NCHAR(20) NULL, 
    [prodPrice] INT NULL, 
    [prodQty] INT NULL, 
    [prodCat] NCHAR(30) NULL, 
    CONSTRAINT [PK_Table] PRIMARY KEY ([prodId])
)
