SET IDENTITY_INSERT [dbo].[Fabricantes] ON
INSERT INTO [dbo].[Fabricantes] ([Id], [Nombre]) VALUES (1, N'Phillips')
INSERT INTO [dbo].[Fabricantes] ([Id], [Nombre]) VALUES (2, N'Wurt')
SET IDENTITY_INSERT [dbo].[Fabricantes] OFF

SET IDENTITY_INSERT [dbo].[Herramientas] ON
INSERT INTO [dbo].[Herramientas] ([Id], [Material], [Precio], [Nombre], [TiempoReparacion], [FabricanteId]) VALUES (1, N'Acero', CAST(12.50 AS Decimal(10, 2)), N'Destornillador', 1, 2)
INSERT INTO [dbo].[Herramientas] ([Id], [Material], [Precio], [Nombre], [TiempoReparacion], [FabricanteId]) VALUES (2, N'Acero', CAST(10.30 AS Decimal(10, 2)), N'Llave Inglesa', 2, 1)
SET IDENTITY_INSERT [dbo].[Herramientas] OFF

INSERT INTO [dbo].[AspNetUsers] ([Id], [Nombre], [Apellidos], [DireccionEnvio], [CorreoElectronico], [NumTelefono], [Rol], [FechaRegistro], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'1', N'Sergio', N'Gomez', N'C/ Ejemplo, 1', N'sergio.gomez@example.com', N'612345678', 2, N'03/03/2025', N'sergio.gomez', N'SERGIO.GOMEZ', N'sergio.gomez@example.com', N'SERGIO.GOMEZ@EXAMPLE.COM', 1, NULL, N'68E4382A-5EAD-4EE4-AE12-7195321871E5', N'7CCE708A-9B3A-4426-806F-EBB87FCB1CDE', N'612345678', 1, 0, NULL, 0, 0)
INSERT INTO [dbo].[AspNetUsers] ([Id], [Nombre], [Apellidos], [DireccionEnvio], [CorreoElectronico], [NumTelefono], [Rol], [FechaRegistro], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'2', N'Lucia', N'Martinez', N'C/ Ejemplo, 2', N'lucia.martinez@example.com', N'623456789', 2, N'02/02/2025', N'lucia.martinez', N'LUCIA.MARTINEZ', N'lucia.martinez@example.com', N'LUCIA.MARTINEZ@EXAMPLE.COM', 1, NULL, N'CC99064E-F27D-49B1-9D89-EC50B0E15AB5', N'C0DAC331-04F9-4C4D-8FEF-1807CFBB3237', N'623456789', 1, 0, NULL, 0, 0)
INSERT INTO [dbo].[AspNetUsers] ([Id], [Nombre], [Apellidos], [DireccionEnvio], [CorreoElectronico], [NumTelefono], [Rol], [FechaRegistro], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'3', N'Miguel', N'Ruiz', N'C/ Calle, 11', N'miguel.ruiz@example.com', N'634567890', 1, N'01/01/2025', N'miguel.ruiz', N'MIGUEL.RUIZ', N'miguel.ruiz@example.com', N'MIGUEL.RUIZ@EXAMPLE.COM', 1, NULL, N'619AD214-172D-43D8-97A5-3652A8F95AE2', N'42F93B85-47B2-473E-9219-35B7DA93E779', N'634567890', 1, 0, NULL, 0, 0)
INSERT INTO [dbo].[AspNetUsers] ([Id], [Nombre], [Apellidos], [DireccionEnvio], [CorreoElectronico], [NumTelefono], [Rol], [FechaRegistro], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'4', N'Jose', N'Fernandez', N'C/ Plaza, 21', N'jose.fernandez@example.com', N'645678901', 2, N'04/04/2025', N'jose.fernandez', N'JOSE.FERNANDEZ', N'jose.fernandez@example.com', N'JOSE.FERNANDEZ@EXAMPLE.COM', 1, NULL, N'C07A688A-7B8C-44BC-BADA-E843818441E3', N'463E2FC0-5363-400D-BDEF-33140BEB0010', N'645678901', 1, 0, NULL, 0, 0)
INSERT INTO [dbo].[AspNetUsers] ([Id], [Nombre], [Apellidos], [DireccionEnvio], [CorreoElectronico], [NumTelefono], [Rol], [FechaRegistro], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'5', N'Luis', N'Lopez', N'C/ Avenida, 9', N'luis.lopez@example.com', N'656789012', 2, N'05/05/2025', N'luis.lopez', N'LUIS.LOPEZ', N'luis.lopez@example.com', N'LUIS.LOPEZ@EXAMPLE.COM', 1, NULL, N'7286B66E-DF84-4C88-B617-CA366B16D997', N'6C2A4851-ACBF-47A3-8B65-3FB7609596C4', N'656789012', 1, 0, NULL, 0, 0)

SET IDENTITY_INSERT [dbo].[Compras] ON
INSERT INTO [dbo].[Compras] ([Id], [FechaCompra], [PrecioTotal], [MetodoPago], [ApplicationUserId]) VALUES (1, N'01/09/2025', CAST(10.99 AS Decimal(10, 2)), 1, 3)
SET IDENTITY_INSERT [dbo].[Compras] OFF

INSERT INTO [dbo].[CompraItems] ([CompraId], [HerramientaId], [Cantidad], [Descripcion], [Precio]) VALUES (1, 2, 1, N'Herramienta de calidad', CAST(11.00 AS Decimal(10, 2)))

SET IDENTITY_INSERT [dbo].[Reparaciones] ON
INSERT INTO [dbo].[Reparaciones] ([Id], [FechaEntrega], [FechaRecogida], [PrecioTotal], [MetodoPago], [ApplicationUserId]) VALUES (1, N'01/10/2025', N'03/10/2025', 10.25, 1, 2)
SET IDENTITY_INSERT [dbo].[Reparaciones] OFF

INSERT INTO [dbo].[ReparacionItems] ([HerramientaId], [ReparacionId], [Precio], [Cantidad], [Descripcion]) VALUES (1, 1, 10.20, 2, N'Muy Bonito')

SET IDENTITY_INSERT [dbo].[Ofertas] ON
INSERT INTO [dbo].[Ofertas] ([Id], [FechaInicio], [FechaFinal], [FechaOferta], [MetodoPago], [DirigidaA]) VALUES (1, N'01/09/2025', N'02/09/2025', N'01/09/2025', 1, 2)
SET IDENTITY_INSERT [dbo].[Ofertas] OFF

INSERT INTO [dbo].[OfertaItems] ([HerramientaId], [OfertaId], [Porcentaje], [PrecioFinal]) VALUES (1, 1, 10, CAST(11.25 AS Decimal(10, 2)))