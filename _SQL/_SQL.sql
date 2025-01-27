Use [master]
GO

-- ================================================================================================ --
-- CREATE SYSTEM LOGIN                                                                              --
-- ================================================================================================ --
IF  NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = N'RestaurantUser')
	CREATE LOGIN [RestaurantUser] WITH PASSWORD=N'WebAPIPassword', DEFAULT_DATABASE=[Test], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

-- ================================================================================================
-- 
-- ================================================================================================

IF NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'Restaurant' )
BEGIN
    CREATE DATABASE [Restaurant]   
END
Go

USE [Restaurant]
Go

-- ================================================================================================ 
-- CREATE db user                                                                                   
-- ================================================================================================ 
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'RestaurantUser')
	CREATE USER [RestaurantUser] FOR LOGIN [RestaurantUser] WITH DEFAULT_SCHEMA=[dbo]
GO

-- ================================================================================================
-- CREATE SCHEMAS 
-- ================================================================================================

-- ================================================================================================
-- CREATE SCHEMAS FOR USER MANAGMENT 
-- ================================================================================================
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'umUser')
BEGIN
	EXEC('CREATE SCHEMA [umUser]');
END
Go

-- ================================================================================================
-- CREATE SCHEMAS FOR EMPLOYEE MANAGMENT
-- ================================================================================================
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'staffing')
BEGIN
	EXEC('CREATE SCHEMA [staffing]');
END
Go

-- ============================================================================================================================================ 
-- Drops                                                                                  
-- ============================================================================================================================================ 
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[staffing].[EmployeeShiftsV]'))
	DROP VIEW [staffing].[EmployeeShiftsV]
GO

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[staffing].[EmployeeRolesV]'))
	DROP VIEW [staffing].[EmployeeRolesV]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[staffing].[EmployeeShifts]') AND type in (N'U'))
	DROP TABLE [staffing].[EmployeeShifts]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[staffing].[EmployeeWorkQuota]') AND type in (N'U'))
	DROP TABLE [staffing].[EmployeeWorkQuota]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[staffing].[EmployeeRoles]') AND type in (N'U'))
	DROP TABLE [staffing].[EmployeeRoles]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[staffing].[EmployeeWorkQuota]') AND type in (N'U'))
	DROP TABLE [staffing].[EmployeeWorkQuota]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[staffing].[Employees]') AND type in (N'U'))
	DROP TABLE [staffing].[Employees]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[staffing].[Roles]') AND type in (N'U'))
	DROP TABLE [staffing].[Roles]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[umUser].[Users]') AND type in (N'U'))
	DROP TABLE [umUser].[Users]
GO


