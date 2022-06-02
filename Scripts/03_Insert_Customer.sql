USE TheatricalPlayers;

BEGIN TRY
	BEGIN TRAN

	IF NOT EXISTS (SELECT * FROM [Customer] WHERE [Name] = 'Amauri')
	BEGIN
		INSERT INTO [Customer] ([Name], [CreationDate], [LastModifiedDate], [Active]) VALUES ('Amauri', GETUTCDATE(), GETUTCDATE(), 1)
	END

	IF NOT EXISTS (SELECT * FROM [Customer] WHERE [Name] = 'Augusto')
	BEGIN
		INSERT INTO [Customer] ([Name], [CreationDate], [LastModifiedDate], [Active]) VALUES ('Augusto', GETUTCDATE(), GETUTCDATE(), 1)
	END

	IF NOT EXISTS (SELECT * FROM [Customer] WHERE [Name] = 'Paludeto')
	BEGIN
		INSERT INTO [Customer] ([Name], [CreationDate], [LastModifiedDate], [Active]) VALUES ('Paludeto', GETUTCDATE(), GETUTCDATE(), 1)
	END

	COMMIT
END TRY
BEGIN CATCH
	ROLLBACK
END CATCH