-- Выбрать всех заказчиков из таблицы Customers, у которых название страны начинается на буквы из диапазона b и g,
-- не используя оператор BETWEEN.

SELECT 
	Customers.[CustomerId] AS 'CustomerId',
    Customers.[Country] AS 'Country'
FROM [dbo].[Customers] Customers
WHERE SUBSTRING(Customers.[Country], 1, 1) IN ('b', 'c', 'd', 'e', 'f', 'g')
ORDER BY Customers.[Country];