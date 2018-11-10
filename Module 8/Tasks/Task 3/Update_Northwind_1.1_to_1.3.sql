IF OBJECT_ID('Regions') IS NULL
BEGIN
    EXEC SP_RENAME '[dbo].[Region]', 'Regions';
END

IF COL_LENGTH('[dbo].[Customers]', 'FoundationDate') IS NULL
BEGIN
    ALTER TABLE [dbo].[Customers]
    ADD [FoundationDate] DATETIME
END
