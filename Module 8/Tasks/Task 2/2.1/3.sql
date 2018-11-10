-- �� ������� Orders ����� ���������� ��������� ����������� (CustomerID), ��������� ������.
-- ������������ ������� COUNT � �� ������������ ����������� WHERE � GROUP.

SELECT COUNT(DISTINCT Orders.[CustomerID]) AS 'Customers with orders count' 
FROM [dbo].[Orders] Orders;