-- ����� ����� ����� ���� ������� �� ������� Order Details � ������ ���������� ����������� ������� � ������ �� ���.
-- ����������� ������� ������ ���� ���� ������ � ����� �������� � ��������� ������� 'Totals'.

SELECT SUM(OrderDetails.[Quantity] * OrderDetails.[UnitPrice] * (1 - OrderDetails.[Discount])) AS 'Totals'
FROM [dbo].[Order Details] OrderDetails;