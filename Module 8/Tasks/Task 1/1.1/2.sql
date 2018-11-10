-- �������� ������, ������� ������� ������ �������������� ������ �� ������� Orders. � ����������� �������
-- ���������� ��� ������� ShippedDate ������ �������� NULL ������ �Not Shipped� (������������ ��������� ������� CAS�).
-- ������ ������ ���������� ������ ������� OrderID � ShippedDate.

SELECT Orders.[OrderID] AS 'OrderID',
CASE 
	WHEN Orders.[ShippedDate] IS NULL 
	THEN 'Not shipped' 
END 
AS 'ShippedDate'
FROM [dbo].[Orders] Orders
WHERE Orders.[ShippedDate] IS NULL;