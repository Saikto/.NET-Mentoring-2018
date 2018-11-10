-- Выдать в результатах запроса имена всех заказчиков из таблицы Customers и суммарное количество
-- их заказов из таблицы Orders. Принять во внимание, что у некоторых заказчиков нет заказов,
-- но они также должны быть выведены в результатах запроса.
-- Упорядочить результаты запроса по возрастанию количества заказов.

SELECT 
	Customers.[ContactName],
	COUNT(Orders.[OrderId]) AS 'OrdersCount'
FROM [dbo].[Customers] Customers
    LEFT JOIN [dbo].[Orders] Orders	ON Customers.[CustomerId] = Orders.[CustomerId]
GROUP BY Customers.[CustomerID], Customers.[ContactName]
ORDER BY 'OrdersCount';