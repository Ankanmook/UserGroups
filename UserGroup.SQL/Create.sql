﻿--start from default
Use [master]

--If Exists => Drop 
DROP DATABASE IF EXISTS [PersonGroups] 
GO

--Creates new database
CREATE DATABASE [PersonGroups]
GO

--Create User login connections for connection strings

--switch to the database
Use PersonGroups
GO

--Create table group
CREATE TABLE [Group]
(
	[Id] int not null IDENTITY(1,1),
	[Name] varchar(128) not null, -- need to write some validation tests
	CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED ([Id] Asc)
)
GO

--Index creation on Group Name
--Name of group should be unique - why common sense 
CREATE UNIQUE NONCLUSTERED INDEX IX_Group_Name
ON [Group]
(
	[Name]
)


--Create table Person with one to one relationship with group
--Reason for 1 to 1 not empty relationship => Point 3 in Create database =>Group they belong to 
CREATE TABLE [Person]
(
	[Id] int not null IDENTITY(1,1),
	[Name] varchar(128) not null, --need to write some tests validation
	[GroupId] int not null CONSTRAINT FK_Person_Group REFERENCES [Group](Id),
	[DateAdded] DateTime not null Default(GETUTCDATE()),
	CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED ([Id] Asc)
)
GO

--Index creation on Name
CREATE NONCLUSTERED INDEX IX_Person_Name
ON [Person]
(
	[Name]
)

GO

--store proc to search name

CREATE OR ALTER Proc usp_Search 
@Name varchar(128) = '', 
@Group varchar(128) = '', 
@PageNumber int = 1, 
@PageSize int = 100,
@SortColumn NVARCHAR(20) = 'Name',  
@SortOrder NVARCHAR(20) = 'ASC'  

AS
BEGIN

--common table expression for results
WITH CTE_Result AS
(
	SELECT 
	p.[Name], p.[DateAdded], g.[Name] as GroupName, p.[Id], g.[Id] as GroupId
	 FROM
	[Person] p 
	Inner Join 
	[Group] g
	On
	p.GroupId = g.Id
	WHERE
	( @Name is null or p.[Name] like '%'+ @Name + '%') -- this will yeild all results
	AND
	(@Group is null or g.[Name] like '%'+ @Group + '%') -- this will yeild all results
	
	ORDER BY  
            CASE WHEN (@SortColumn = 'Name' AND @SortOrder='ASC')  
                        THEN p.[Name]  
            END ASC,  
            CASE WHEN (@SortColumn = 'Name' AND @SortOrder='DESC')  
                        THEN p.[Name]    
            END DESC,  
            CASE WHEN (@SortColumn = 'Group' AND @SortOrder='ASC')  
                        THEN g.[Name]  
            END ASC,  
            CASE WHEN (@SortColumn = 'Group' AND @SortOrder='DESC')  
                        THEN g.[Name]    
			END DESC            

	OFFSET @PageSize * (@PageNumber - 1) ROWS
	FETCH NEXT @PageSize ROWS Only
	),
	--common table expression for number of rows
	CTE_TotalRows AS
	(
		SELECT Count(p.[Id]) as TotalRows 
		 FROM
	[Person] p 
	Inner Join 
	[Group] g
	On
	p.GroupId = g.Id
	WHERE
	(@Name is null or p.[Name]  like '%'+ @Name + '%')
	AND
	(@Group is null or g.[Name]  like '%'+ @Group + '%') 
	)

	SELECT t.TotalRows, r.[Name], r.[DateAdded],r.[GroupName], r.[GroupId], r.[Id] 
	 FROM
	CTE_Result r ,CTE_TotalRows t  
	  
    OPTION (RECOMPILE)  

END
GO
 
 ---Prepopulate 2 groups and 5 people into this application
 -- needs to be done via MVC App but is done here for testing purposes
 IF EXISTS
 (SELECT * FROM INFORMATION_SCHEMA.TABLES 
    WHERE 
	TABLE_SCHEMA = 'dbo' 
    AND  TABLE_NAME = 'Group'
	)
BEGIN
    INSERT INTO [Group]
	(Name)
	Values
	('Marvel'), 
	('DC')
END
GO

 IF EXISTS
 (SELECT * FROM INFORMATION_SCHEMA.TABLES 
    WHERE 
	TABLE_SCHEMA = 'dbo' 
    AND  TABLE_NAME = 'Person'
				 )
BEGIN
	    INSERT INTO [Person]
	(Name, GroupId, DateAdded)
	Values
	('Spiderman', 1, GETDATE()),
	('Thor',1,GETDATE()),
	('Wolverine',1,GETDATE()),
	('Batman', 2, GETDATE()),
	('Superman', 2, GETDATE())
END
GO



---END OF SCRIPTS



---EF Scaffolding command if needed
--Scaffold-DBContext "Data Source=.;Initial Catalog=PersonGroups;Integrated Security=True;ConnectRetryCount= 0" Microsoft.EntityFrameworkCore.SQLServer -OutputDir Models -Context PersonGroupsContext -DataAnnotations