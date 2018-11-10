-- ������ ���� ����������� (������� CompanyName � ������� Suppliers), � ������� ��� ���� �� ������ �������� �� ������
-- (UnitsInStock � ������� Products ����� 0). ������������ ��������� SELECT ��� ����� ������� � �������������� 
-- ��������� IN.

SELECT Suppliers.[CompanyName]
FROM [dbo].[Suppliers] Suppliers
WHERE Suppliers.[SupplierID] IN 
	(SELECT ProductsT.[SupplierID]
	FROM [dbo].[Products] ProductsT
	WHERE ProductsT.[UnitsInStock] = 0);
