--create database AshenCrownDb
USE master
GO

/*******************************************************************************
   Drop database if it exists
********************************************************************************/
IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'AshenCrownDb')
BEGIN
ALTER DATABASE AshenCrownDb SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
DROP DATABASE AshenCrownDb;
END

GO
CREATE DATABASE AshenCrownDb
GO
USE AshenCrownDb
GO
create table Mission(
Id int primary key,
Content nvarchar(max),
IsComplete bit default(0)
)