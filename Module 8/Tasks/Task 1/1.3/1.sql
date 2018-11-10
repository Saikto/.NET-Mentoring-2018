-- ������� ��� ������ (OrderID) �� ������� Order Details (������ �� ������ �����������),
-- ��� ����������� �������� � ����������� �� 3 �� 10 ������������ � ��� ������� Quantity � ������� Order Details.
-- ������������ �������� BETWEEN. ������ ������ ���������� ������ ������� OrderID.

SELECT DISTINCT 
	OrderDetails.[OrderID] AS 'OrderID'
FROM [dbo].[Order Details] OrderDetails
WHERE OrderDetails.[Quantity] BETWEEN 3 AND 10;