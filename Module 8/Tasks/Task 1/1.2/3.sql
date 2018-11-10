-- ¬ыбрать из таблицы Customers все страны, в которых проживают заказчики.
-- —трана должна быть упом€нута только один раз и список отсортирован по убыванию.
-- Ќе использовать предложение GROUP BY. ¬озвращать только одну колонку в результатах запроса.

SELECT DISTINCT
    Customers.[Country] AS 'Country'
FROM [dbo].[Customers] Customers
ORDER BY Customers.[Country] DESC;