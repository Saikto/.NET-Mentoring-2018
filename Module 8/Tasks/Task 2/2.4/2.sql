-- ������ ���� ���������, ������� ����� ����� 150 �������. ������������ ��������� SELECT.

SELECT Employees.[EmployeeID]
FROM [dbo].[Employees] Employees
WHERE 
	(SELECT COUNT(Orders.[OrderID]) 
	FROM [dbo].[Orders] Orders 
	WHERE Orders.[EmployeeID] = Employees.[EmployeeID]) > 150;