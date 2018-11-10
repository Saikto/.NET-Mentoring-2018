-- Выбрать из таблицы Customers всех заказчиков, не проживающих в USA и Canada.
-- Запрос сделать с помощью оператора IN. Возвращать колонки с именем пользователя и названием страны
-- в результатах запроса. Упорядочить результаты запроса по имени заказчиков.

SELECT 
    Customers.[ContactName] AS 'Contact Name',
    Customers.[Country] AS 'Country'
FROM [dbo].[Customers] Customers
WHERE Customers.[Country] NOT IN ('USA', 'Canada')
ORDER BY Customers.[ContactName];