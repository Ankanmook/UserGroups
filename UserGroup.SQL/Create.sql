--start from default
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
	[Name] varchar(128), -- need to write some validation tests
	CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED ([Id] Asc)
)
GO

--Name of group should be unique - why common sense 


--Index creation on Group Name
CREATE NONCLUSTERED INDEX IX_Group_Name
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
	[DateAdded] DateTime not null,
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

CREATE OR ALTER Proc Search 
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
	p.[Name], p.[DateAdded], g.[Name] as GroupName, p.[Id]
	 FROM
	[Person] p 
	Inner Join 
	[Group] g
	On
	p.GroupId = g.Id
	WHERE
	(p.[Name] is null or p.[Name] like '%'+ @Name + '%') -- this will yeild all results
	AND
	(g.[Name] is null or g.[Name] like '%'+ @Group + '%') -- this will yeild all results
	
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
	(p.[Name]  is null or p.[Name]  like '%'+ @Name + '%')
	AND
	(g.[Name]  is null or g.[Name]  like '%'+ @Group + '%') 
	)

	SELECT TotalRows, p.[Name], p.[DateAdded], g.[Name] as GroupName, p.[Id] 
	 FROM
	[Person] p 
	Inner Join 
	[Group] g
	On
	p.GroupId = g.Id
	, CTE_TotalRows   
    WHERE EXISTS (SELECT 1 FROM CTE_Result WHERE CTE_Result.Id = p.Id)  
	  
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



