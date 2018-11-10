-- �� ������� Orders ����� ���������� �������, ������� ��� �� ���� ���������� 
-- (�.�. � ������� ShippedDate ��� �������� ���� ��������). ������������ ��� ���� ������� ������ �������� COUNT.
-- �� ������������ ����������� WHERE � GROUP.

SELECT COUNT(*) - COUNT(Orders.[ShippedDate]) AS 'Not delivered orders count' 
FROM [dbo].[Orders] Orders;