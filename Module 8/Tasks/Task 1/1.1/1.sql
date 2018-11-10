-- ������� � ������� Orders ������, ������� ���� ���������� ����� 6 ��� 1998 ���� (������� ShippedDate)
-- ������������ � ������� ���������� � ShipVia >= 2. ������ ������ ���������� ������ ������� OrderID, ShippedDate
-- � ShipVia.
DECLARE
    @date DATETIME = Convert(DATETIME, '1998-05-06'),
    @shipVia INT = 2;
    

SELECT 
    Orders.[OrderID] AS 'OrderID',
    Orders.[ShippedDate] AS 'ShippedDate',
    Orders.[ShipVia] AS 'ShipVia'
FROM [dbo].[Orders] Orders
WHERE Orders.[ShippedDate] >= @date AND Orders.[ShipVia] >= @shipVia;