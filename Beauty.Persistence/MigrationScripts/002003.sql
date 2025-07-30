IF SCHEMA_ID(N'Sms') IS NULL EXEC(N'CREATE SCHEMA [Sms];');

GO

CREATE TABLE [Sms].[Histories] (
    [Id] int NOT NULL IDENTITY,
    [CreateDateTime] datetime2 NOT NULL,
    [CreateUser] uniqueidentifier NOT NULL,
    [IsDeleted] bit NOT NULL,
    [Text] nvarchar(max) NULL,
    [ReceptorNumber] nvarchar(max) NULL,
    [SenderNumber] nvarchar(max) NULL,
    [Messageid] bigint NOT NULL,
    [Status] int NOT NULL,
    [StatusText] nvarchar(max) NULL,
    CONSTRAINT [PK_Histories] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Sms].[Messages] (
    [Id] smallint NOT NULL IDENTITY,
    [CreateDateTime] datetime2 NOT NULL,
    [CreateUser] uniqueidentifier NOT NULL,
    [IsDeleted] bit NOT NULL,
    [Type] int NOT NULL,
    [Text] nvarchar(max) NULL,
    [ParametersForSample] nvarchar(max) NULL,
    [AllowSend] bit NOT NULL,
    [SendTime] time NULL,
    CONSTRAINT [PK_Messages] PRIMARY KEY ([Id])
);

GO

INSERT INTO [_].[__DbMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201019140215_002', N'3.1.8');

GO

ALTER TABLE [Sms].[Messages] ADD [Title] nvarchar(max) NULL;

GO

INSERT INTO [_].[__DbMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201019192726_003', N'3.1.8');

GO

