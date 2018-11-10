-- ����� ����������� � ���������, ������� ����� � ����� ������.
-- ���� � ������ ����� ������ ���� ��� ��������� ���������, ��� ������ ���� ��� ��������� �����������,
-- �� ���������� � ����� ���������� � ��������� �� ������ �������� � �������������� �����.
-- �� ������������ ����������� JOIN.

SELECT Customers.[City], 
       Customers.[CompanyName] AS 'Customer',
       CONCAT(Employees.[FirstName], ' ', Employees.[LastName]) AS 'Employee' 
FROM [dbo].[Customers] Customers,
	 [dbo].[Employees] Employees
WHERE Customers.[City] = Employees.[City]
ORDER BY Customers.[CustomerID], Employees.[EmployeeID]
