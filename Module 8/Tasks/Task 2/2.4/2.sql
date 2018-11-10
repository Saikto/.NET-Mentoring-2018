-- Выдать всех продавцов, которые имеют более 150 заказов. Использовать вложенный SELECT.

SELECT Employees.[EmployeeID]
FROM [dbo].[Employees] Employees
WHERE 
	(SELECT COUNT(Orders.[OrderID]) 
	FROM [dbo].[Orders] Orders 
	WHERE Orders.[EmployeeID] = Employees.[EmployeeID]) > 150;