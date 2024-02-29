using System;
using NerdStore.Core.DomainObjects;
using Xunit;
using FluentAssertions;
using NerdStore.Catalogo.Domain;

namespace NerdStore.Vendas.Domain.Tests
{
    public class PedidoTests
    {

        [Fact]
        public void AdicionarItem_DeveAdicionarItemAoPedido()
        {
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var item = new PedidoItem(Guid.NewGuid(), "Produto teste", 100, 2);

            pedido.AdicionarItem(item);

            pedido.PedidoItems.Should().Contain(item);
        }

        [Fact]
        public void RemoverItem_DeveRemoverItemDoPedido()
        {
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var item = new PedidoItem(Guid.NewGuid(), "Produto teste", 100, 2);
            pedido.AdicionarItem(item);

            pedido.RemoverItem(item);

            pedido.PedidoItems.Should().NotContain(item);
        }

        [Fact]
        public void AtualizarItem_DeveAtualizarItemDoPedido()
        {
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();
            var quantidadeInicial = 2;
            var valorUnitario = 100;
            var item = new PedidoItem(produtoId, "Camisa Nerd", quantidadeInicial, valorUnitario);
            pedido.AdicionarItem(item);

            var novaQuantidade = 3;
            item.AtualizarUnidades(novaQuantidade);
            pedido.AtualizarItem(item);

            pedido.PedidoItems.Should().ContainSingle(i => i.Quantidade == novaQuantidade);
        }

        [Fact]
        public void CalcularValorPedido_DeveCalcularValorTotalDoPedido()
        {
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId1 = Guid.NewGuid();
            var produtoId2 = Guid.NewGuid();
            var item1 = new PedidoItem(produtoId1, "Produto teste 1", 100, 2);
            var item2 = new PedidoItem(produtoId2, "Produto teste 2", 50, 3);
            pedido.AdicionarItem(item1);
            pedido.AdicionarItem(item2);

            pedido.CalcularValorPedido();

            pedido.ValorTotal.Should().Be(350);
        }

        [Fact]
        public void AplicarVoucher_DeveAplicarVoucherNoPedido()
        {
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(System.Guid.NewGuid());
            var voucher = new Voucher
            {
                Codigo = "COD123",
                Percentual = 50,
                ValorDesconto = null,
                Quantidade = 1,
                DataCriacao = DateTime.Now,
                DataValidade = DateTime.Now.AddDays(7),
                Ativo = true,
                Utilizado = false,
                TipoDescontoVoucher = TipoDescontoVoucher.Porcentagem
            };

            var validationResult = pedido.AplicarVoucher(voucher);

            validationResult.IsValid.Should().BeTrue();
            pedido.VoucherUtilizado.Should().BeTrue();
            pedido.Voucher.Should().Be(voucher);
        }

        [Fact]
        public void Produto_Deve_Configurar_Propriedades_Corretamente()
        {
            var nomeEsperado = "Produto Teste";
            var descricaoEsperada = "Descrição do Produto Teste"; // Certifique-se de adicionar uma descrição válida aqui
            var precoEsperado = 100.00m;

            var produto = new Produto(nomeEsperado, descricaoEsperada, true, precoEsperado, Guid.NewGuid(), DateTime.Now, "imagem.jpg", null);

            Assert.Equal(nomeEsperado, produto.Nome);
            Assert.Equal(descricaoEsperada, produto.Descricao); // Verifica se a descrição está configurada corretamente
            Assert.Equal(precoEsperado, produto.Valor);
            Assert.True(produto.Ativo);
            Assert.NotEqual(Guid.Empty, produto.CategoriaId);
            Assert.NotEqual(default(DateTime), produto.DataCadastro);
        }


        [Fact]
        public void Pedido_Deve_RemoverItemInexistenteDoPedido()
        {
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var item = new PedidoItem(Guid.NewGuid(), "Produto teste", 100, 2);


            Action act = () => pedido.RemoverItem(item);

            act.Should().Throw<DomainException>().WithMessage("O item não pertence ao pedido");
        }

