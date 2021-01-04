CREATE VIEW [user].[Question]
WITH SCHEMABINDING
AS
SELECT p.QuestionId,
       p.Title,
       p.Description,
       p.Tags
FROM
       base.Question AS p;

    Go

GRANT SELECT ON OBJECT::[user].[Question] TO [ForumAdmin] AS [dbo];
GO

GRANT UPDATE ON OBJECT::[user].[Question] TO [ForumAdmin] AS [dbo];
GO

GRANT INSERT ON OBJECT::[user].[Question] TO [ForumAdmin] AS [dbo];
GO

GRANT DELETE ON OBJECT::[user].[Question] TO [ForumAdmin] AS [dbo];
GO

GRANT SELECT ON OBJECT::[user].[Question] TO [AppUser] AS [dbo];
GO

GRANT UPDATE ON OBJECT::[user].[Question] TO [AppUser] AS [dbo];
GO

GRANT INSERT ON OBJECT::[user].[Question] TO [AppUser] AS [dbo];
GO

GRANT DELETE ON OBJECT::[user].[Question] TO [AppUser] AS [dbo];
GO

