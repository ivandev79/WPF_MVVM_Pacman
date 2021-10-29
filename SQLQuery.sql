USE master ;
GO
CREATE DATABASE UserScore
--Drop TRIGGER IF EXISTS INSERT_Score
--Drop table IF EXISTS dbo.Score
GO
USE UserScore ;
GO
CREATE TABLE Score
(	ScoreId int IDENTITY(1, 1) PRIMARY KEY not null,
	UserName nvarchar(200), --maby FK
	MyScore int,
	DateAdded DATETIME NOT NULL CONSTRAINT DF_MyTable_CreateDate_GETDATE DEFAULT CURRENT_TIMESTAMP
)
go
Create TRIGGER INSERT_Score ON Score 
FOR  insert
AS BEGIN
    UPDATE Score
        SET DateAdded = CURRENT_TIMESTAMP
		  WHERE ScoreId IN (SELECT MAX(ScoreId) FROM Score)
END;

INSERT INTO Score (UserName,MyScore) values('UFO',22)
INSERT Score  values('Valera',322,11)

