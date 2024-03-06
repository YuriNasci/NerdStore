USE [master]
GO
/****** Object:  Database [NerdStoreDb]    Script Date: 05/03/2024 22:23:46 ******/
CREATE DATABASE [NerdStoreDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'NerdStoreDb', FILENAME = N'C:\Users\Alexander\NerdStoreDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'NerdStoreDb_log', FILENAME = N'C:\Users\Alexander\NerdStoreDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [NerdStoreDb] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [NerdStoreDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [NerdStoreDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [NerdStoreDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [NerdStoreDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [NerdStoreDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [NerdStoreDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [NerdStoreDb] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [NerdStoreDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [NerdStoreDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [NerdStoreDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [NerdStoreDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [NerdStoreDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [NerdStoreDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [NerdStoreDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [NerdStoreDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [NerdStoreDb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [NerdStoreDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [NerdStoreDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [NerdStoreDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [NerdStoreDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [NerdStoreDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [NerdStoreDb] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [NerdStoreDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [NerdStoreDb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [NerdStoreDb] SET  MULTI_USER 
GO
ALTER DATABASE [NerdStoreDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [NerdStoreDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [NerdStoreDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [NerdStoreDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [NerdStoreDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [NerdStoreDb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [NerdStoreDb] SET QUERY_STORE = OFF
GO
USE [NerdStoreDb]
GO
USE [NerdStoreDb]
GO
/****** Object:  Sequence [dbo].[MinhaSequencia]    Script Date: 05/03/2024 22:23:46 ******/
CREATE SEQUENCE [dbo].[MinhaSequencia] 
 AS [int]
 START WITH 1000
 INCREMENT BY 1
 MINVALUE -2147483648
 MAXVALUE 2147483647
 CACHE 
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 05/03/2024 22:23:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[__SeedingHistory]    Script Date: 05/03/2024 22:23:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__SeedingHistory](
	[Name] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK___SeedingHistory] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 05/03/2024 22:23:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 05/03/2024 22:23:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 05/03/2024 22:23:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 05/03/2024 22:23:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 05/03/2024 22:23:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 05/03/2024 22:23:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 05/03/2024 22:23:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categorias]    Script Date: 05/03/2024 22:23:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categorias](
	[Id] [uniqueidentifier] NOT NULL,
	[Nome] [varchar](250) NOT NULL,
	[Codigo] [int] NOT NULL,
 CONSTRAINT [PK_Categorias] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pagamentos]    Script Date: 05/03/2024 22:23:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pagamentos](
	[Id] [uniqueidentifier] NOT NULL,
	[PedidoId] [uniqueidentifier] NOT NULL,
	[Status] [varchar](100) NULL,
	[Valor] [decimal](18, 2) NOT NULL,
	[NomeCartao] [varchar](250) NOT NULL,
	[NumeroCartao] [varchar](16) NOT NULL,
	[ExpiracaoCartao] [varchar](10) NOT NULL,
	[CvvCartao] [varchar](4) NOT NULL,
 CONSTRAINT [PK_Pagamentos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PedidoItems]    Script Date: 05/03/2024 22:23:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PedidoItems](
	[Id] [uniqueidentifier] NOT NULL,
	[PedidoId] [uniqueidentifier] NOT NULL,
	[ProdutoId] [uniqueidentifier] NOT NULL,
	[ProdutoNome] [varchar](250) NOT NULL,
	[Quantidade] [int] NOT NULL,
	[ValorUnitario] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_PedidoItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pedidos]    Script Date: 05/03/2024 22:23:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pedidos](
	[Id] [uniqueidentifier] NOT NULL,
	[Codigo] [int] NOT NULL,
	[ClienteId] [uniqueidentifier] NOT NULL,
	[VoucherId] [uniqueidentifier] NULL,
	[VoucherUtilizado] [bit] NOT NULL,
	[Desconto] [decimal](18, 2) NOT NULL,
	[ValorTotal] [decimal](18, 2) NOT NULL,
	[DataCadastro] [datetime2](7) NOT NULL,
	[PedidoStatus] [int] NOT NULL,
 CONSTRAINT [PK_Pedidos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Produtos]    Script Date: 05/03/2024 22:23:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Produtos](
	[Id] [uniqueidentifier] NOT NULL,
	[CategoriaId] [uniqueidentifier] NOT NULL,
	[Nome] [varchar](250) NOT NULL,
	[Descricao] [varchar](500) NOT NULL,
	[Ativo] [bit] NOT NULL,
	[Valor] [decimal](18, 2) NOT NULL,
	[DataCadastro] [datetime2](7) NOT NULL,
	[Imagem] [varchar](250) NOT NULL,
	[QuantidadeEstoque] [int] NOT NULL,
	[Altura] [int] NOT NULL,
	[Largura] [int] NOT NULL,
	[Profundidade] [int] NOT NULL,
 CONSTRAINT [PK_Produtos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transacoes]    Script Date: 05/03/2024 22:23:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transacoes](
	[Id] [uniqueidentifier] NOT NULL,
	[PedidoId] [uniqueidentifier] NOT NULL,
	[PagamentoId] [uniqueidentifier] NOT NULL,
	[Total] [decimal](18, 2) NOT NULL,
	[StatusTransacao] [int] NOT NULL,
 CONSTRAINT [PK_Transacoes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vouchers]    Script Date: 05/03/2024 22:23:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vouchers](
	[Id] [uniqueidentifier] NOT NULL,
	[Codigo] [varchar](100) NOT NULL,
	[Percentual] [decimal](18, 2) NULL,
	[ValorDesconto] [decimal](18, 2) NULL,
	[Quantidade] [int] NOT NULL,
	[TipoDescontoVoucher] [int] NOT NULL,
	[DataCriacao] [datetime2](7) NOT NULL,
	[DataUtilizacao] [datetime2](7) NULL,
	[DataValidade] [datetime2](7) NOT NULL,
	[Ativo] [bit] NOT NULL,
	[Utilizado] [bit] NOT NULL,
 CONSTRAINT [PK_Vouchers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190724003649_Initial', N'5.0.17')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190808211831_Pagamentos', N'5.0.17')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190812212256_Pedidos', N'5.0.17')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20201016183704_Add_Seeding_Tracking', N'5.0.12')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230605201820_Identity', N'5.0.17')
GO
INSERT [dbo].[__SeedingHistory] ([Name]) VALUES (N'20240301175200_Add_CargaTeste_Catalogo.sql')
GO
SET IDENTITY_INSERT [dbo].[AspNetUserClaims] ON 

INSERT [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (18, N'b74ddd14-6340-4840-95c2-db12554843e5', N'Produto', N'Adicionar')
INSERT [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (19, N'b74ddd14-6340-4840-95c2-db12554843e5', N'Produto', N'Editar')
INSERT [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (20, N'b74ddd14-6340-4840-95c2-db12554843e5', N'Produto', N'Excluir')
INSERT [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (21, N'b74ddd14-6340-4840-95c2-db12554843e5', N'Produto', N'Leitura')
INSERT [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (22, N'17c6c007-ad05-455f-903e-edbfff4b856b', N'Produto', N'Leitura')
SET IDENTITY_INSERT [dbo].[AspNetUserClaims] OFF
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'17c6c007-ad05-455f-903e-edbfff4b856b', N'usuario@gmail.com', N'USUARIO@GMAIL.COM', N'usuario@gmail.com', N'USUARIO@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEGLeF3dFPXISZAhjm5XwH8uKObnMItyg+mRjEfVQwfJmFuR++EgM+CD9AuEaPlu5jw==', N'6a93d2ca-118b-49b9-a10d-81eb17c803f8', N'c5843891-5b88-4591-85e8-b63385c890bc', N'21970696089', 0, 0, NULL, 0, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'b74ddd14-6340-4840-95c2-db12554843e5', N'admin@gmail.com', N'ADMIN@GMAIL.COM', N'admin@gmail.com', N'ADMIN@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEH8UUNb+HLzEtIWREp+8NafRDbyyv5M0SEWuFvmXMrVKxPacVplBtsA2pn6dscBfPQ==', N'542f8c34-9a13-4757-92fe-e464ef8ee2a6', N'7ada8207-5a72-4e26-ad6a-99fdd93352df', N'21970851350', 0, 0, NULL, 0, 0)
GO
INSERT [dbo].[Categorias] ([Id], [Nome], [Codigo]) VALUES (N'b18f1b37-a19e-469a-a5c1-64f42aac4ed2', N'Adesivos', 102)
INSERT [dbo].[Categorias] ([Id], [Nome], [Codigo]) VALUES (N'3f24e628-4d4d-4fd4-836f-74260db1033b', N'Bones', 103)
INSERT [dbo].[Categorias] ([Id], [Nome], [Codigo]) VALUES (N'3e0f3705-05a6-452b-9346-781330b9eee0', N'Camisas', 100)
INSERT [dbo].[Categorias] ([Id], [Nome], [Codigo]) VALUES (N'88fa66ef-4a37-4f65-b54c-7b235945dc2d', N'Canecas', 101)
INSERT [dbo].[Categorias] ([Id], [Nome], [Codigo]) VALUES (N'd0dff668-4ff0-4fe9-a92d-bacbd393c234', N'Iphone', 105)
INSERT [dbo].[Categorias] ([Id], [Nome], [Codigo]) VALUES (N'c6a17573-9e7f-43db-8943-fba457fc0a3c', N'Smartphone', 104)
GO
INSERT [dbo].[Pagamentos] ([Id], [PedidoId], [Status], [Valor], [NomeCartao], [NumeroCartao], [ExpiracaoCartao], [CvvCartao]) VALUES (N'243cd63a-b25c-4cb5-9ed0-09ebbd8a73ae', N'19d477ed-bcdc-41b2-a07b-7d51dece059b', NULL, CAST(336.00 AS Decimal(18, 2)), N'Alexander V Silva', N'5294528612799100', N'04/25', N'298')
INSERT [dbo].[Pagamentos] ([Id], [PedidoId], [Status], [Valor], [NomeCartao], [NumeroCartao], [ExpiracaoCartao], [CvvCartao]) VALUES (N'6c9c788b-0630-4aeb-9aac-616f69597fd0', N'bb334951-5101-4831-9a0e-a8354a7851af', NULL, CAST(90.00 AS Decimal(18, 2)), N'Marcos Oliveira da Silva', N'5102332074472526', N'09/09/2024', N'770')
INSERT [dbo].[Pagamentos] ([Id], [PedidoId], [Status], [Valor], [NomeCartao], [NumeroCartao], [ExpiracaoCartao], [CvvCartao]) VALUES (N'0a893e80-351a-4165-997e-bf8231e076d6', N'5f06ddba-5335-432a-a185-e473207a62b9', NULL, CAST(227.00 AS Decimal(18, 2)), N'Alexander V Silva', N'5457928917977042', N'28/02/2026', N'110')
INSERT [dbo].[Pagamentos] ([Id], [PedidoId], [Status], [Valor], [NomeCartao], [NumeroCartao], [ExpiracaoCartao], [CvvCartao]) VALUES (N'731a7c04-3091-4a31-b633-e6756c27497f', N'36c85d72-6049-4253-b37e-9ae1bd0006f5', NULL, CAST(118.00 AS Decimal(18, 2)), N'Alexander V Silva', N'4716354331918100', N'04/2025', N'110')
GO
INSERT [dbo].[PedidoItems] ([Id], [PedidoId], [ProdutoId], [ProdutoNome], [Quantidade], [ValorUnitario]) VALUES (N'8502d318-9929-4efa-abda-173fe591324e', N'19d477ed-bcdc-41b2-a07b-7d51dece059b', N'9a8c1c47-3b6f-4cc2-9ccf-58069a4d9f90', N'Camiseta Code', 2, CAST(99.00 AS Decimal(18, 2)))
INSERT [dbo].[PedidoItems] ([Id], [PedidoId], [ProdutoId], [ProdutoNome], [Quantidade], [ValorUnitario]) VALUES (N'def05582-5faf-4edd-b789-323501785e55', N'b10421d9-9bc8-454f-909f-8dcd5a577d86', N'e10926e6-0e1d-4a86-0e07-08dc36650581', N'Camiseta Developer', 1, CAST(69.00 AS Decimal(18, 2)))
INSERT [dbo].[PedidoItems] ([Id], [PedidoId], [ProdutoId], [ProdutoNome], [Quantidade], [ValorUnitario]) VALUES (N'cbd8ee4e-e66f-499f-9d26-3a1216ecbf37', N'5f06ddba-5335-432a-a185-e473207a62b9', N'9ea9a530-cac6-4cb3-0658-08dc366bd945', N'Caneca StarBugs', 1, CAST(59.00 AS Decimal(18, 2)))
INSERT [dbo].[PedidoItems] ([Id], [PedidoId], [ProdutoId], [ProdutoNome], [Quantidade], [ValorUnitario]) VALUES (N'88ca8e43-0b16-4d20-b6c0-433d0c5409a8', N'36c85d72-6049-4253-b37e-9ae1bd0006f5', N'9ea9a530-cac6-4cb3-0658-08dc366bd945', N'Caneca StarBugs', 2, CAST(59.00 AS Decimal(18, 2)))
INSERT [dbo].[PedidoItems] ([Id], [PedidoId], [ProdutoId], [ProdutoNome], [Quantidade], [ValorUnitario]) VALUES (N'737a9061-cc20-4f22-93e7-50a2ce2df610', N'7dc0f341-9839-4cca-bf55-6a8876d984fd', N'e10926e6-0e1d-4a86-0e07-08dc36650581', N'Camiseta Developer', 2, CAST(69.00 AS Decimal(18, 2)))
INSERT [dbo].[PedidoItems] ([Id], [PedidoId], [ProdutoId], [ProdutoNome], [Quantidade], [ValorUnitario]) VALUES (N'cff0679c-f833-44c8-9e8f-829a3ebc5ca5', N'5f06ddba-5335-432a-a185-e473207a62b9', N'e10926e6-0e1d-4a86-0e07-08dc36650581', N'Camiseta Developer', 1, CAST(69.00 AS Decimal(18, 2)))
INSERT [dbo].[PedidoItems] ([Id], [PedidoId], [ProdutoId], [ProdutoNome], [Quantidade], [ValorUnitario]) VALUES (N'9e28229c-dc05-4c11-8609-97a54b9fd323', N'bb334951-5101-4831-9a0e-a8354a7851af', N'9a8c1c47-3b6f-4cc2-9ccf-58069a4d9f90', N'Camiseta Code', 1, CAST(90.00 AS Decimal(18, 2)))
INSERT [dbo].[PedidoItems] ([Id], [PedidoId], [ProdutoId], [ProdutoNome], [Quantidade], [ValorUnitario]) VALUES (N'01441093-6d05-4fb2-89cf-b56efbfc5e59', N'c865d64e-3118-4f19-8b98-36b6c604b22e', N'731380bf-e48d-4601-87e4-031345aa2edc', N'Samsung Galaxy S4*', 1, CAST(1199.00 AS Decimal(18, 2)))
INSERT [dbo].[PedidoItems] ([Id], [PedidoId], [ProdutoId], [ProdutoNome], [Quantidade], [ValorUnitario]) VALUES (N'c4a974fe-dc52-4f20-af3c-bf8779573617', N'792df984-9287-4459-b051-b89d0f8c2814', N'9a8c1c47-3b6f-4cc2-9ccf-58069a4d9f90', N'Camiseta Code', 1, CAST(90.00 AS Decimal(18, 2)))
INSERT [dbo].[PedidoItems] ([Id], [PedidoId], [ProdutoId], [ProdutoNome], [Quantidade], [ValorUnitario]) VALUES (N'e7321bcf-47f5-4491-bca4-cdf837a8b954', N'19d477ed-bcdc-41b2-a07b-7d51dece059b', N'e10926e6-0e1d-4a86-0e07-08dc36650581', N'Camiseta Developer', 2, CAST(69.00 AS Decimal(18, 2)))
INSERT [dbo].[PedidoItems] ([Id], [PedidoId], [ProdutoId], [ProdutoNome], [Quantidade], [ValorUnitario]) VALUES (N'2c8f8632-6852-4f2d-8b7e-d37b09adfa90', N'5f06ddba-5335-432a-a185-e473207a62b9', N'9a8c1c47-3b6f-4cc2-9ccf-58069a4d9f90', N'Camiseta Code', 1, CAST(99.00 AS Decimal(18, 2)))
INSERT [dbo].[PedidoItems] ([Id], [PedidoId], [ProdutoId], [ProdutoNome], [Quantidade], [ValorUnitario]) VALUES (N'6aa1ea34-d94b-467a-9d77-ece326b37ebf', N'b10421d9-9bc8-454f-909f-8dcd5a577d86', N'9ea9a530-cac6-4cb3-0658-08dc366bd945', N'Caneca StarBugs', 2, CAST(59.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Pedidos] ([Id], [Codigo], [ClienteId], [VoucherId], [VoucherUtilizado], [Desconto], [ValorTotal], [DataCadastro], [PedidoStatus]) VALUES (N'c865d64e-3118-4f19-8b98-36b6c604b22e', 1008, N'4885e451-b0e4-4490-b959-04fabc806d32', NULL, 0, CAST(0.00 AS Decimal(18, 2)), CAST(1199.00 AS Decimal(18, 2)), CAST(N'2024-03-05T00:44:11.0599043' AS DateTime2), 0)
INSERT [dbo].[Pedidos] ([Id], [Codigo], [ClienteId], [VoucherId], [VoucherUtilizado], [Desconto], [ValorTotal], [DataCadastro], [PedidoStatus]) VALUES (N'7dc0f341-9839-4cca-bf55-6a8876d984fd', 1005, N'4885e451-b0e4-4490-b959-04fabc806d32', NULL, 0, CAST(0.00 AS Decimal(18, 2)), CAST(256.00 AS Decimal(18, 2)), CAST(N'2024-02-27T23:44:26.6444759' AS DateTime2), 1)
INSERT [dbo].[Pedidos] ([Id], [Codigo], [ClienteId], [VoucherId], [VoucherUtilizado], [Desconto], [ValorTotal], [DataCadastro], [PedidoStatus]) VALUES (N'19d477ed-bcdc-41b2-a07b-7d51dece059b', 1002, N'4885e451-b0e4-4490-b959-04fabc806d32', NULL, 0, CAST(0.00 AS Decimal(18, 2)), CAST(336.00 AS Decimal(18, 2)), CAST(N'2023-06-09T20:50:39.1552461' AS DateTime2), 4)
INSERT [dbo].[Pedidos] ([Id], [Codigo], [ClienteId], [VoucherId], [VoucherUtilizado], [Desconto], [ValorTotal], [DataCadastro], [PedidoStatus]) VALUES (N'b10421d9-9bc8-454f-909f-8dcd5a577d86', 1003, N'4885e451-b0e4-4490-b959-04fabc806d32', NULL, 0, CAST(0.00 AS Decimal(18, 2)), CAST(187.00 AS Decimal(18, 2)), CAST(N'2024-02-27T23:22:05.6407983' AS DateTime2), 1)
INSERT [dbo].[Pedidos] ([Id], [Codigo], [ClienteId], [VoucherId], [VoucherUtilizado], [Desconto], [ValorTotal], [DataCadastro], [PedidoStatus]) VALUES (N'36c85d72-6049-4253-b37e-9ae1bd0006f5', 1006, N'4885e451-b0e4-4490-b959-04fabc806d32', NULL, 0, CAST(0.00 AS Decimal(18, 2)), CAST(118.00 AS Decimal(18, 2)), CAST(N'2024-02-28T02:32:32.4777009' AS DateTime2), 4)
INSERT [dbo].[Pedidos] ([Id], [Codigo], [ClienteId], [VoucherId], [VoucherUtilizado], [Desconto], [ValorTotal], [DataCadastro], [PedidoStatus]) VALUES (N'bb334951-5101-4831-9a0e-a8354a7851af', 1001, N'4885e451-b0e4-4490-b959-04fabc806d32', NULL, 0, CAST(0.00 AS Decimal(18, 2)), CAST(90.00 AS Decimal(18, 2)), CAST(N'2023-06-09T19:18:00.9155992' AS DateTime2), 4)
INSERT [dbo].[Pedidos] ([Id], [Codigo], [ClienteId], [VoucherId], [VoucherUtilizado], [Desconto], [ValorTotal], [DataCadastro], [PedidoStatus]) VALUES (N'792df984-9287-4459-b051-b89d0f8c2814', 1000, N'4885e451-b0e4-4490-b959-04fabc806d32', NULL, 0, CAST(0.00 AS Decimal(18, 2)), CAST(90.00 AS Decimal(18, 2)), CAST(N'2023-06-06T09:53:45.9937065' AS DateTime2), 1)
INSERT [dbo].[Pedidos] ([Id], [Codigo], [ClienteId], [VoucherId], [VoucherUtilizado], [Desconto], [ValorTotal], [DataCadastro], [PedidoStatus]) VALUES (N'5f06ddba-5335-432a-a185-e473207a62b9', 1004, N'4885e451-b0e4-4490-b959-04fabc806d32', NULL, 0, CAST(0.00 AS Decimal(18, 2)), CAST(227.00 AS Decimal(18, 2)), CAST(N'2024-02-27T23:28:04.6145612' AS DateTime2), 4)
GO
INSERT [dbo].[Produtos] ([Id], [CategoriaId], [Nome], [Descricao], [Ativo], [Valor], [DataCadastro], [Imagem], [QuantidadeEstoque], [Altura], [Largura], [Profundidade]) VALUES (N'f836df52-fbfa-42be-a45f-019320572ba0', N'd0dff668-4ff0-4fe9-a92d-bacbd393c234', N'IPhone*', N'Aliquam erat volutpat *', 1, CAST(1998.00 AS Decimal(18, 2)), CAST(N'2024-03-04T23:59:19.3277662' AS DateTime2), N'iphone.png', 20, 5, 5, 5)
INSERT [dbo].[Produtos] ([Id], [CategoriaId], [Nome], [Descricao], [Ativo], [Valor], [DataCadastro], [Imagem], [QuantidadeEstoque], [Altura], [Largura], [Profundidade]) VALUES (N'731380bf-e48d-4601-87e4-031345aa2edc', N'c6a17573-9e7f-43db-8943-fba457fc0a3c', N'Samsung Galaxy S4*', N'Aliquam erat volutpat *', 1, CAST(1199.00 AS Decimal(18, 2)), CAST(N'2024-03-04T23:59:19.3487761' AS DateTime2), N'galaxy-s4.jpg', 20, 5, 5, 5)
INSERT [dbo].[Produtos] ([Id], [CategoriaId], [Nome], [Descricao], [Ativo], [Valor], [DataCadastro], [Imagem], [QuantidadeEstoque], [Altura], [Largura], [Profundidade]) VALUES (N'fcd9c1ef-6ac2-46a9-a09f-176adda22fc6', N'c6a17573-9e7f-43db-8943-fba457fc0a3c', N'Z1*', N'Aliquam erat volutpat *', 1, CAST(1389.00 AS Decimal(18, 2)), CAST(N'2024-03-04T23:59:19.3879958' AS DateTime2), N'Z1.png', 0, 5, 5, 5)
INSERT [dbo].[Produtos] ([Id], [CategoriaId], [Nome], [Descricao], [Ativo], [Valor], [DataCadastro], [Imagem], [QuantidadeEstoque], [Altura], [Largura], [Profundidade]) VALUES (N'baee72ba-275a-475f-9653-20aaf46ff7d8', N'c6a17573-9e7f-43db-8943-fba457fc0a3c', N'Samsung Galaxy S4', N'Aliquam erat volutpat', 1, CAST(989.00 AS Decimal(18, 2)), CAST(N'2024-03-04T23:59:22.2627261' AS DateTime2), N'galaxy-s4.jpg', 20, 5, 5, 5)
INSERT [dbo].[Produtos] ([Id], [CategoriaId], [Nome], [Descricao], [Ativo], [Valor], [DataCadastro], [Imagem], [QuantidadeEstoque], [Altura], [Largura], [Profundidade]) VALUES (N'ccbbb2e8-19d0-4334-bfd9-3452f4b6323d', N'3e0f3705-05a6-452b-9346-781330b9eee0', N'Camiseta Code', N'Camiseta 100% algodão', 1, CAST(89.00 AS Decimal(18, 2)), CAST(N'2024-03-04T23:59:12.0215555' AS DateTime2), N'camiseta2.jpg', 0, 5, 5, 5)
INSERT [dbo].[Produtos] ([Id], [CategoriaId], [Nome], [Descricao], [Ativo], [Valor], [DataCadastro], [Imagem], [QuantidadeEstoque], [Altura], [Largura], [Profundidade]) VALUES (N'd9a67134-4c38-41e2-b78f-4f462a312db3', N'88fa66ef-4a37-4f65-b54c-7b235945dc2d', N'Caneca StarBugs', N'Aliquam erat volutpat', 1, CAST(49.00 AS Decimal(18, 2)), CAST(N'2024-03-04T23:59:12.0417492' AS DateTime2), N'caneca1.jpg', 0, 5, 5, 5)
INSERT [dbo].[Produtos] ([Id], [CategoriaId], [Nome], [Descricao], [Ativo], [Valor], [DataCadastro], [Imagem], [QuantidadeEstoque], [Altura], [Largura], [Profundidade]) VALUES (N'0255161b-62b3-4193-9649-59abd5f36b38', N'd0dff668-4ff0-4fe9-a92d-bacbd393c234', N'IPhone', N'Aliquam erat volutpat', 1, CAST(2998.00 AS Decimal(18, 2)), CAST(N'2024-03-04T23:59:22.2392842' AS DateTime2), N'iphone.png', 0, 5, 5, 5)
INSERT [dbo].[Produtos] ([Id], [CategoriaId], [Nome], [Descricao], [Ativo], [Valor], [DataCadastro], [Imagem], [QuantidadeEstoque], [Altura], [Largura], [Profundidade]) VALUES (N'763cdb88-38f9-4c39-92ef-65fe78355c79', N'c6a17573-9e7f-43db-8943-fba457fc0a3c', N'Samsung Galaxy Note', N'Aliquam erat volutpat', 1, CAST(1179.00 AS Decimal(18, 2)), CAST(N'2024-03-04T23:59:22.2897152' AS DateTime2), N'galaxy-note.jpg', 0, 5, 5, 5)
INSERT [dbo].[Produtos] ([Id], [CategoriaId], [Nome], [Descricao], [Ativo], [Valor], [DataCadastro], [Imagem], [QuantidadeEstoque], [Altura], [Largura], [Profundidade]) VALUES (N'a1a1d175-2b2d-4082-b6b0-8ab4b983de1f', N'3e0f3705-05a6-452b-9346-781330b9eee0', N'Camiseta Developer', N'Camiseta 100% algodão', 1, CAST(99.00 AS Decimal(18, 2)), CAST(N'2024-03-04T23:59:11.9881543' AS DateTime2), N'Camiseta1.jpg', 0, 5, 5, 5)
INSERT [dbo].[Produtos] ([Id], [CategoriaId], [Nome], [Descricao], [Ativo], [Valor], [DataCadastro], [Imagem], [QuantidadeEstoque], [Altura], [Largura], [Profundidade]) VALUES (N'61b73928-339a-476e-8deb-99497e225143', N'c6a17573-9e7f-43db-8943-fba457fc0a3c', N'Samsung Galaxy Note*', N'Aliquam erat volutpat *', 1, CAST(1289.00 AS Decimal(18, 2)), CAST(N'2024-03-04T23:59:19.3684952' AS DateTime2), N'galaxy-note.jpg', 0, 5, 5, 5)
INSERT [dbo].[Produtos] ([Id], [CategoriaId], [Nome], [Descricao], [Ativo], [Valor], [DataCadastro], [Imagem], [QuantidadeEstoque], [Altura], [Largura], [Profundidade]) VALUES (N'b9ebf18e-fdd1-4259-8eea-a32515ba8d1d', N'88fa66ef-4a37-4f65-b54c-7b235945dc2d', N'Caneca Code', N'Aliquam erat volutpat', 1, CAST(45.00 AS Decimal(18, 2)), CAST(N'2024-03-04T23:59:12.0623081' AS DateTime2), N'caneca2.jpg', 0, 5, 5, 5)
INSERT [dbo].[Produtos] ([Id], [CategoriaId], [Nome], [Descricao], [Ativo], [Valor], [DataCadastro], [Imagem], [QuantidadeEstoque], [Altura], [Largura], [Profundidade]) VALUES (N'c414b743-8e60-475c-b98b-b29616663591', N'c6a17573-9e7f-43db-8943-fba457fc0a3c', N'Z1', N'Aliquam erat volutpat', 1, CAST(1089.00 AS Decimal(18, 2)), CAST(N'2024-03-04T23:59:22.3093503' AS DateTime2), N'Z1.png', 0, 5, 5, 5)
GO
INSERT [dbo].[Transacoes] ([Id], [PedidoId], [PagamentoId], [Total], [StatusTransacao]) VALUES (N'edf057ab-edb8-41a9-a24b-1687f1c9c059', N'bb334951-5101-4831-9a0e-a8354a7851af', N'6c9c788b-0630-4aeb-9aac-616f69597fd0', CAST(90.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[Transacoes] ([Id], [PedidoId], [PagamentoId], [Total], [StatusTransacao]) VALUES (N'852c1bff-743e-47a2-bd0b-2781b2529b17', N'5f06ddba-5335-432a-a185-e473207a62b9', N'0a893e80-351a-4165-997e-bf8231e076d6', CAST(227.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[Transacoes] ([Id], [PedidoId], [PagamentoId], [Total], [StatusTransacao]) VALUES (N'5711039d-5af6-40ab-9559-28ec490e1f62', N'19d477ed-bcdc-41b2-a07b-7d51dece059b', N'243cd63a-b25c-4cb5-9ed0-09ebbd8a73ae', CAST(336.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[Transacoes] ([Id], [PedidoId], [PagamentoId], [Total], [StatusTransacao]) VALUES (N'cf930f55-0a95-45b3-9028-6e26d3796382', N'36c85d72-6049-4253-b37e-9ae1bd0006f5', N'731a7c04-3091-4a31-b633-e6756c27497f', CAST(118.00 AS Decimal(18, 2)), 1)
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 05/03/2024 22:23:46 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 05/03/2024 22:23:46 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 05/03/2024 22:23:46 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 05/03/2024 22:23:46 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 05/03/2024 22:23:46 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 05/03/2024 22:23:46 ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 05/03/2024 22:23:46 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PedidoItems_PedidoId]    Script Date: 05/03/2024 22:23:46 ******/
CREATE NONCLUSTERED INDEX [IX_PedidoItems_PedidoId] ON [dbo].[PedidoItems]
(
	[PedidoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Pedidos_VoucherId]    Script Date: 05/03/2024 22:23:46 ******/
CREATE NONCLUSTERED INDEX [IX_Pedidos_VoucherId] ON [dbo].[Pedidos]
(
	[VoucherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Produtos_CategoriaId]    Script Date: 05/03/2024 22:23:46 ******/
CREATE NONCLUSTERED INDEX [IX_Produtos_CategoriaId] ON [dbo].[Produtos]
(
	[CategoriaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Transacoes_PagamentoId]    Script Date: 05/03/2024 22:23:46 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Transacoes_PagamentoId] ON [dbo].[Transacoes]
(
	[PagamentoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Pedidos] ADD  DEFAULT (NEXT VALUE FOR [MinhaSequencia]) FOR [Codigo]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[PedidoItems]  WITH CHECK ADD  CONSTRAINT [FK_PedidoItems_Pedidos_PedidoId] FOREIGN KEY([PedidoId])
REFERENCES [dbo].[Pedidos] ([Id])
GO
ALTER TABLE [dbo].[PedidoItems] CHECK CONSTRAINT [FK_PedidoItems_Pedidos_PedidoId]
GO
ALTER TABLE [dbo].[Pedidos]  WITH CHECK ADD  CONSTRAINT [FK_Pedidos_Vouchers_VoucherId] FOREIGN KEY([VoucherId])
REFERENCES [dbo].[Vouchers] ([Id])
GO
ALTER TABLE [dbo].[Pedidos] CHECK CONSTRAINT [FK_Pedidos_Vouchers_VoucherId]
GO
ALTER TABLE [dbo].[Produtos]  WITH CHECK ADD  CONSTRAINT [FK_Produtos_Categorias_CategoriaId] FOREIGN KEY([CategoriaId])
REFERENCES [dbo].[Categorias] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Produtos] CHECK CONSTRAINT [FK_Produtos_Categorias_CategoriaId]
GO
ALTER TABLE [dbo].[Transacoes]  WITH CHECK ADD  CONSTRAINT [FK_Transacoes_Pagamentos_PagamentoId] FOREIGN KEY([PagamentoId])
REFERENCES [dbo].[Pagamentos] ([Id])
GO
ALTER TABLE [dbo].[Transacoes] CHECK CONSTRAINT [FK_Transacoes_Pagamentos_PagamentoId]
GO
USE [master]
GO
ALTER DATABASE [NerdStoreDb] SET  READ_WRITE 
GO
