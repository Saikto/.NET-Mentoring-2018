-- �� ������� Orders ����� ���������� ������� � ������������ �� �����.
--  � ����������� ������� ���� ���������� ��� ������� c ���������� Year � Total.
-- �������� ����������� ������, ������� ��������� ���������� ���� �������

SELECT 
    YEAR(Orders.[OrderDate]) AS 'Year',
	COUNT(Orders.[OrderId]) AS 'Total' 
FROM [dbo].[Orders] Orders
GROUP BY YEAR(Orders.[OrderDate]);

SELECT 
	COUNT(Orders.[OrderId]) AS 'Total' 
FROM [dbo].[Orders] Orders;