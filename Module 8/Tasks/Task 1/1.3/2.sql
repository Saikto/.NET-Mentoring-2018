-- ������� ���� ���������� �� ������� Customers, � ������� �������� ������ ���������� �� ����� �� ��������� b � g.
-- ������������ �������� BETWEEN. ���������, ��� � ���������� ������� �������� Germany.
-- ������ ������ ���������� ������ ������� CustomerID � Country � ������������ �� Country.

SELECT 
	Customers.[CustomerId] AS 'CustomerId',
    Customers.[Country] AS 'Country'
FROM [dbo].[Customers] Customers
WHERE SUBSTRING(Customers.[Country], 1, 1) BETWEEN 'b' AND 'g'
ORDER BY Customers.[Country];