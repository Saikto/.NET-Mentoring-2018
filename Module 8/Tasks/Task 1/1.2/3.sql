-- ������� �� ������� Customers ��� ������, � ������� ��������� ���������.
-- ������ ������ ���� ��������� ������ ���� ��� � ������ ������������ �� ��������.
-- �� ������������ ����������� GROUP BY. ���������� ������ ���� ������� � ����������� �������.

SELECT DISTINCT
    Customers.[Country] AS 'Country'
FROM [dbo].[Customers] Customers
ORDER BY Customers.[Country] DESC;