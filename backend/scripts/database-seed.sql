USE [TekusDb]
GO

-- Warning: This script inserts data directly and generates new GUIDs.
-- If the DatabaseSeeder.cs is used, EF Core will handle seeding automatically on startup.
-- This script is provided if manual seeding is preferred.

DECLARE @UserId UNIQUEIDENTIFIER = NEWID();
INSERT INTO [dbo].[Users] ([Id], [Email], [PasswordHash], [FullName], [Role])
VALUES (@UserId, N'admin@tekus.com', N'$2a$11$m9O7G1n1o2wT5R8V0sP8e.5A0mG4xL8aQ.V7kL1sJ8wH0nO.pZ3Vq', N'System Administrator', N'Admin')
-- Note: the hash above corresponds to 'Admin123!' but may vary. It's better to use EF seeding for bcrypt.

-- Delete existing to avoid duplicates if running multiple times manually
DELETE FROM [dbo].[ProviderServices];
DELETE FROM [dbo].[Services];
DELETE FROM [dbo].[Providers];

DECLARE @S1 UNIQUEIDENTIFIER = NEWID();
DECLARE @S2 UNIQUEIDENTIFIER = NEWID();
DECLARE @S3 UNIQUEIDENTIFIER = NEWID();
DECLARE @S4 UNIQUEIDENTIFIER = NEWID();
DECLARE @S5 UNIQUEIDENTIFIER = NEWID();
DECLARE @S6 UNIQUEIDENTIFIER = NEWID();
DECLARE @S7 UNIQUEIDENTIFIER = NEWID();
DECLARE @S8 UNIQUEIDENTIFIER = NEWID();

INSERT INTO [dbo].[Services] ([Id], [Name], [HourlyRate], [CreatedAt]) VALUES 
(@S1, N'Descarga espacial de contenidos', 50.00, GETUTCDATE()),
(@S2, N'Desaparición forzada de bytes', 75.50, GETUTCDATE()),
(@S3, N'Desarrollo Web Frontend', 40.00, GETUTCDATE()),
(@S4, N'Desarrollo Web Backend', 45.00, GETUTCDATE()),
(@S5, N'Consultoría Cloud', 120.00, GETUTCDATE()),
(@S6, N'Auditoría de Seguridad', 150.00, GETUTCDATE()),
(@S7, N'Diseño UI/UX', 35.00, GETUTCDATE()),
(@S8, N'Soporte Técnico L2', 25.00, GETUTCDATE());

DECLARE @P1 UNIQUEIDENTIFIER = NEWID();
DECLARE @P2 UNIQUEIDENTIFIER = NEWID();
DECLARE @P3 UNIQUEIDENTIFIER = NEWID();

INSERT INTO [dbo].[Providers] ([Id], [Nit], [Name], [WebsiteUrl], [Email], [Country], [CreatedAt]) VALUES 
(@P1, N'800123456-1', N'Importaciones Tekus S.A.', N'https://tekus.com', N'contacto@tekus.com', N'Colombia', GETUTCDATE()),
(@P2, N'900987654-2', N'Global Cloud Tech', N'https://globalcloud.com', N'info@globalcloud.com', N'USA', GETUTCDATE()),
(@P3, N'700456123-3', N'Sistemas Andinos', N'https://sistemasandinos.co', N'ventas@sandinos.co', N'Colombia', GETUTCDATE());

INSERT INTO [dbo].[ProviderServices] ([Id], [ProviderId], [ServiceId], [CustomHourlyRate]) VALUES 
(NEWID(), @P1, @S1, NULL),
(NEWID(), @P1, @S2, NULL),
(NEWID(), @P2, @S5, NULL),
(NEWID(), @P2, @S6, NULL),
(NEWID(), @P3, @S3, NULL),
(NEWID(), @P3, @S4, NULL),
(NEWID(), @P3, @S8, NULL);
GO
