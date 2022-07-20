DECLARE @seedAccounts TABLE (
	[Id] INT NOT NULL PRIMARY KEY,
	[FirstName] NVARCHAR(50),
	[LastName] NVARCHAR(50)	
	);

INSERT INTO @seedAccounts
VALUES 
(2344, 'Tommy', 'Test'),
(2233, 'Barry', 'Test'),
(8766, 'Sally', 'Test'),
(2345, 'Jerry', 'Test'),
(2346, 'Ollie', 'Test'),
(2347, 'Tara', 'Test'),
(2348, 'Tammy', 'Test'),
(2349, 'Simon', 'Test'),
(2350, 'Colin', 'Test'),
(2351, 'Gladys', 'Test'),
(2352, 'Greg', 'Test'),
(2353, 'Tony', 'Test'),
(2355, 'Arthur', 'Test'),
(2356, 'Craig', 'Test'),
(6776, 'Laura', 'Test'),
(4534, 'JOSH', 'TEST'),
(1234, 'Freya', 'Test'),
(1239, 'Noddy', 'Test'),
(1240, 'Archie', 'Test'),
(1241, 'Lara', 'Test'),
(1242, 'Tim', 'Test'),
(1243, 'Graham', 'Test'),
(1244, 'Tony', 'Test'),
(1245, 'Neville', 'Test'),
(1246, 'Jo', 'Test'),
(1247, 'Jim', 'Test'),
(1248, 'Pam', 'Test')

MERGE INTO [Account] as target
USING @seedAccounts as source
ON 
	target.[Id] = source.[Id]
AND target.[FirstName] = source.[FirstName]
AND target.[LastName] = source.[LastName]
WHEN NOT MATCHED THEN
	INSERT (
		[Id], 
		[FirstName], 
		[LastName])
	VALUES (
		source.[Id],
		source.[FirstName],
		source.[LastName]
		);

