-- Определить продавцов, которые обслуживают регион 'Western' (таблица Region).

SELECT DISTINCT 
    Employees.[EmployeeId],
    Employees.[FirstName]
FROM [dbo].[Employees] Employees

	INNER JOIN [dbo].[EmployeeTerritories] EmployeeTerritories 
	ON Employees.[EmployeeID] = EmployeeTerritories.[EmployeeID]

	INNER JOIN [dbo].[Territories] Territories 
	ON EmployeeTerritories.[TerritoryID] = Territories.[TerritoryID]

	INNER JOIN [dbo].[Region] Region 
	ON Region.[RegionID] = Territories.[RegionID]

WHERE Region.[RegionDescription] = 'Western';