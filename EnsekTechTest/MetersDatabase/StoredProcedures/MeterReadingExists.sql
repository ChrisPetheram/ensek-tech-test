CREATE PROCEDURE [dbo].[MeterReadingExists]
	@AccountId INT,
	@ReadingTime DATETIME2,
	@ReadingValue INT
AS
	SELECT COUNT(*) 
	FROM [MeterReading] 
	WHERE 
		[AccountId] = @AccountId
	AND [MeterReadingDateTime] = @ReadingTime
	AND [MeterReadValue] = @ReadingValue
