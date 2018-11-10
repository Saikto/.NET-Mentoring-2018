-- Найти покупателей и продавцов, которые живут в одном городе.
-- Если в городе живут только один или несколько продавцов, или только один или несколько покупателей,
-- то информация о таких покупателя и продавцах не должна попадать в результирующий набор.
-- Не использовать конструкцию JOIN.

SELECT Customers.[City], 
       Customers.[CompanyName] AS 'Customer',
       CONCAT(Employees.[FirstName], ' ', Employees.[LastName]) AS 'Employee' 
FROM [dbo].[Customers] Customers,
	 [dbo].[Employees] Employees
WHERE Customers.[City] = Employees.[City]
ORDER BY Customers.[CustomerID], Employees.[EmployeeID]
