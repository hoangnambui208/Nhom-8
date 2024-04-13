CREATE TABLE [dbo].[Bills] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Date]        DATE  NULL,
    [TotalAmount] INT  NULL,
    [Items]       NVARCHAR (MAX) NULL,
    [Month] NCHAR(10) NULL, 
    [Year] NCHAR(10) NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

