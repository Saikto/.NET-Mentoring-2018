-- По таблице Employees найти для каждого продавца его руководителя.

SELECT 
    Employees.[EmployeeID] AS 'EmployeeID',
	Employees.[FirstName] AS 'Seller name',
	(SELECT Managers.[FirstName] 
        FROM [dbo].[Employees] Managers
        WHERE Managers.[EmployeeID] = Employees.[ReportsTo]) AS 'Manager'
FROM [dbo].[Employees] Employees;