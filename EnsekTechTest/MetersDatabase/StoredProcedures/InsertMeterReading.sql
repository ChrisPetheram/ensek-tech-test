CREATE PROCEDURE [dbo].[InsertMeterReading]
	@AccountId INT,
	@ReadingTime DATETIME2,
	@ReadingValue INT
AS
BEGIN TRY
		INSERT INTO [MeterReading] (
			AccountId, 
			MeterReadingDateTime, 
			MeterReadValue)
		VALUES (
			@AccountId,
			@ReadingTime,
			@ReadingValue
		);

		RETURN SCOPE_IDENTITY();
	END TRY
	BEGIN CATCH
		RETURN 0
	END CATCH	
