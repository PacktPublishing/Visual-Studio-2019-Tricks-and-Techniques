USE WritingExample;
GO

CREATE TABLE [dbo].[WritingInstrument] (
    [Id]          INT           NOT NULL IDENTITY,
    [Name]        VARCHAR (250) NOT NULL,
    [Description] VARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
