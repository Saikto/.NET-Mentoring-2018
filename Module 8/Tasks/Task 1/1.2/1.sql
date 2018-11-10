-- Выбрать из таблицы Customers всех заказчиков, проживающих в USA и Canada.
-- Запрос сделать с только помощью оператора IN. Возвращать колонки с именем пользователя и названием страны
-- в результатах запроса. Упорядочить результаты запроса по имени заказчиков и по месту проживания.

SELECT 
    Customers.[ContactName] AS 'Contact Name',
	Customers.[Country] AS 'Country'
FROM [dbo].[Customers] Customers
WHERE Customers.[Country] IN ('USA', 'Canada')
ORDER BY Customers.[ContactName], Customers.[Address];