        [Fact]
        public void Pedido_Deve_AtualizarUnidadesDeItemNoPedido()
        {
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();
            var produtoNome = "Produto teste";
            var quantidadeInicial = 2;
            var valorUnitario = 100;
            var item = new PedidoItem(produtoId, produtoNome, quantidadeInicial, valorUnitario);
            pedido.AdicionarItem(item);

            var novaQuantidade = 3;
            pedido.AtualizarUnidades(item, novaQuantidade);

            pedido.PedidoItems.Should().ContainSingle(i => i.Quantidade == novaQuantidade);
        }

        [Fact]
        public void Pedido_Deve_AplicarVoucherInvalidoNoPedido()
        {
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var voucher = new Voucher
            {
                Codigo = "COD123",
                Percentual = 50,
                ValorDesconto = null,
                Quantidade = 1,
                DataCriacao = DateTime.Now,
                DataValidade = DateTime.Now.AddDays(-1), // Voucher expirado
                Ativo = true,
                Utilizado = false,
                TipoDescontoVoucher = TipoDescontoVoucher.Porcentagem
            };

            var validationResult = pedido.AplicarVoucher(voucher);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage == "Este voucher está expirado.");
        }

        [Fact]
        public void Pedido_Deve_AplicarVoucherRepetidoNoPedido()
        {
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var voucher = new Voucher
            {
                Id = Guid.NewGuid(), // Adicionei um Id para o voucher
                Codigo = "COD123",
                Percentual = 50,
                ValorDesconto = null,
                Quantidade = 1,
                DataCriacao = DateTime.Now,
                DataValidade = DateTime.Now.AddDays(7),
                Ativo = true,
                Utilizado = false,
                TipoDescontoVoucher = TipoDescontoVoucher.Porcentagem
            };

            // Aplicar o voucher uma vez
            var validationResult1 = pedido.AplicarVoucher(voucher);

            // Tentar aplicar o voucher novamente
            var validationResult2 = pedido.AplicarVoucher(voucher);

            validationResult1.IsValid.Should().BeTrue(); // Primeira aplicação deve ser válida
            validationResult2.IsValid.Should().BeFalse(); // Segunda aplicação deve ser inválida
            validationResult2.Errors.Should().ContainSingle(e => e.ErrorMessage == "Este voucher já foi aplicado anteriormente no pedido.");
        }

        [Fact]
        public void Pedido_Deve_AplicarVoucherComDescontoPorValorNoPedido()
        {
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();
            var produtoNome = "Produto teste";
            var quantidade = 2;
            var valorUnitario = 100;
            var item = new PedidoItem(produtoId, produtoNome, quantidade, valorUnitario);
            pedido.AdicionarItem(item);

            var voucher = new Voucher
            {
                Codigo = "COD123",
                Percentual = null,
                ValorDesconto = 50, // Desconto de R$50
                Quantidade = 1,
                DataCriacao = DateTime.Now,
                DataValidade = DateTime.Now.AddDays(7),
                Ativo = true,
                Utilizado = false,
                TipoDescontoVoucher = TipoDescontoVoucher.Valor
            };

            var validationResult = pedido.AplicarVoucher(voucher);

            validationResult.IsValid.Should().BeTrue();
            pedido.VoucherUtilizado.Should().BeTrue();
            pedido.Desconto.Should().Be(50); // Valor do desconto
            pedido.ValorTotal.Should().Be(150); // Valor total após o desconto
        }

        [Fact]
        public void Pedido_Deve_AplicarVoucherComDescontoPorPercentualNoPedido()
        {
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();
            var produtoNome = "Produto teste";
            var quantidade = 2;
            var valorUnitario = 100;
            var item = new PedidoItem(produtoId, produtoNome, quantidade, valorUnitario);
            pedido.AdicionarItem(item);

            var voucher = new Voucher
            {
                Codigo = "COD123",
                Percentual = 10, // Desconto de 10%
                ValorDesconto = null,
                Quantidade = 1,
                DataCriacao = DateTime.Now,
                DataValidade = DateTime.Now.AddDays(7),
                Ativo = true,
                Utilizado = false,
                TipoDescontoVoucher = TipoDescontoVoucher.Porcentagem
            };

            var validationResult = pedido.AplicarVoucher(voucher);

            validationResult.IsValid.Should().BeTrue();
            pedido.VoucherUtilizado.Should().BeTrue();
            pedido.Desconto.Should().Be(20); // Valor do desconto (10% de R$200)
            pedido.ValorTotal.Should().Be(180); // Valor total após o desconto
        }
    }
}