/******** Add you seeeding script here ********/

INSERT [dbo].[Pagamentos] ([Id], [PedidoId], [Status], [Valor], [NomeCartao], [NumeroCartao], [ExpiracaoCartao], [CvvCartao]) VALUES (N'243cd63a-b25c-4cb5-9ed0-09ebbd8a73ae', N'19d477ed-bcdc-41b2-a07b-7d51dece059b', NULL, CAST(336.00 AS Decimal(18, 2)), N'Alexander V Silva', N'5294528612799100', N'04/25', N'298')
INSERT [dbo].[Pagamentos] ([Id], [PedidoId], [Status], [Valor], [NomeCartao], [NumeroCartao], [ExpiracaoCartao], [CvvCartao]) VALUES (N'6c9c788b-0630-4aeb-9aac-616f69597fd0', N'bb334951-5101-4831-9a0e-a8354a7851af', NULL, CAST(90.00 AS Decimal(18, 2)), N'Marcos Oliveira da Silva', N'5102332074472526', N'09/09/2024', N'770')
INSERT [dbo].[Pagamentos] ([Id], [PedidoId], [Status], [Valor], [NomeCartao], [NumeroCartao], [ExpiracaoCartao], [CvvCartao]) VALUES (N'0a893e80-351a-4165-997e-bf8231e076d6', N'5f06ddba-5335-432a-a185-e473207a62b9', NULL, CAST(227.00 AS Decimal(18, 2)), N'Alexander V Silva', N'5457928917977042', N'28/02/2026', N'110')
INSERT [dbo].[Pagamentos] ([Id], [PedidoId], [Status], [Valor], [NomeCartao], [NumeroCartao], [ExpiracaoCartao], [CvvCartao]) VALUES (N'731a7c04-3091-4a31-b633-e6756c27497f', N'36c85d72-6049-4253-b37e-9ae1bd0006f5', NULL, CAST(118.00 AS Decimal(18, 2)), N'Alexander V Silva', N'4716354331918100', N'04/2025', N'110')

INSERT [dbo].[Transacoes] ([Id], [PedidoId], [PagamentoId], [Total], [StatusTransacao]) VALUES (N'edf057ab-edb8-41a9-a24b-1687f1c9c059', N'bb334951-5101-4831-9a0e-a8354a7851af', N'6c9c788b-0630-4aeb-9aac-616f69597fd0', CAST(90.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[Transacoes] ([Id], [PedidoId], [PagamentoId], [Total], [StatusTransacao]) VALUES (N'852c1bff-743e-47a2-bd0b-2781b2529b17', N'5f06ddba-5335-432a-a185-e473207a62b9', N'0a893e80-351a-4165-997e-bf8231e076d6', CAST(227.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[Transacoes] ([Id], [PedidoId], [PagamentoId], [Total], [StatusTransacao]) VALUES (N'5711039d-5af6-40ab-9559-28ec490e1f62', N'19d477ed-bcdc-41b2-a07b-7d51dece059b', N'243cd63a-b25c-4cb5-9ed0-09ebbd8a73ae', CAST(336.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[Transacoes] ([Id], [PedidoId], [PagamentoId], [Total], [StatusTransacao]) VALUES (N'cf930f55-0a95-45b3-9028-6e26d3796382', N'36c85d72-6049-4253-b37e-9ae1bd0006f5', N'731a7c04-3091-4a31-b633-e6756c27497f', CAST(118.00 AS Decimal(18, 2)), 1)

/******* Comment out or remove following line once you add your script ********/
--RAISERROR ('No seeding script implemented', 16,1)