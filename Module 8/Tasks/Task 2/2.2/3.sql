-- ѕо таблице Orders найти количество заказов, сделанных каждым продавцом и дл€ каждого покупател€.
-- Ќеобходимо определить это только дл€ заказов, сделанных в 1998 году.

DECLARE 
    @year INT = 1998;

SELECT 
	Orders.[EmployeeID],
	Orders.[CustomerID],
	COUNT(Orders.[OrderID]) AS 'Orders count'
FROM [dbo].[Orders] Orders 
WHERE YEAR(Orders.[OrderDate]) = @year
GROUP BY Orders.[EmployeeID], Orders.[CustomerID];