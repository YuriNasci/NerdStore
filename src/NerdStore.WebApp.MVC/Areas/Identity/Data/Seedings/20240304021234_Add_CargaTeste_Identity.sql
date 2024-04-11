/******** Add you seeeding script here ********/

INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'17c6c007-ad05-455f-903e-edbfff4b856b', N'usuario@gmail.com', N'USUARIO@GMAIL.COM', N'usuario@gmail.com', N'USUARIO@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEGLeF3dFPXISZAhjm5XwH8uKObnMItyg+mRjEfVQwfJmFuR++EgM+CD9AuEaPlu5jw==', N'6a93d2ca-118b-49b9-a10d-81eb17c803f8', N'c5843891-5b88-4591-85e8-b63385c890bc', N'21970696089', 0, 0, NULL, 0, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'b74ddd14-6340-4840-95c2-db12554843e5', N'admin@gmail.com', N'ADMIN@GMAIL.COM', N'admin@gmail.com', N'ADMIN@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEH8UUNb+HLzEtIWREp+8NafRDbyyv5M0SEWuFvmXMrVKxPacVplBtsA2pn6dscBfPQ==', N'542f8c34-9a13-4757-92fe-e464ef8ee2a6', N'7ada8207-5a72-4e26-ad6a-99fdd93352df', N'21970851350', 0, 0, NULL, 0, 0)

SET IDENTITY_INSERT [dbo].[AspNetUserClaims] ON 

INSERT [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (18, N'b74ddd14-6340-4840-95c2-db12554843e5', N'Produto', N'Adicionar')
INSERT [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (19, N'b74ddd14-6340-4840-95c2-db12554843e5', N'Produto', N'Editar')
INSERT [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (20, N'b74ddd14-6340-4840-95c2-db12554843e5', N'Produto', N'Excluir')
INSERT [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (21, N'b74ddd14-6340-4840-95c2-db12554843e5', N'Produto', N'Leitura')
INSERT [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (22, N'17c6c007-ad05-455f-903e-edbfff4b856b', N'Produto', N'Leitura')
SET IDENTITY_INSERT [dbo].[AspNetUserClaims] OFF

/******* Comment out or remove following line once you add your script ********/
--RAISERROR ('No seeding script implemented', 16,1)