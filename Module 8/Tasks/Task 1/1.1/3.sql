-- ������� � ������� Orders ������, ������� ���� ���������� ����� 6 ��� 1998 ���� (ShippedDate)
-- �� ������� ��� ���� ��� ������� ��� �� ����������. � ������� ������ ������������ ������ ������� OrderID
-- (������������� � Order Number) � ShippedDate (������������� � Shipped Date).
-- � ����������� ������� ���������� ��� ������� ShippedDate ������ �������� NULL ������ �Not Shipped�,
-- ��� ��������� �������� ���������� ���� � ������� �� ���������.

DECLARE
    @date DATETIME = Convert(DATETIME, '1998-05-06'),
	@default_dt_format INT = 0

SELECT 
	Orders.[OrderID] AS 'Order Number',
CASE 
	WHEN Orders.[ShippedDate] IS NULL 
	THEN 'Not shipped'
	ELSE CONVERT(VARCHAR(30), Orders.[ShippedDate], @default_dt_format)
END 
AS 'Shipped Date'
FROM [dbo].[Orders] Orders
WHERE Orders.[ShippedDate] IS NULL OR Orders.[ShippedDate] > @date;