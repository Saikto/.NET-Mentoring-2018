-- � ������� Products ����� ��� �������� (������� ProductName), ��� ����������� ��������� 'chocolade'.
-- ��������, ��� � ��������� 'chocolade' ����� ���� �������� ���� ����� 'c' � �������� - ����� ��� ��������,
-- ������� ������������� ����� �������

SELECT Products.[ProductName]
FROM [dbo].[Products] Products
WHERE Products.[ProductName] LIKE '%cho_olade%'