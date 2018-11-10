IF OBJECT_ID('EmployeeCreditCards') IS NULL
BEGIN
    CREATE TABLE [dbo].[EmployeeCreditCards]
	(
        [CreditCardNumber]	 INT 			NOT NULL,
        [ExpirationDate]	 DATETIME 		NOT NULL,
        [CardHolderName]	 VARCHAR(50)	NOT NULL,
        [EmployeeId]		 INT 			NOT NULL,
		CONSTRAINT [PK_EmployeeCreditCards] PRIMARY KEY ([CreditCardNumber] ASC),
		CONSTRAINT [FK_EmployeeCreditCards_Employees] FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employees] ([EmployeeID])
    );
END