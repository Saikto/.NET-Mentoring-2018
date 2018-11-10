-- ������ ���� ���������� (������� Customers), ������� �� ����� �� ������ ������ (��������� �� ������� Orders).
-- ������������ �������� EXISTS.

SELECT Customers.[CustomerId]
FROM [dbo].[Customers] Customers
WHERE NOT EXISTS 
	(SELECT Orders.[OrderId] 
	FROM [dbo].[Orders] Orders 
	WHERE Orders.[CustomerID] = Customers.[CustomerID]);