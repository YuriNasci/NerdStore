/******** Add you seeeding script here ********/

INSERT [dbo].[Categorias] ([Id], [Nome], [Codigo]) VALUES (N'9e099831-7828-4661-a2b8-9bb6fcbc1d28', N'Canecas', 101)
INSERT [dbo].[Categorias] ([Id], [Nome], [Codigo]) VALUES (N'6b4cfaab-c0d3-4cfa-a086-a9c371efa66f', N'Adesivos', 102)
INSERT [dbo].[Categorias] ([Id], [Nome], [Codigo]) VALUES (N'dcd70fac-e8ac-4af3-9a62-fec7ba52f972', N'Camisas', 100)

INSERT [dbo].[Produtos] ([Id], [CategoriaId], [Nome], [Descricao], [Ativo], [Valor], [DataCadastro], [Imagem], [QuantidadeEstoque], [Altura], [Largura], [Profundidade]) VALUES (N'e10926e6-0e1d-4a86-0e07-08dc36650581', N'dcd70fac-e8ac-4af3-9a62-fec7ba52f972', N'Camiseta Developer', N'Camiseta 100% algodão', 1, CAST(69.00 AS Decimal(18, 2)), CAST(N'2024-02-25T21:51:16.6102960' AS DateTime2), N'Camiseta1.jpg', 10, 5, 5, 5)
INSERT [dbo].[Produtos] ([Id], [CategoriaId], [Nome], [Descricao], [Ativo], [Valor], [DataCadastro], [Imagem], [QuantidadeEstoque], [Altura], [Largura], [Profundidade]) VALUES (N'9ea9a530-cac6-4cb3-0658-08dc366bd945', N'9e099831-7828-4661-a2b8-9bb6fcbc1d28', N'Caneca StarBugs', N'Caneca personalizada', 1, CAST(59.00 AS Decimal(18, 2)), CAST(N'2024-02-25T22:40:01.6474797' AS DateTime2), N'caneca1.jpg', 7, 5, 5, 5)
INSERT [dbo].[Produtos] ([Id], [CategoriaId], [Nome], [Descricao], [Ativo], [Valor], [DataCadastro], [Imagem], [QuantidadeEstoque], [Altura], [Largura], [Profundidade]) VALUES (N'9a8c1c47-3b6f-4cc2-9ccf-58069a4d9f90', N'dcd70fac-e8ac-4af3-9a62-fec7ba52f972', N'Camiseta Code', N'Camiseta 100% algodão', 1, CAST(99.00 AS Decimal(18, 2)), CAST(N'2019-07-19T00:00:00.0000000' AS DateTime2), N'camiseta2.jpg', 0, 5, 5, 5)


/******* Comment out or remove following line once you add your script ********/
-- ('No seeding script implemented', 16,1)