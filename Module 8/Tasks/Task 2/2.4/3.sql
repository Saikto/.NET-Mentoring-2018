-- Выдать всех заказчиков (таблица Customers), которые не имеют ни одного заказа (подзапрос по таблице Orders).
-- Использовать оператор EXISTS.

SELECT Customers.[CustomerId]
FROM [dbo].[Customers] Customers
WHERE NOT EXISTS 
	(SELECT Orders.[OrderId] 
	FROM [dbo].[Orders] Orders 
	WHERE Orders.[CustomerID] = Customers.[CustomerID]);