

USE [Restaurant]
GO

SELECT [ID]
      ,[EmployeeRoleID]
      ,[ShiftDate]
      ,[StartTime]
      ,[EndTime]
      ,[Description]
      ,[UserID]
      ,[DModify]
      ,[Version]
  FROM [staffing].[EmployeeShifts]

GO

USE [Restaurant]
GO


--[EmployeeRoleID] ,[ShiftDate]  ,[StartTime] ,[EndTime] -> to create indexes checks
			---------

			-- Shift for 20.01 - 26.01

---Shift for 20.01
-------- Marina Mirkova
INSERT INTO [staffing].[EmployeeShifts]  ([EmployeeRoleID] ,[ShiftDate]  ,[StartTime] ,[EndTime] ,[Description] ,[UserID])
     VALUES  (4, '2025-01-20', '09:15:00', '12:30:00', 'Monday shift', 1)

INSERT INTO [staffing].[EmployeeShifts]  ([EmployeeRoleID] ,[ShiftDate]  ,[StartTime] ,[EndTime] ,[Description] ,[UserID])
     VALUES  (5, '2025-01-20', '14:10:00', '16:00:00', 'Monday shift', 1)

INSERT INTO [staffing].[EmployeeShifts]  ([EmployeeRoleID] ,[ShiftDate]  ,[StartTime] ,[EndTime] ,[Description] ,[UserID])
     VALUES  (4, '2025-01-20', '19:00:00', '21:00:00', 'Monday shift', 1)

	 ----------July Dancheva
INSERT INTO [staffing].[EmployeeShifts]  ([EmployeeRoleID] ,[ShiftDate]  ,[StartTime] ,[EndTime] ,[Description] ,[UserID])
     VALUES  (8, '2025-01-20', '07:00:00', '09:00:00', 'Monday shift', 1)

INSERT INTO [staffing].[EmployeeShifts]  ([EmployeeRoleID] ,[ShiftDate]  ,[StartTime] ,[EndTime] ,[Description] ,[UserID])
     VALUES  (7, '2025-01-20', '09:30:00', '13:00:00', 'Monday shift', 1)

INSERT INTO [staffing].[EmployeeShifts]  ([EmployeeRoleID] ,[ShiftDate]  ,[StartTime] ,[EndTime] ,[Description] ,[UserID])
     VALUES  (6, '2025-01-20', '15:00:00', '17:00:00', 'Monday shift', 1)


-- Shift for 21.01
-------- Marina Mirkova
INSERT INTO [staffing].[EmployeeShifts]  ([EmployeeRoleID] ,[ShiftDate]  ,[StartTime] ,[EndTime] ,[Description] ,[UserID])
     VALUES  (4, '2025-01-21', '10:15:00', '13:30:00', 'Monday shift', 1)

INSERT INTO [staffing].[EmployeeShifts]  ([EmployeeRoleID] ,[ShiftDate]  ,[StartTime] ,[EndTime] ,[Description] ,[UserID])
     VALUES  (5, '2025-01-21', '14:10:00', '16:00:00', 'Monday shift', 1)

INSERT INTO [staffing].[EmployeeShifts]  ([EmployeeRoleID] ,[ShiftDate]  ,[StartTime] ,[EndTime] ,[Description] ,[UserID])
     VALUES  (4, '2025-01-21', '18:30:00', '20:00:00', 'Monday shift', 1)

	 ----------July Dancheva
INSERT INTO [staffing].[EmployeeShifts]  ([EmployeeRoleID] ,[ShiftDate]  ,[StartTime] ,[EndTime] ,[Description] ,[UserID])
     VALUES  (8, '2025-01-21', '07:30:00', '09:00:00', 'Monday shift', 1)

INSERT INTO [staffing].[EmployeeShifts]  ([EmployeeRoleID] ,[ShiftDate]  ,[StartTime] ,[EndTime] ,[Description] ,[UserID])
     VALUES  (7, '2025-01-21', '10:30:00', '13:10:00', 'Monday shift', 1)

INSERT INTO [staffing].[EmployeeShifts]  ([EmployeeRoleID] ,[ShiftDate]  ,[StartTime] ,[EndTime] ,[Description] ,[UserID])
     VALUES  (6, '2025-01-21', '15:30:00', '17:00:00', 'Monday shift', 1)

GO



			-- Shift for 27.01 - 02.02

---Shift for 28.01
-------- Marina Mirkova
INSERT INTO [staffing].[EmployeeShifts]  ([EmployeeRoleID] ,[ShiftDate]  ,[StartTime] ,[EndTime] ,[Description] ,[UserID])
     VALUES  (4, '2025-01-27', '09:15:00', '12:30:00', 'Monday shift', 1)

INSERT INTO [staffing].[EmployeeShifts]  ([EmployeeRoleID] ,[ShiftDate]  ,[StartTime] ,[EndTime] ,[Description] ,[UserID])
     VALUES  (5, '2025-01-27', '14:10:00', '16:00:00', 'Monday shift', 1)

