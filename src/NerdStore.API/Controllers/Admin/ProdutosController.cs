using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalogo.Application.Services;
using NerdStore.Catalogo.Application.ViewModels;
using NerdStore.Core.Services.WebAPI.Controllers;

namespace NerdStore.API.Controllers.Admin
{    
    [Route("api/admin")]
    public class ProdutosController : MainController
    {
        private readonly IProdutoAppService _produtoAppService;

        public ProdutosController(IProdutoAppService produtoAppService)
        {
            _produtoAppService = produtoAppService;
        }

        [HttpGet]
        [Route("produtos")]
        public async Task<IActionResult> ObterProdutos()
        {
            var response = await _produtoAppService.ObterTodos();
            if (response == null || !response.Any()) ProcessarRespostaMensagem(StatusCodes.Status404NotFound, "Não existem dados para exibição.");
            return RespostaPersonalizada(response);
        }

        [HttpPost]
        [Route("novo-produto")]
        public async Task<IActionResult> NovoProduto(ProdutoViewModel produtoViewModel)
        {
            if (!ModelState.IsValid) return RespostaPersonalizada(StatusCodes.Status400BadRequest);

            var response = await _produtoAppService.AdicionarProduto(produtoViewModel);

            if(response == false) ProcessarRespostaMensagem(StatusCodes.Status400BadRequest, "Opa! Ocorreu um erro ao tentar criar um novo produto.");

            AdicionaMensagemSucesso("Produto criado com sucesso.");

            return RespostaPersonalizada(StatusCodes.Status201Created);
        }

        [HttpPut]
        [Route("editar-produto")]
        public async Task<IActionResult> AtualizarProduto(ProdutoViewModel produtoViewModel)
        {            
            ModelState.Remove("QuantidadeEstoque");
            if (!ModelState.IsValid) RespostaPersonalizada(StatusCodes.Status400BadRequest);

            var response = await _produtoAppService.AtualizarProduto(produtoViewModel);

            if (response == false) ProcessarRespostaMensagem(StatusCodes.Status400BadRequest, "Opa! Ocorreu um erro ao tentar atualizar o produto.");
            
            AdicionaMensagemSucesso("Produto atualizado com sucesso.");
            
            return RespostaPersonalizada(StatusCodes.Status200OK);
        }

        [HttpPut]
        [Route("produtos-atualizar-estoque/{id}/{quantidade}")]
        public async Task<IActionResult> AtualizarEstoque([FromRoute]Guid id, [FromRoute]int quantidade)
        {
            if (!ModelState.IsValid) RespostaPersonalizada(StatusCodes.Status400BadRequest);
            
            ProdutoViewModel vmProduto;
            if (quantidade > 0)
            {
                vmProduto = await _produtoAppService.ReporEstoque(id, quantidade);
            }
            else
            {
                vmProduto = await _produtoAppService.DebitarEstoque(id, quantidade);
            }

            if (vmProduto == null) 
            {
                ProcessarRespostaMensagem(StatusCodes.Status400BadRequest, "Opa! Ocorreu um erro ao tentar atualizar o estoque.");
            }

            AdicionaMensagemSucesso("Estoque atualizado com sucesso.");

            return RespostaPersonalizada(StatusCodes.Status200OK);
        }

        [HttpGet]
        [Route("produto-categorias")]
        public async Task<IActionResult> ObterCategorias()
        {
            var categorias = await _produtoAppService.ObterCategorias();
            return RespostaPersonalizada(categorias);
        }
    }
}
