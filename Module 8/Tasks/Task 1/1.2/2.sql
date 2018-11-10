-- ������� �� ������� Customers ���� ����������, �� ����������� � USA � Canada.
-- ������ ������� � ������� ��������� IN. ���������� ������� � ������ ������������ � ��������� ������
-- � ����������� �������. ����������� ���������� ������� �� ����� ����������.

SELECT 
    Customers.[ContactName] AS 'Contact Name',
    Customers.[Country] AS 'Country'
FROM [dbo].[Customers] Customers
WHERE Customers.[Country] NOT IN ('USA', 'Canada')
ORDER BY Customers.[ContactName];