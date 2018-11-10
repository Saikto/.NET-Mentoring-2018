-- Написать запрос, который выводит только недоставленные заказы из таблицы Orders. В результатах запроса
-- возвращать для колонки ShippedDate вместо значений NULL строку ‘Not Shipped’ (использовать системную функцию CASЕ).
-- Запрос должен возвращать только колонки OrderID и ShippedDate.

SELECT Orders.[OrderID] AS 'OrderID',
CASE 
	WHEN Orders.[ShippedDate] IS NULL 
	THEN 'Not shipped' 
END 
AS 'ShippedDate'
FROM [dbo].[Orders] Orders
WHERE Orders.[ShippedDate] IS NULL;