-- ѕо таблице Orders найти количество заказов с группировкой по годам.
--  ¬ результатах запроса надо возвращать две колонки c названи€ми Year и Total.
-- Ќаписать проверочный запрос, который вычисл€ет количество всех заказов

SELECT 
    YEAR(Orders.[OrderDate]) AS 'Year',
	COUNT(Orders.[OrderId]) AS 'Total' 
FROM [dbo].[Orders] Orders
GROUP BY YEAR(Orders.[OrderDate]);

SELECT 
	COUNT(Orders.[OrderId]) AS 'Total' 
FROM [dbo].[Orders] Orders;