INSERT INTO [staffing].[EmployeeShifts]  ([EmployeeRoleID] ,[ShiftDate]  ,[StartTime] ,[EndTime] ,[Description] ,[UserID])
     VALUES  (4, '2025-01-27', '19:00:00', '21:00:00', 'Monday shift', 1)

	 ----------July Dancheva
INSERT INTO [staffing].[EmployeeShifts]  ([EmployeeRoleID] ,[ShiftDate]  ,[StartTime] ,[EndTime] ,[Description] ,[UserID])
     VALUES  (8, '2025-01-27', '07:00:00', '09:00:00', 'Monday shift', 1)

INSERT INTO [staffing].[EmployeeShifts]  ([EmployeeRoleID] ,[ShiftDate]  ,[StartTime] ,[EndTime] ,[Description] ,[UserID])
     VALUES  (7, '2025-01-27', '09:30:00', '13:00:00', 'Monday shift', 1)

INSERT INTO [staffing].[EmployeeShifts]  ([EmployeeRoleID] ,[ShiftDate]  ,[StartTime] ,[EndTime] ,[Description] ,[UserID])
     VALUES  (6, '2025-01-27', '15:00:00', '17:00:00', 'Monday shift', 1)


-- Shift for 28.01
-------- Marina Mirkova
INSERT INTO [staffing].[EmployeeShifts]  ([EmployeeRoleID] ,[ShiftDate]  ,[StartTime] ,[EndTime] ,[Description] ,[UserID])
     VALUES  (4, '2025-01-28', '10:15:00', '13:30:00', 'Monday shift', 1)

INSERT INTO [staffing].[EmployeeShifts]  ([EmployeeRoleID] ,[ShiftDate]  ,[StartTime] ,[EndTime] ,[Description] ,[UserID])
     VALUES  (5, '2025-01-28', '14:10:00', '16:00:00', 'Monday shift', 1)

INSERT INTO [staffing].[EmployeeShifts]  ([EmployeeRoleID] ,[ShiftDate]  ,[StartTime] ,[EndTime] ,[Description] ,[UserID])
     VALUES  (4, '2025-01-28', '18:30:00', '20:00:00', 'Monday shift', 1)

	 ----------July Dancheva
INSERT INTO [staffing].[EmployeeShifts]  ([EmployeeRoleID] ,[ShiftDate]  ,[StartTime] ,[EndTime] ,[Description] ,[UserID])
     VALUES  (8, '2025-01-28', '07:30:00', '09:00:00', 'Monday shift', 1)

INSERT INTO [staffing].[EmployeeShifts]  ([EmployeeRoleID] ,[ShiftDate]  ,[StartTime] ,[EndTime] ,[Description] ,[UserID])
     VALUES  (7, '2025-01-28', '10:30:00', '13:10:00', 'Monday shift', 1)

INSERT INTO [staffing].[EmployeeShifts]  ([EmployeeRoleID] ,[ShiftDate]  ,[StartTime] ,[EndTime] ,[Description] ,[UserID])
     VALUES  (6, '2025-01-28', '15:30:00', '17:00:00', 'Monday shift', 1)

GO

select * from [staffing].[EmployeeShifts]

select * from [staffing].[EmployeeShiftsV] 
where EmployeeID = 5 --and RoleID = 1 
--and ShiftDate = '2025-01-20'
order by ShiftDate, StartTime, EmployeeID

select * from [staffing].[EmployeeRolesV]  where EmployeeID = 4

select * from [staffing].[Employees] where ID in (4, 5)
select * from [staffing].[Roles] where ID in (1, 2, 5)
select * from [staffing].[EmployeeRoles] where EmployeeID = 4
select * from [staffing].[EmployeeRoles] where EmployeeID = 5
select * from [staffing].[EmployeeShifts]



select * from [staffing].[EmployeeShiftsV] where EmployeeID = 9

Declare @RoleID int = 3 -- IsPrimary = 1
select * from [staffing].[Roles] where ID = 3
select * from [staffing].[EmployeeRoles] where EmployeeID = 4


select * from [staffing].[EmployeeShiftsV] where EmployeeID = 4




select emplr.ID as EmployeeRoleID, empl.ID as EmployeeID, empl.FullName, rol.ID as RoleID, rol.[Name], rol.IsAvailable 
from [staffing].[EmployeeRoles] emplr 
	join [staffing].[Employees] empl on empl.ID = emplr.EmployeeID
	join [staffing].[Roles] rol on rol.ID = emplr.RoleID
where empl.ID = 4

select 
	    emplr.ID as EmployeeRoleID 
	   ,empl.ID as EmployeeID 
	   ,empl.FullName 
	   ,rol.ID as RoleID 
	   ,rol.[Name] as RoleName 
	   ,rol.IsAvailable
	   ,emplSh.ID as EmpoyeeShiftID
	   ,emplSh.ShiftDate
	   ,emplSh.StartTime
	   ,emplSh.EndTime
	   ,emplSh.[Description] as EmployeeShiftDescr
from [staffing].[EmployeeShifts] emplSh
	join [staffing].[EmployeeRoles] emplR on emplR.ID = emplSh.EmployeeRoleID
	join [staffing].[Employees] empl on empl.ID = emplr.EmployeeID
	join [staffing].[Roles] rol on rol.ID = emplr.RoleID