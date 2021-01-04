CREATE TABLE [base].[Question] (
    [QuestionId]       UNIQUEIDENTIFIER NOT NULL,
    [Title]          NVARCHAR (255)   NOT NULL,
    [Description]          NVARCHAR (255)   NOT NULL,
    [Tags]          NVARCHAR (255)   NOT NULL,
    CONSTRAINT [PK_Question] PRIMARY KEY CLUSTERED ([QuestionId] ASC)
);
