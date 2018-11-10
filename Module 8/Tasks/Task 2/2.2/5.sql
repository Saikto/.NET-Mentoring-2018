-- Найти всех покупателей, которые живут в одном городе

SELECT 
	Customers.[City],
	Customers.[ContactName]
FROM [dbo].[Customers] Customers
WHERE City IN 
	(SELECT Customers.[City]
	FROM [dbo].[Customers] Customers
	GROUP BY Customers.[City]
	HAVING COUNT(*) > 1)
ORDER BY Customers.[City]
