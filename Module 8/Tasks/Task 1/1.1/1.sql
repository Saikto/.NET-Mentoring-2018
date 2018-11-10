-- ‚ыбрать в таблице Orders заказы, которые были доставлены после 6 мая 1998 года (колонка ShippedDate)
-- включительно и которые доставлены с ShipVia >= 2. Запрос должен возвращать только колонки OrderID, ShippedDate
-- и ShipVia.
DECLARE
    @date DATETIME = Convert(DATETIME, '1998-05-06'),
    @shipVia INT = 2;
    

SELECT 
    Orders.[OrderID] AS 'OrderID',
    Orders.[ShippedDate] AS 'ShippedDate',
    Orders.[ShipVia] AS 'ShipVia'
FROM [dbo].[Orders] Orders
WHERE Orders.[ShippedDate] >= @date AND Orders.[ShipVia] >= @shipVia;