CREATE TABLE [dbo].[tasks] (
    [id]          INT  NOT NULL,
    [description] TEXT NULL,
    [dueDate]     TEXT NULL,
    [completed]   TEXT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

