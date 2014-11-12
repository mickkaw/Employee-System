USE [C:\PROJECTS\HEADSPRING.EMPLOYEE\HEADSPRING.EMPLOYEE\APP_DATA\HSEMPLOYEEDB.MDF]
GO
-----------------------------------------------------------------------------------		

IF OBJECT_ID ('[dbo].[Jobs]','u') IS NOT NULL 
	DROP TABLE [dbo].[Jobs]

GO

CREATE TABLE [dbo].[Jobs](
	[JobID] [int] IDENTITY(1,1) NOT NULL,
	[JobCode] [varchar](10) NOT NULL,
	[JobDescr] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[JobID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

INSERT
  INTO	[dbo].[Jobs]
		(JobCode, JobDescr)
VALUES	('CHAIR','Chairman'),
		('PREZ','President'),
		('CIO','Chief Information Officer'),
		('PROG','Programmer'),
		('ENG','Engineer'),
		('MAN','Manager'),
		('TLEAD','Team Lead'),
		('DOC','Documentor'),
		('DBA','Database Administrator'),
		('SYSADM','System Administrator'),
		('ADMIN','Administrator')

GO
-----------------------------------------------------------------------------------		

IF OBJECT_ID ('[dbo].[Locations]','u') IS NOT NULL 
	DROP TABLE [dbo].[Locations]

GO

CREATE TABLE [dbo].[Locations](
	[LocationID] [int] IDENTITY(1,1) NOT NULL,
	[LocationDescr] [varchar](50) NOT NULL,
	[City] [varchar](30) NULL,
	[State] [varchar](2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[LocationID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

INSERT
  INTO	[dbo].[Locations]
		(LocationDescr,	City, State)
VALUES	('Morado Circle','Austin','TX'),
		('Woodland Park Plaza','Houston','TX'),
		('Quorum Drive','Dallas','TX')

GO
-----------------------------------------------------------------------------------		

IF OBJECT_ID ('[dbo].[PhoneTypes]','u') IS NOT NULL 
	DROP TABLE [dbo].[PhoneTypes]

GO

CREATE TABLE [dbo].[PhoneTypes](
	[PhoneTypeID] [int] IDENTITY(1,1) NOT NULL,
	[TypeDescription] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[PhoneTypeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

INSERT
  INTO	PhoneTypes
		(
			TypeDescription
		)
VALUES	('Work'),
		('Cell'),
		('Home')
		
GO
-----------------------------------------------------------------------------------

IF OBJECT_ID ('[dbo].[Roles]','u') IS NOT NULL 
	DROP TABLE [dbo].[Roles]

GO

CREATE TABLE [dbo].[Roles](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](8) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

INSERT
  INTO	[dbo].[Roles]
		(RoleName)
VALUES	('Employee'),
		('Manager'),
		('Admin')
GO
-----------------------------------------------------------------------------------

IF OBJECT_ID ('[dbo].[Employees]','u') IS NOT NULL 
	DROP TABLE [dbo].[Employees]
	
GO
	
CREATE TABLE [dbo].[Employees](
	[EmployeeID] [int] IDENTITY(1,1) NOT NULL,
	[FName] [varchar](20) NULL,
	[MI] [varchar](1) NULL,
	[LName] [varchar](30) NOT NULL,
	[eMail] [varchar](200) NULL,
	[JobID] [int] NULL,
	[LocationID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

INSERT
  INTO	[dbo].[Employees]
		(FName, MI, LNAME, eMail, JobID, LocationID)
VALUES	('Dustin','','Wells','dwells@headspring.com', 1, 1),
		('JT','','McCormick','', 2, 1),
		('Glen','','Burnside','', 3, 1),
		('Dave','','Valentino','dvalentino@headspring.com', 5, 1),
		('Deran','','Schilling','', 5, 2),
		('Cedric','','Yao','cyao2@headspring.com', 5, 3),
		('Bob','','Smith','', null, 2),
		('George','','Jones','', null, 1),
		('Kevin','','Tomlin','', null, null),
		('Steven','','Tyler','styler@gmail.com', 6, null),
		('Michael','J','Kawejsza','mkawejsza@gmail.com', 4, 1),
		('Ben', '', 'Heebner', '', 5, 1)

GO
-----------------------------------------------------------------------------------

IF OBJECT_ID ('[dbo].[Phones]','u') IS NOT NULL 
	DROP TABLE [dbo].[Phones]
	
GO

CREATE TABLE [dbo].[Phones](
	[EmployeeID] [int] NOT NULL,
	[PhoneNumber] [varchar](20) NOT NULL,
	[IsPrimary] [bit] NOT NULL,
	[PhoneTypeID] [int] NULL,
	[Extension] [varchar](5) NULL,
PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC,
	[PhoneNumber] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Phones] ADD  DEFAULT ((0)) FOR [IsPrimary]
GO

INSERT
  INTO	[dbo].[Phones]
		(EmployeeID, PhoneNumber, IsPrimary, PhoneTypeID, Extension)
VALUES	(1, '5555555555', 1, 1, '3843'),
		(1, '3214893294', 0, 2, ''),
		(2, '5559383392', 0, 1, '4930'),
		(2, '6853432832', 1, 2, ''),
		(2, '5392336843', 0, 3, ''),
		(6, '3772093911', 1, 1, '4930'),
		(6, '3773224900', 0, 2, '')

GO
-----------------------------------------------------------------------------------

IF OBJECT_ID ('[dbo].[Users]','u') IS NOT NULL 
	DROP TABLE [dbo].[Users]
	
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[EmpID] [int] NULL,
	[RoleID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

INSERT
  INTO	[dbo].[Users]
		(UserName, EmpID, RoleID)
VALUES	('HSPRING\dwells', 1, 1),
		('HSPRING\jtmccormick', 2, 1),
		('HSPRING\gburnside', 3, 1),
		('HSPRING\dvalentino', 4, 3),
		('HSPRING\dschilling', 5, 3),
		('HSPRING\cyao', 6, 1),
		('HSPRING\bsmith', 7, 1),
		('HSPRING\gjones', 8, 3),
		('HSPRING\ktomlin', 9, 3),
		('HSPRING\styler', 10, 1),
		('HSPRING\mkawejsza', 11, 1),
		('HSPRING\bheebner', 12, 3)

GO
-----------------------------------------------------------------------------------
-----------------------------------------------------------------------------------
-----------------------------------------------------------------------------------
		
IF OBJECT_ID ('[dbo].[vw_Users]') IS NOT NULL 
	DROP VIEW [dbo].[vw_Users]

GO

CREATE VIEW [dbo].[vw_Users]
AS 
SELECT	U.UserID
		, U.UserName
		, U.EmpID
		, U.RoleID
		, R.RoleName
  FROM	Users AS U
		LEFT JOIN Roles AS R
			ON	U.RoleID = R.RoleID;

GO
-----------------------------------------------------------------------------------

IF OBJECT_ID ('[dbo].[vw_Employees]') IS NOT NULL 
	DROP VIEW [dbo].[vw_Employees]

GO

CREATE VIEW [dbo].[vw_Employees]
AS 
SELECT	EMP.EmployeeID
		, EMP.FName
		, EMP.LName
		, EMP.MI
		, EMP.eMail
		, EMP.JobID
		, J.JobCode
		, J.JobDescr
		, EMP.LocationID
		, LOC.LocationDescr
		, LOC.City
		, LOC.State
  FROM	Employees AS EMP
		LEFT JOIN Jobs AS J
			ON EMP.JobID = J.JobID
		LEFT JOIN Locations AS LOC
			ON EMP.LocationID = LOC.LocationID
		LEFT JOIN Phones AS PH
			ON EMP.EmployeeID = PH.EmployeeID
			
GO

-----------------------------------------------------------------------------------

select * from Jobs
select * from Locations
select * from PhoneTypes
select * from Roles

select * from Users
select * from Employees
select * from Phones


select * from vw_Users
select * from vw_Employees