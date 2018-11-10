-- �� ������� Orders ����� ���������� �������, c�������� ������ ���������. ����� ��� ���������� ��������-
-- ��� ����� ������ � ������� Orders, ��� � ������� EmployeeID ������ �������� ��� ������� ��������.
-- � ����������� ������� ���� ���������� ������� � ������ �������� 
-- (������ ������������� ��� ���������� ������������� LastName & FirstName.
-- ��� ������ LastName & FirstName ������ ���� �������� ��������� �������� � ������� ��������� �������.
-- ����� �������� ������ ������ ������������ ����������� �� EmployeeID.) � ��������� ������� �Seller� � �������
-- c ����������� ������� ���������� � ��������� 'Amount'. ���������� ������� ������ ���� ����������� �� ��������
-- ���������� �������.

SELECT 
    (SELECT CONCAT(Employees.[LastName],' ', Employees.[FirstName]) 
     FROM [dbo].[Employees] Employees
     WHERE Employees.[EmployeeID] = Orders.[EmployeeID]) AS 'Seller',
	COUNT(Orders.[OrderId]) AS 'Amount'
FROM [dbo].[Orders] Orders 
GROUP BY Orders.[EmployeeID]
ORDER BY 'Amount' DESC;