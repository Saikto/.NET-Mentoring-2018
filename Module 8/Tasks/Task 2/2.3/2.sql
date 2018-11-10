-- ������ � ����������� ������� ����� ���� ���������� �� ������� Customers � ��������� ����������
-- �� ������� �� ������� Orders. ������� �� ��������, ��� � ��������� ���������� ��� �������,
-- �� ��� ����� ������ ���� �������� � ����������� �������.
-- ����������� ���������� ������� �� ����������� ���������� �������.

SELECT 
	Customers.[ContactName],
	COUNT(Orders.[OrderId]) AS 'OrdersCount'
FROM [dbo].[Customers] Customers
    LEFT JOIN [dbo].[Orders] Orders	ON Customers.[CustomerId] = Orders.[CustomerId]
GROUP BY Customers.[CustomerID], Customers.[ContactName]
ORDER BY 'OrdersCount';