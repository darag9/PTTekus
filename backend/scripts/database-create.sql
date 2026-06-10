-- Use master to create database
USE [master]
GO

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'TekusDb')
BEGIN
    CREATE DATABASE [TekusDb]
END
GO

USE [TekusDb]
GO

-- Create Users table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Users](
        [Id] [uniqueidentifier] NOT NULL,
        [Email] [nvarchar](200) NOT NULL,
        [PasswordHash] [nvarchar](max) NOT NULL,
        [FullName] [nvarchar](200) NOT NULL,
        [Role] [nvarchar](50) NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
    )
    CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_Email] ON [dbo].[Users] ([Email] ASC)
END
GO

-- Create Providers table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Providers]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Providers](
        [Id] [uniqueidentifier] NOT NULL,
        [Nit] [nvarchar](20) NOT NULL,
        [Name] [nvarchar](200) NOT NULL,
        [WebsiteUrl] [nvarchar](200) NULL,
        [Email] [nvarchar](200) NOT NULL,
        [Country] [nvarchar](100) NOT NULL,
        [CreatedAt] [datetime2](7) NOT NULL,
        [CreatedBy] [nvarchar](max) NULL,
        [LastModifiedAt] [datetime2](7) NULL,
        [LastModifiedBy] [nvarchar](max) NULL,
        CONSTRAINT [PK_Providers] PRIMARY KEY CLUSTERED ([Id] ASC)
    )
    CREATE UNIQUE NONCLUSTERED INDEX [IX_Providers_Nit] ON [dbo].[Providers] ([Nit] ASC)
END
GO

-- Create Services table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Services]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Services](
        [Id] [uniqueidentifier] NOT NULL,
        [Name] [nvarchar](200) NOT NULL,
        [HourlyRate] [decimal](18, 2) NOT NULL,
        [CreatedAt] [datetime2](7) NOT NULL,
        [CreatedBy] [nvarchar](max) NULL,
        [LastModifiedAt] [datetime2](7) NULL,
        [LastModifiedBy] [nvarchar](max) NULL,
        CONSTRAINT [PK_Services] PRIMARY KEY CLUSTERED ([Id] ASC)
    )
END
GO

-- Create ProviderServices table (Many-to-Many join table)
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProviderServices]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[ProviderServices](
        [Id] [uniqueidentifier] NOT NULL,
        [ProviderId] [uniqueidentifier] NOT NULL,
        [ServiceId] [uniqueidentifier] NOT NULL,
        [CustomHourlyRate] [decimal](18, 2) NULL,
        CONSTRAINT [PK_ProviderServices] PRIMARY KEY CLUSTERED ([Id] ASC),
        CONSTRAINT [FK_ProviderServices_Providers_ProviderId] FOREIGN KEY([ProviderId]) REFERENCES [dbo].[Providers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ProviderServices_Services_ServiceId] FOREIGN KEY([ServiceId]) REFERENCES [dbo].[Services] ([Id]) ON DELETE CASCADE
    )
    CREATE UNIQUE NONCLUSTERED INDEX [IX_ProviderServices_ProviderId_ServiceId] ON [dbo].[ProviderServices] ([ProviderId] ASC, [ServiceId] ASC)
    CREATE NONCLUSTERED INDEX [IX_ProviderServices_ServiceId] ON [dbo].[ProviderServices] ([ServiceId] ASC)
END
GO