-- ============================================================================================================================================ 
--                                                                                   
-- ============================================================================================================================================ 
CREATE TABLE [umUser].[Users](
	[ID]        [int] IDENTITY(1, 1) NOT NULL,
	[UserName]  [nvarchar](50)	   NOT NULL,
	[Password]  [varbinary](50)        NULL,
    [FullName]	[nvarchar](150)        NULL,
	[Mail]      [nvarchar](50)		   NULL,
	[Phone]     [nvarchar](50)         NULL,
	[D_Modify]  [datetime2]        NOT NULL,
	[Version] int				   NOT NULL,
	CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [umUser].[Users] ADD  CONSTRAINT [DF_Users_D_Modify]  DEFAULT (getdate()) FOR [D_Modify]
ALTER TABLE [umUser].[Users] ADD  CONSTRAINT [DF_Users_Version]   DEFAULT ((0)) FOR [Version]
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_UMUsers_Name] ON [umUser].[Users] 
(
	[UserName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

ALTER TABLE [umUser].[Users] ADD CONSTRAINT uc_UsersUserName UNIQUE (UserName)
Go


-- ============================================================================================================================================ 
--                                                                                   
-- ============================================================================================================================================ 
CREATE TABLE [staffing].[Employees](
	[ID]               [int] IDENTITY(1, 1)  NOT NULL,
	[FullName]         [nvarchar](255)	     NOT NULL,
	[Email]            [nvarchar](255)	     NOT NULL,
	[Phone]            [nvarchar](20)	     NOT NULL,
	[UserID]		   [int]				 NOT NULL,
	[DModify]          [datetime2]           NOT NULL,
	[Version]          [int]				 NOT NULL,
	CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [staffing].[Employees] ADD  CONSTRAINT [DF_Employees_DModify]  DEFAULT (getdate()) FOR [DModify]
ALTER TABLE [staffing].[Employees] ADD  CONSTRAINT [DF_Employees_Version]  DEFAULT ((0)) FOR [Version]
GO


ALTER TABLE [staffing].[Employees] WITH CHECK ADD CONSTRAINT [FK_Employees_User_UserID] FOREIGN KEY([UserID]) 
	REFERENCES [umUser].[Users] ([ID])
GO
ALTER TABLE [staffing].[Employees] CHECK CONSTRAINT [FK_Employees_User_UserID]
GO

-- ============================================================================================================================================ 
--                                                                                   
-- ============================================================================================================================================ 
CREATE TABLE [staffing].[EmployeeWorkQuota](
	[ID]               [int] IDENTITY(1, 1)  NOT NULL,
	[EmployeeID]	   [int]				 NOT NULL,
	[MandatoryHours]   [decimal](5, 2)       NOT NULL,
	[PeriodType]       [int]                 NOT NULL,
	[UserID]		   [int]				 NOT NULL,
	[DModify]          [datetime2]           NOT NULL,
	[Version]          [int]				 NOT NULL,
	CONSTRAINT [PK_EmployeeWorkQuota] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [staffing].[EmployeeWorkQuota] ADD  CONSTRAINT [DF_EmployeeWorkQuota_DModify]  DEFAULT (getdate()) FOR [DModify]
ALTER TABLE [staffing].[EmployeeWorkQuota] ADD  CONSTRAINT [DF_EmployeeWorkQuota_Version]  DEFAULT ((0)) FOR [Version]
GO

ALTER TABLE [staffing].[EmployeeWorkQuota]  WITH CHECK ADD CONSTRAINT [FK_EmployeeWorkQuota_UserID] FOREIGN KEY([UserID]) 
	REFERENCES [umUser].[Users] ([ID])
GO
ALTER TABLE [staffing].[EmployeeWorkQuota] CHECK CONSTRAINT [FK_EmployeeWorkQuota_UserID]
GO

ALTER TABLE [staffing].[EmployeeWorkQuota]  WITH CHECK ADD CONSTRAINT [FK_EmployeeWorkQuota_EmployeeID] FOREIGN KEY([EmployeeID]) 
	REFERENCES [staffing].[Employees] ([ID])
GO
ALTER TABLE [staffing].[EmployeeWorkQuota] CHECK CONSTRAINT [FK_EmployeeWorkQuota_EmployeeID]
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Staffing_Description', 
	@value = N'This field is an ENUM in C# code representing the period type (Daily = 1, Weekly = 2, Monthly = 3)', 
    @level0type = N'SCHEMA', @level0name = 'staffing', 
    @level1type = N'TABLE', @level1name = 'EmployeeWorkQuota', 
    @level2type = N'COLUMN', @level2name = 'PeriodType';
Go

-- ============================================================================================================================================ 
--                                                                                   
-- ============================================================================================================================================ 
CREATE TABLE [staffing].[Roles](
	[ID]                      [int] IDENTITY(1, 1)  NOT NULL,
	[IsPrimary]               [bit]                 NOT NULL, 
	[SeatsAvailable]          [int]                 NOT NULL,  
	[IsAvailable]             [bit]                 NOT NULL,
	[Name]                    [nvarchar](50)	    NOT NULL,
	[Description]             [nvarchar](max)	        NULL,
	[UserID]		          [int]				    NOT NULL,
	[DModify]                 [datetime2]           NOT NULL,
	[Version]                 [int]				    NOT NULL,
	CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [staffing].[Roles] ADD  CONSTRAINT [DF_Roles_DModify]  DEFAULT (getdate()) FOR [DModify]
ALTER TABLE [staffing].[Roles] ADD  CONSTRAINT [DF_Roles_Version]  DEFAULT ((0)) FOR [Version]
GO

ALTER TABLE [staffing].[Roles]  WITH CHECK ADD CONSTRAINT [FK_Roles_UserID] FOREIGN KEY([UserID]) 
	REFERENCES [umUser].[Users] ([ID])
GO
ALTER TABLE [staffing].[Roles] CHECK CONSTRAINT [FK_Roles_UserID]
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_Roles_Name] ON [staffing].[Roles] 
(
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

ALTER TABLE [staffing].[Roles] ADD CONSTRAINT uc_RolesName UNIQUE ([Name])
Go

EXEC sp_addextendedproperty 
    @name = N'MS_Staffing_Description', 
	@value = N'This field represents the role type: 
				IsPrimary = true: This means that if the employee is assigned to this position, they cannot work in another role.
				IsPrimary = false: This means that the employee can work in another role, as long as no role with the IsPrimary flag set to true is assigned to them.', 
    @level0type = N'SCHEMA', @level0name = 'staffing', 
    @level1type = N'TABLE', @level1name = 'Roles', 
    @level2type = N'COLUMN', @level2name = 'IsPrimary';
Go

EXEC sp_addextendedproperty 
    @name = N'MS_Staffing_Description', 
	@value = N'This field represents the availability for this role.
				IsAvailable = true: This means that there are available jobs for this position.
				IsAvailable = false: This means that there are no available jobs for this position.', 
    @level0type = N'SCHEMA', @level0name = 'staffing', 
    @level1type = N'TABLE', @level1name = 'Roles', 
    @level2type = N'COLUMN', @level2name = 'IsAvailable';
Go

-- ============================================================================================================================================ 
--                                                                                   
-- ============================================================================================================================================ 
CREATE TABLE [staffing].[EmployeeRoles](
	[ID]                [int] IDENTITY(1, 1) NOT NULL,
	[EmployeeID]	    [int]				 NOT NULL,
	[RoleID]            [int]				 NOT NULL,
	[RoleStartDate]     [datetime2]          NOT NULL,
	[Description]       [nvarchar](max)	         NULL,
	[UserID]		    [int]				 NOT NULL,
	[DModify]           [datetime2]          NOT NULL,
	[Version]           [int]				 NOT NULL,
	CONSTRAINT [PK_EmployeeRoles] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [staffing].[EmployeeRoles] ADD  CONSTRAINT [DF_EmployeeRoles_DModify]  DEFAULT (getdate()) FOR [DModify]
ALTER TABLE [staffing].[EmployeeRoles] ADD  CONSTRAINT [DF_EmployeeRoles_Version]  DEFAULT ((0)) FOR [Version]
GO

ALTER TABLE [staffing].[EmployeeRoles] WITH CHECK ADD CONSTRAINT [FK_EmployeeRoles_Employees_EmployeeID] FOREIGN KEY([EmployeeID]) 
	REFERENCES [staffing].[Employees] ([ID])
GO
ALTER TABLE [staffing].[EmployeeRoles] CHECK CONSTRAINT [FK_EmployeeRoles_Employees_EmployeeID]
GO

ALTER TABLE [staffing].[EmployeeRoles] WITH CHECK ADD CONSTRAINT [FK_EmployeeRoles_Role_RoleID] FOREIGN KEY([RoleID]) 
	REFERENCES [staffing].[Roles] ([ID])
GO
ALTER TABLE [staffing].[EmployeeRoles] CHECK CONSTRAINT [FK_EmployeeRoles_Role_RoleID]
GO


ALTER TABLE [staffing].[EmployeeRoles] WITH CHECK ADD CONSTRAINT [FK_EmployeeRoles_User_UserID] FOREIGN KEY([UserID]) 
	REFERENCES [umUser].[Users] ([ID])
GO
ALTER TABLE [staffing].[EmployeeRoles] CHECK CONSTRAINT [FK_EmployeeRoles_User_UserID]
GO

-- ============================================================================================================================================ 
--                                                                                   
-- ============================================================================================================================================ 
CREATE TABLE [staffing].[EmployeeShifts](
	[ID]                [int] IDENTITY(1, 1) NOT NULL,
	[EmployeeRoleID]	[int]				 NOT NULL,
	[ShiftDate]         [date]                   NULL,
	[StartTime]         [time]                   NULL,
	[EndTime]           [time]                   NULL,
	[Description]       [nvarchar](max)	         NULL,
	[UserID]		    [int]				 NOT NULL,
	[DModify]           [datetime2]          NOT NULL,
	[Version]           [int]				 NOT NULL,
	CONSTRAINT [PK_EmployeeShifts] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [staffing].[EmployeeShifts] ADD  CONSTRAINT [DF_EmployeeShifts_DModify]  DEFAULT (getdate()) FOR [DModify]
ALTER TABLE [staffing].[EmployeeShifts] ADD  CONSTRAINT [DF_EmployeeShifts_Version]  DEFAULT ((0)) FOR [Version]
GO

ALTER TABLE [staffing].[EmployeeShifts] WITH CHECK ADD CONSTRAINT [FK_EmployeeShifts_EmployeeRoles_EmployeeRoleID] FOREIGN KEY([EmployeeRoleID]) 
	REFERENCES [staffing].[EmployeeRoles] ([ID])
GO
ALTER TABLE [staffing].[EmployeeShifts] CHECK CONSTRAINT [FK_EmployeeShifts_EmployeeRoles_EmployeeRoleID]
GO

CREATE NONCLUSTERED INDEX [IX_EmployeeShifts_EmployeeRoleID] ON [staffing].[EmployeeShifts] ([EmployeeRoleID]);
CREATE NONCLUSTERED INDEX [IX_EmployeeShifts_ShiftDate] ON [staffing].[EmployeeShifts] ([ShiftDate]);
CREATE NONCLUSTERED INDEX [IX_EmployeeShifts_StartTime] ON [staffing].[EmployeeShifts] ([StartTime]);
Go

-- ============================================================================================================================================ 
--                 VIEWS                                                                  
-- ============================================================================================================================================ 
--================================================================================
--[EmployeeRolesV]
--================================================================================
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[staffing].[EmployeeRolesV]'))
	DROP VIEW [staffing].[EmployeeRolesV]
GO

CREATE VIEW [staffing].[EmployeeRolesV]
AS
(
	select 
			emplr.ID as EmployeeRoleID
		   ,empl.ID as EmployeeID 
		   ,empl.FullName 
		   ,rol.ID as RoleID 
		   ,rol.[Name] as RoleName
		   ,rol.IsAvailable as RoleIsAvailable
		   ,rol.IsPrimary as RoleIsPrimary
		   ,rol.[Description] as RoleDescr
	from [staffing].[EmployeeRoles] emplr 
		join [staffing].[Employees] empl on empl.ID = emplr.EmployeeID
		join [staffing].[Roles] rol on rol.ID = emplr.RoleID
)
GO


--================================================================================
--[EmployeeShiftsV]
--================================================================================
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[staffing].[EmployeeShiftsV]'))
	DROP VIEW [staffing].[EmployeeShiftsV]
GO

CREATE VIEW [staffing].[EmployeeShiftsV]
AS
(
	SELECT 
		    emplSh.ID as EmpoyeeShiftID
		   ,emplr.ID as EmployeeRoleID 
		   ,empl.ID as EmployeeID 
		   ,empl.FullName 
		   ,rol.ID as RoleID 
		   ,rol.[Name] as RoleName 
		   ,rol.IsAvailable
		   ,rol.IsPrimary as IsPrimaryRole
		   ,emplSh.ShiftDate
		   ,emplSh.StartTime
		   ,emplSh.EndTime
		   ,emplSh.[Description] as EmployeeShiftDescr
	FROM [staffing].[Employees] empl
		LEFT JOIN [staffing].[EmployeeRoles] emplR ON empl.ID = emplR.EmployeeID
		LEFT JOIN [staffing].[Roles] rol ON emplR.RoleID = rol.ID
		LEFT JOIN [staffing].[EmployeeShifts] emplSh ON emplR.ID = emplSh.EmployeeRoleID
)
GO


---------------------------------------------- GRANT PERMISSIONS -----------------------------------------------------------------------
GRANT select ON [umUser].[Users] TO [RestaurantUser];  
GO
GRANT select ON [staffing].[EmployeeShifts] TO [RestaurantUser];  
GO  
GRANT insert ON [staffing].[EmployeeShifts] TO [RestaurantUser];  
GO  
GRANT update ON [staffing].[EmployeeShifts] TO [RestaurantUser];  
GO  
GRANT delete ON [staffing].[EmployeeShifts] TO [RestaurantUser];  
GO 
GRANT select ON [staffing].[EmployeeShiftsV] TO [RestaurantUser];  
GO
GRANT select ON [staffing].[EmployeeWorkQuota] TO [RestaurantUser];  
GO
GRANT select ON [staffing].[EmployeeRoles] TO [RestaurantUser];  
GO 
GRANT insert ON [staffing].[EmployeeRoles] TO [RestaurantUser];  
GO  
GRANT update ON [staffing].[EmployeeRoles] TO [RestaurantUser];  
GO  
GRANT select ON [staffing].[Roles] TO [RestaurantUser];  
GO  
GRANT update ON [staffing].[Roles] TO [RestaurantUser];  
GO  
GRANT select ON [staffing].[Employees] TO [RestaurantUser];  
GO  


---- Creation of DUMMY users that are going to create, delete his  wallets

USE [Restaurant]
GO

INSERT INTO [umUser].[Users] ([UserName],[Password],[FullName],[Mail], [Phone])
     VALUES ('Admin', 0x4D7956617262696E61727944617461, 'Elena Margina', 'e.margina@gmail.com', '0883360301')
Go

Declare @UserID int 
select @UserID = ID from [umUser].[Users] where [UserName] = 'Admin'

-----------------------------------------------------------------------------------------------		 
--                                     Roles
-----------------------------------------------------------------------------------------------
INSERT INTO [staffing].[Roles] ([IsPrimary],[SeatsAvailable],[IsAvailable],[Name],[Description],[UserID])
         values(0, 15, 1, 'Waiter', 'Some description about this role', @UserID)

INSERT INTO [staffing].[Roles] ([IsPrimary],[SeatsAvailable],[IsAvailable],[Name],[Description],[UserID])
         values(0, 10, 1, 'Barman', 'Some description about this role', @UserID)

INSERT INTO [staffing].[Roles] ([IsPrimary],[SeatsAvailable],[IsAvailable],[Name],[Description],[UserID])
         values(1, 1, 2, 'Chef', 'Some description about this role', @UserID)

INSERT INTO [staffing].[Roles] ([IsPrimary],[SeatsAvailable],[IsAvailable],[Name],[Description],[UserID])
         values(1, 10, 1, 'Manager', 'Some description about this role', @UserID)

INSERT INTO [staffing].[Roles] ([IsPrimary],[SeatsAvailable],[IsAvailable],[Name],[Description],[UserID])
         values(0, 10, 1, 'Cleaner', 'Some description about this role', @UserID)

-----------------------------------------------------------------------------------------------		 
--                                     Empolyees
-----------------------------------------------------------------------------------------------
INSERT INTO [staffing].[Employees]([FullName],[Email],[Phone],[UserID])
     VALUES('Gergana Petrova', 'g.petrova@gmail.com', '0889776655', @UserID)

INSERT INTO [staffing].[Employees]([FullName],[Email],[Phone],[UserID])
     VALUES('Georgy Petrov', 'g.petrov@gmail.com', '0889776666', @UserID)

INSERT INTO [staffing].[Employees]([FullName],[Email],[Phone],[UserID])
     VALUES('Daniela Dimova', 'd.dimova@gmail.com', '0899778877', @UserID)

INSERT INTO [staffing].[Employees]([FullName],[Email],[Phone],[UserID])
     VALUES('Marina Mirkova', 'm.mirkova@gmail.com', '0887667744', @UserID)

INSERT INTO [staffing].[Employees]([FullName],[Email],[Phone],[UserID])
     VALUES('July Dancheva', 'j.dancheva@gmail.com', '0887996688', @UserID)

INSERT INTO [staffing].[Employees]([FullName],[Email],[Phone],[UserID])
     VALUES('Kristian Hristov', 'k.hristov@gmail.com', '0887991122', @UserID)

INSERT INTO [staffing].[Employees]([FullName],[Email],[Phone],[UserID])
     VALUES('Bernard Bingov', 'b.bingov@gmail.com', '0888888888', @UserID)

INSERT INTO [staffing].[Employees]([FullName],[Email],[Phone],[UserID])
     VALUES('Arnold Arminov', 'a.arminov@gmail.com', '0889778877', @UserID)

INSERT INTO [staffing].[Employees]([FullName],[Email],[Phone],[UserID])
     VALUES('Tina Arturova', 't.arturova@gmail.com', '0887334433', @UserID)

-----------------------------------------------------------------------------------------------		 
--                         EmployeeWorkQuota and EmployeeRoles
-----------------------------------------------------------------------------------------------
declare @EmployeeID int, @RoleID int 

select @EmployeeID = ID from [staffing].[Employees] where FullName = 'Gergana Petrova'
select @RoleID = ID from [staffing].[Roles] where [Name] = 'Chef'
	INSERT INTO [staffing].[EmployeeWorkQuota] ([EmployeeID],[MandatoryHours],[PeriodType],[UserID])
		 VALUES(@EmployeeID, 120, 3, @UserID)
	INSERT INTO [staffing].[EmployeeRoles]([EmployeeID],[RoleID],[RoleStartDate],[Description],[UserID])
		 VALUES(@EmployeeID, @RoleID, getdate()-3, 'Some description', @UserID)

select @EmployeeID = ID from [staffing].[Employees] where FullName = 'Georgy Petrov'
select @RoleID = ID from [staffing].[Roles] where [Name] = 'Manager'
	INSERT INTO [staffing].[EmployeeWorkQuota] ([EmployeeID],[MandatoryHours],[PeriodType],[UserID])
		 VALUES(@EmployeeID, 166, 3, @UserID)
	INSERT INTO [staffing].[EmployeeRoles]([EmployeeID],[RoleID],[RoleStartDate],[Description],[UserID])
		 VALUES(@EmployeeID, @RoleID, getdate()-3, 'Some description', @UserID)


select @EmployeeID = ID from [staffing].[Employees] where FullName = 'Daniela Dimova'
select @RoleID = ID from [staffing].[Roles] where [Name] = 'Chef'
	INSERT INTO [staffing].[EmployeeWorkQuota] ([EmployeeID],[MandatoryHours],[PeriodType],[UserID])
		 VALUES(@EmployeeID, 130, 3, @UserID)
	INSERT INTO [staffing].[EmployeeRoles]([EmployeeID],[RoleID],[RoleStartDate],[Description],[UserID])
		 VALUES(@EmployeeID, @RoleID, getdate()-3, 'Some description', @UserID)

select @EmployeeID = ID from [staffing].[Employees] where FullName = 'Marina Mirkova'
select @RoleID = ID from [staffing].[Roles] where [Name] = 'Waiter'
	INSERT INTO [staffing].[EmployeeWorkQuota] ([EmployeeID],[MandatoryHours],[PeriodType],[UserID])
		 VALUES(@EmployeeID, 160, 3, @UserID)
	INSERT INTO [staffing].[EmployeeRoles]([EmployeeID],[RoleID],[RoleStartDate],[Description],[UserID])
		 VALUES(@EmployeeID, @RoleID, getdate()-3, 'Some description', @UserID)
select @RoleID = ID from [staffing].[Roles] where [Name] = 'Barman'
	INSERT INTO [staffing].[EmployeeRoles]([EmployeeID],[RoleID],[RoleStartDate],[Description],[UserID])
		 VALUES(@EmployeeID, @RoleID, getdate()-3, 'Some description', @UserID)

select @EmployeeID = ID from [staffing].[Employees] where FullName = 'July Dancheva'
select @RoleID = ID from [staffing].[Roles] where [Name] = 'Waiter'
	INSERT INTO [staffing].[EmployeeWorkQuota] ([EmployeeID],[MandatoryHours],[PeriodType],[UserID])
		 VALUES(@EmployeeID, 50, 3, @UserID)
	INSERT INTO [staffing].[EmployeeRoles]([EmployeeID],[RoleID],[RoleStartDate],[Description],[UserID])
		 VALUES(@EmployeeID, @RoleID, getdate()-3, 'Some description', @UserID)
select @RoleID = ID from [staffing].[Roles] where [Name] = 'Barman'
	INSERT INTO [staffing].[EmployeeRoles]([EmployeeID],[RoleID],[RoleStartDate],[Description],[UserID])
		 VALUES(@EmployeeID, @RoleID, getdate()-3, 'Some description', @UserID)
select @RoleID = ID from [staffing].[Roles] where [Name] = 'Cleaner'
	INSERT INTO [staffing].[EmployeeRoles]([EmployeeID],[RoleID],[RoleStartDate],[Description],[UserID])
		 VALUES(@EmployeeID, @RoleID, getdate()-3, 'Some description', @UserID)

select @EmployeeID = ID from [staffing].[Employees] where FullName = 'Kristian Hristov'
select @RoleID = ID from [staffing].[Roles] where [Name] = 'Barman'
	INSERT INTO [staffing].[EmployeeWorkQuota] ([EmployeeID],[MandatoryHours],[PeriodType],[UserID])
		 VALUES(@EmployeeID, 160, 3, @UserID)
	INSERT INTO [staffing].[EmployeeRoles]([EmployeeID],[RoleID],[RoleStartDate],[Description],[UserID])
		 VALUES(@EmployeeID, @RoleID, getdate()-3, 'Some description', @UserID)

select @EmployeeID = ID from [staffing].[Employees] where FullName = 'Bernard Bingov'
	INSERT INTO [staffing].[EmployeeWorkQuota] ([EmployeeID],[MandatoryHours],[PeriodType],[UserID])
		 VALUES(@EmployeeID, 160, 3, @UserID)
select @RoleID = ID from [staffing].[Roles] where [Name] = 'Barman'
	INSERT INTO [staffing].[EmployeeRoles]([EmployeeID],[RoleID],[RoleStartDate],[Description],[UserID])
		 VALUES(@EmployeeID, @RoleID, getdate()-3, 'Some description', @UserID)

GO


select * from [umUser].[Users]
select * from [staffing].[EmployeeWorkQuota]
select * from [staffing].[Employees]
select * from [staffing].[Roles] 
select * from [staffing].[EmployeeRoles]


---------------------------------------------------
-- To View Extended Properties (Comments)
---------------------------------------------------
SELECT 
    objname = obj.name, 
    colname = col.name, 
    ep.name AS property_name, 
    ep.value AS property_value
FROM 
    sys.extended_properties ep
INNER JOIN 
    sys.tables obj ON ep.major_id = obj.object_id
LEFT JOIN 
    sys.columns col ON ep.major_id = col.object_id AND ep.minor_id = col.column_id
WHERE 
    ep.name = 'MS_Staffing_Description';
Go