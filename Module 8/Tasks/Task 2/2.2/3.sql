-- �� ������� Orders ����� ���������� �������, ��������� ������ ��������� � ��� ������� ����������.
-- ���������� ���������� ��� ������ ��� �������, ��������� � 1998 ����.

DECLARE 
    @year INT = 1998;

SELECT 
	Orders.[EmployeeID],
	Orders.[CustomerID],
	COUNT(Orders.[OrderID]) AS 'Orders count'
FROM [dbo].[Orders] Orders 
WHERE YEAR(Orders.[OrderDate]) = @year
GROUP BY Orders.[EmployeeID], Orders.[CustomerID];