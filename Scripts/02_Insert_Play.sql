USE TheatricalPlayers;

BEGIN TRY
	BEGIN TRAN
	
	IF NOT EXISTS (SELECT * FROM [Play] WHERE [Name] = 'Hamlet' AND [Lines] = 4024 AND [PlayType] = 1)
	BEGIN
		INSERT INTO [Play] ([Name], [Lines], [PlayType], [CreationDate], [LastModifiedDate], [Active]) VALUES ('Hamlet', 4024, 1, GETUTCDATE(), GETUTCDATE(), 1)
	END

	IF NOT EXISTS (SELECT * FROM [Play] WHERE [Name] = 'As You Like It' AND [Lines] = 2670 AND [PlayType] = 2)
	BEGIN
		INSERT INTO [Play] ([Name], [Lines], [PlayType], [CreationDate], [LastModifiedDate], [Active]) VALUES ('As You Like It', 2670, 2, GETUTCDATE(), GETUTCDATE(), 1)
	END

	IF NOT EXISTS (SELECT * FROM [Play] WHERE [Name] = 'Othello' AND [Lines] = 3560 AND [PlayType] = 1)
	BEGIN
		INSERT INTO [Play] ([Name], [Lines], [PlayType], [CreationDate], [LastModifiedDate], [Active]) VALUES ('Othello', 3560, 1, GETUTCDATE(), GETUTCDATE(), 1)
	END

	IF NOT EXISTS (SELECT * FROM [Play] WHERE [Name] = 'Henry V' AND [Lines] = 3227 AND [PlayType] = 3)
	BEGIN
		INSERT INTO [Play] ([Name], [Lines], [PlayType], [CreationDate], [LastModifiedDate], [Active]) VALUES ('Henry V', 3227, 3, GETUTCDATE(), GETUTCDATE(), 1)
	END

	IF NOT EXISTS (SELECT * FROM [Play] WHERE [Name] = 'King John' AND [Lines] = 2648 AND [PlayType] = 2)
	BEGIN
		INSERT INTO [Play] ([Name], [Lines], [PlayType], [CreationDate], [LastModifiedDate], [Active]) VALUES ('King John', 2648, 3, GETUTCDATE(), GETUTCDATE(), 1)
	END

	IF NOT EXISTS (SELECT * FROM [Play] WHERE [Name] = 'Richard III' AND [Lines] = 3718 AND [PlayType] = 3)
	BEGIN
		INSERT INTO [Play] ([Name], [Lines], [PlayType], [CreationDate], [LastModifiedDate], [Active]) VALUES ('Richard III', 3718, 3, GETUTCDATE(), GETUTCDATE(), 1)
	END

	COMMIT
END TRY
BEGIN CATCH
	ROLLBACK
END CATCH