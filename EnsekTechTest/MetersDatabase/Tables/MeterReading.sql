CREATE TABLE [dbo].[MeterReading]
(
	[Id] INT NOT NULL IDENTITY PRIMARY KEY,
	[AccountId] INT NOT NULL,
	[MeterReadingDateTime] DATETIME2,
	[MeterReadValue] INT,

	CONSTRAINT [FK_Account_MeterReading] FOREIGN KEY ([AccountId]) REFERENCES [Account]([Id])
)
