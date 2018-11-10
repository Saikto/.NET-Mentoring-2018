-- Âûáğàòü â òàáëèöå Orders çàêàçû, êîòîğûå áûëè äîñòàâëåíû ïîñëå 6 ìàÿ 1998 ãîäà (ShippedDate)
-- íå âêëş÷àÿ ıòó äàòó èëè êîòîğûå åùå íå äîñòàâëåíû. Â çàïğîñå äîëæíû âîçâğàùàòüñÿ òîëüêî êîëîíêè OrderID
-- (ïåğåèìåíîâàòü â Order Number) è ShippedDate (ïåğåèìåíîâàòü â Shipped Date).
-- Â ğåçóëüòàòàõ çàïğîñà âîçâğàùàòü äëÿ êîëîíêè ShippedDate âìåñòî çíà÷åíèé NULL ñòğîêó ‘Not Shipped’,
-- äëÿ îñòàëüíûõ çíà÷åíèé âîçâğàùàòü äàòó â ôîğìàòå ïî óìîë÷àíèş.

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