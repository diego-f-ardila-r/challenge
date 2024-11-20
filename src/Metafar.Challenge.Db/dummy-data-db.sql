-- Data dummy for users, cards, accounts

-- Insert into dbo.users
INSERT INTO dbo.users (user_id, first_name, last_name, created_date, updated_date, user_name)
VALUES
    (NEWID(), 'John', 'Doe', GETDATE(), GETDATE(), 'johndoe'),
    (NEWID(), 'Jane', 'Smith', GETDATE(), GETDATE(), 'janesmith'),
    (NEWID(), 'Michael', 'Brown', GETDATE(), GETDATE(), 'michaelb');

-- Retrieve inserted user IDs for relationships
DECLARE @UserId1 UNIQUEIDENTIFIER = (SELECT TOP 1 user_id FROM dbo.users WHERE user_name = 'johndoe');
DECLARE @UserId2 UNIQUEIDENTIFIER = (SELECT TOP 1 user_id FROM dbo.users WHERE user_name = 'janesmith');
DECLARE @UserId3 UNIQUEIDENTIFIER = (SELECT TOP 1 user_id FROM dbo.users WHERE user_name = 'michaelb');

-- Insert into dbo.accounts
INSERT INTO dbo.accounts (account_id, user_id, account_number, balance, created_date, updated_date)
VALUES
    (NEWID(), @UserId1, 1001, 1500.00, GETDATE(), GETDATE()),
    (NEWID(), @UserId2, 1002, 2000.00, GETDATE(), GETDATE()),
    (NEWID(), @UserId3, 1003, 500.00, GETDATE(), GETDATE());

-- Retrieve inserted account IDs for relationships
DECLARE @AccountId1 UNIQUEIDENTIFIER = (SELECT TOP 1 account_id FROM dbo.accounts WHERE account_number = 1001);
DECLARE @AccountId2 UNIQUEIDENTIFIER = (SELECT TOP 1 account_id FROM dbo.accounts WHERE account_number = 1002);
DECLARE @AccountId3 UNIQUEIDENTIFIER = (SELECT TOP 1 account_id FROM dbo.accounts WHERE account_number = 1003);

-- Insert into dbo.cards
INSERT INTO dbo.cards (card_id, account_id, card_number, access_pin, failed_attempts, is_blocked, created_date, updated_date)
VALUES
    (NEWID(), @AccountId1, 12345678, 1234, 0, 0, GETDATE(), GETDATE()),
    (NEWID(), @AccountId2, 23456789, 5678, 2, 0, GETDATE(), GETDATE()),
    (NEWID(), @AccountId3, 34567890, 9012, 4, 1, GETDATE(), GETDATE());

-- Insert into dbo.operations
INSERT INTO dbo.operations (operation_id, account_id, operation_type, amount, created_date, updated_date)
VALUES
    (NEWID(), @AccountId1, 'Deposit', 500.00, GETDATE(), GETDATE()),
    (NEWID(), @AccountId1, 'Withdrawal', 200.00, GETDATE(), GETDATE()),
    (NEWID(), @AccountId2, 'Transfer', 100.00, GETDATE(), GETDATE()),
    (NEWID(), @AccountId3, 'Withdrawal', 50.00, GETDATE(), GETDATE());

-- Generate operations
-- Insert 100 random records into dbo.operations
DECLARE @i INT = 1;
WHILE @i <= 100
    BEGIN
        -- Randomly select an account ID
        DECLARE @SelectedAccountId UNIQUEIDENTIFIER =
            CASE
                WHEN @i % 3 = 1 THEN @AccountId1
                WHEN @i % 3 = 2 THEN @AccountId2
                ELSE @AccountId3
                END;

        -- Randomly select an operation type
        DECLARE @OperationType NVARCHAR(MAX) =
            CASE
                WHEN @i % 4 = 0 THEN 'Deposit'
                WHEN @i % 4 = 1 THEN 'Withdrawal'
                WHEN @i % 4 = 2 THEN 'Transfer'
                ELSE 'Payment'
                END;

        -- Randomly generate an amount between 10 and 1000
        DECLARE @Amount DECIMAL(18, 2) = ROUND((RAND() * (1000 - 10) + 10), 2);

        -- Insert the record
        INSERT INTO dbo.operations (operation_id, account_id, operation_type, amount, created_date, updated_date)
        VALUES (NEWID(), @SelectedAccountId, @OperationType, @Amount, DATEADD(DAY, -@i, GETDATE()), DATEADD(DAY, -@i, GETDATE()));

        -- Update the account balance based on the operation type
        IF @OperationType IN ('Deposit', 'Transfer')
            BEGIN
                UPDATE dbo.accounts
                SET balance = balance + @Amount
                WHERE account_id = @SelectedAccountId;
            END
        ELSE -- 'Withdrawal' or 'Payment'
            BEGIN
                UPDATE dbo.accounts
                SET balance = balance - @Amount
                WHERE account_id = @SelectedAccountId;
            END;

        SET @i = @i + 1;
    END;
