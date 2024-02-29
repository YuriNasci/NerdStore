using System;
using NerdStore.Core.DomainObjects;
using Xunit;

namespace NerdStore.Catalogo.Domain.Tests
{
    public class ProdutoTests
    {
        [Fact]
        public void Produto_Validar_ValidacoesDevemRetornarExceptions()
        {
            // Arrange & Act & Assert

            var ex = Assert.Throws<DomainException>(() =>
                new Produto(string.Empty, "Descricao", false, 100, Guid.NewGuid(), DateTime.Now, "Imagem", new Dimensoes(1, 1, 1))
            );

            Assert.Equal("O campo Nome do produto não pode estar vazio", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Produto("Nome", string.Empty, false, 100, Guid.NewGuid(), DateTime.Now, "Imagem", new Dimensoes(1, 1, 1))
            );

            Assert.Equal("O campo Descricao do produto não pode estar vazio", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Produto("Nome", "Descricao", false, 0, Guid.NewGuid(), DateTime.Now, "Imagem", new Dimensoes(1, 1, 1))
            );

            Assert.Equal("O campo Valor do produto não pode se menor igual a 0", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Produto("Nome", "Descricao", false, 100, Guid.Empty, DateTime.Now, "Imagem", new Dimensoes(1, 1, 1))
            );

            Assert.Equal("O campo CategoriaId do produto não pode estar vazio", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Produto("Nome", "Descricao", false, 100, Guid.NewGuid(), DateTime.Now, string.Empty, new Dimensoes(1, 1, 1))
            );

            Assert.Equal("O campo Imagem do produto não pode estar vazio", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Produto("Nome", "Descricao", false, 100, Guid.NewGuid(), DateTime.Now, "Imagem", new Dimensoes(0, 1, 1))
            );

            Assert.Equal("O campo Altura não pode ser menor ou igual a 0", ex.Message);
        }

        [Fact]
        public void Produto_Ativar_DeveAtivarProduto()
        {
            var produto = new Produto("Nome", "Descricao", false, 100, Guid.NewGuid(), DateTime.Now, "Imagem", new Dimensoes(1, 1, 1));

            produto.Ativar();

            Assert.True(produto.Ativo);
        }

        [Fact]
        public void Produto_Desativar_DeveDesativarProduto()
        {
            var produto = new Produto("Nome", "Descricao", true, 100, Guid.NewGuid(), DateTime.Now, "Imagem", new Dimensoes(1, 1, 1));

            produto.Desativar();

            Assert.False(produto.Ativo);
        }

        [Fact]
        public void Produto_AlterarCategoria_DeveAlterarCategoriaDoProduto()
        {
            var produto = new Produto("Nome", "Descricao", true, 100, Guid.NewGuid(), DateTime.Now, "Imagem", new Dimensoes(1, 1, 1));
            var novaCategoria = new Categoria("Nova Categoria", 123);

            produto.AlterarCategoria(novaCategoria);

            Assert.Equal(novaCategoria, produto.Categoria);
            Assert.Equal(novaCategoria.Id, produto.CategoriaId);
        }

        [Fact]
        public void Produto_AlterarDescricao_DeveAlterarDescricaoDoProduto()
        {
            var produto = new Produto("Nome", "Descricao", true, 100, Guid.NewGuid(), DateTime.Now, "Imagem", new Dimensoes(1, 1, 1));
            var novaDescricao = "Nova Descrição";

            produto.AlterarDescricao(novaDescricao);

            Assert.Equal(novaDescricao, produto.Descricao);
        }

        [Theory]
        [InlineData(10, true)]
        [InlineData(0, false)]
        public void Produto_PossuiEstoque_DeveRetornarTrueQuandoEstoqueSuficiente(int quantidadeEstoque, bool resultadoEsperado)
        {
            var produto = new Produto("Nome", "Descricao", true, 100, Guid.NewGuid(), DateTime.Now, "Imagem", new Dimensoes(1, 1, 1));
            produto.ReporEstoque(quantidadeEstoque);

            var possuiEstoque = produto.PossuiEstoque(5);

            Assert.Equal(resultadoEsperado, possuiEstoque);
        }

        [Theory]
        [InlineData(10, 5, 5)]
        public void Produto_DebitarEstoque_DeveAlterarQuantidadeEstoque(int estoqueInicial, int quantidadeDebitada, int estoqueFinalEsperado)
        {
            var produto = new Produto("Nome", "Descricao", true, 100, Guid.NewGuid(), DateTime.Now, "Imagem", new Dimensoes(1, 1, 1));
            produto.ReporEstoque(estoqueInicial);

            produto.DebitarEstoque(quantidadeDebitada);

            Assert.Equal(estoqueFinalEsperado, produto.QuantidadeEstoque);
        }

        [Theory]
        [InlineData(10, 15)]
        public void Produto_DebitarEstoque_DeveLancarExcecaoQuandoEstoqueInsuficiente(int estoqueInicial, int quantidadeDebitada)
        {
            var produto = new Produto("Nome", "Descricao", true, 100, Guid.NewGuid(), DateTime.Now, "Imagem", new Dimensoes(1, 1, 1));
            produto.ReporEstoque(estoqueInicial);

            Assert.Throws<DomainException>(() => produto.DebitarEstoque(quantidadeDebitada));
        }

        [Fact]
        public void Produto_AlterarAtributoCategoria_DeveAtualizarAtributoDaCategoria()
        {
            var categoriaAntiga = new Categoria("Categoria Antiga", 123);
            var categoriaNova = new Categoria("Nova Categoria", 456);
            var produto = new Produto("Camisa Nerd", "Camisa com estampa de super-heróis", true, 80, categoriaAntiga.Id, DateTime.Now, "camisa_nerd.jpg", new Dimensoes(30, 20, 2));

            produto.AlterarCategoria(categoriaNova);

            Assert.Equal(categoriaNova, produto.Categoria);
            Assert.Equal(categoriaNova.Id, produto.CategoriaId);
        }

        [Fact]
        public void Produto_AssociarCategoria_DeveAssociarCategoriaAoProduto()
        {
            var categoria = new Categoria("Camisas de Nerd", 456);
            var produto = new Produto("Camisa Nerd", "Camisa com estampa de super-heróis", true, 80, categoria.Id, DateTime.Now, "camisa_nerd.jpg", new Dimensoes(30, 20, 2));

            produto.AlterarCategoria(categoria);

            Assert.Equal(categoria, produto.Categoria);
            Assert.Equal(categoria.Id, produto.CategoriaId);
        }
    }
}
