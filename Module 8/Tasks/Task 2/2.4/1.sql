-- ¬ыдать всех поставщиков (колонка CompanyName в таблице Suppliers), у которых нет хот€ бы одного продукта на складе
-- (UnitsInStock в таблице Products равно 0). »спользовать вложенный SELECT дл€ этого запроса с использованием 
-- оператора IN.

SELECT Suppliers.[CompanyName]
FROM [dbo].[Suppliers] Suppliers
WHERE Suppliers.[SupplierID] IN 
	(SELECT ProductsT.[SupplierID]
	FROM [dbo].[Products] ProductsT
	WHERE ProductsT.[UnitsInStock] = 0);
