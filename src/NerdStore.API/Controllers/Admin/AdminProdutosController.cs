using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalogo.Application.Services;
using NerdStore.Catalogo.Application.ViewModels;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.Core.Services.WebAPI.Controllers;

namespace NerdStore.API.Controllers.Admin
{
    [Route("api/admin")]
    public class AdminProdutosController : MainController
    {
        private readonly IProdutoAppService _produtoAppService;

        public AdminProdutosController(IProdutoAppService produtoAppService, 
                                       INotificationHandler<DomainNotification> notifications,
                                       IMediatorHandler mediatorHandler) : base(notifications, mediatorHandler)
        {
            _produtoAppService = produtoAppService;
        }

        [HttpGet]
        [Route("produtos")]
        public async Task<IActionResult> ObterProdutos()
        {
            var response = await _produtoAppService.ObterTodos();

            if (response == null || !response.Any()) 
                return ProcessarRespostaMensagem(StatusCodes.Status404NotFound, "Não existem dados para exibição.");

            return RespostaPersonalizada(response);
        }

        [HttpPost]
        [Route("novo-produto")]
        public async Task<IActionResult> NovoProduto(ProdutoViewModel produtoViewModel)
        {
            if (!ModelState.IsValid) return ProcessarRespostaMensagem(StatusCodes.Status400BadRequest, "Ocorreu um erro ao tentar criar um novo produto.");

            var response = await _produtoAppService.AdicionarProduto(produtoViewModel);

            if(response == false) return ProcessarRespostaMensagem(StatusCodes.Status400BadRequest, "Ocorreu um erro ao tentar criar um novo produto.");

            AdicionaMensagemSucesso("Novo Produto criado com sucesso.");

            return RespostaPersonalizada(StatusCodes.Status201Created);
        }

        [HttpPut]
        [Route("atualizar-produto")]
        public async Task<IActionResult> AtualizarProduto(ProdutoViewModel produtoViewModel)
        {            
            ModelState.Remove("QuantidadeEstoque");
            if (!ModelState.IsValid) return ProcessarRespostaMensagem(StatusCodes.Status400BadRequest, "Ocorreu um erro ao tentar atualizar o produto.");

            var response = await _produtoAppService.AtualizarProduto(produtoViewModel);

            if (response == false) return ProcessarRespostaMensagem(StatusCodes.Status400BadRequest, "Ocorreu um erro ao tentar atualizar o produto.");
            
            AdicionaMensagemSucesso("Produto atualizado com sucesso.");
            
            return RespostaPersonalizada(StatusCodes.Status200OK);
        }

        [HttpPut]
        [Route("produto/{id}/atualizar-estoque/{quantidade}")]
        public async Task<IActionResult> AtualizarEstoque([FromRoute]Guid id, [FromRoute]int quantidade)
        {
            if (!ModelState.IsValid) return ProcessarRespostaMensagem(StatusCodes.Status400BadRequest, "Ocorreu um erro ao tentar atualizar o estoque.");
            
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
                return ProcessarRespostaMensagem(StatusCodes.Status400BadRequest, "Ocorreu um erro ao tentar atualizar o estoque.");
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

        [HttpPost]
        [Route("novo-produto-com-imagem")]
        public async Task<IActionResult> NovoProdutoComImagem([FromForm] ProdutoImagemViewModel produtoViewModel)
        {
            if (!ModelState.IsValid) return ProcessarRespostaMensagem(StatusCodes.Status400BadRequest, "Ocorreu um erro ao tentar criar um novo produto.");

            if (produtoViewModel.Imagem != null && produtoViewModel.Imagem.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                await produtoViewModel.Imagem.CopyToAsync(memoryStream);
                produtoViewModel.ImagemBase64String = Convert.ToBase64String(memoryStream.ToArray());
            }

            var response = await _produtoAppService.AdicionarProduto(produtoViewModel);

            if(response == false) return ProcessarRespostaMensagem(StatusCodes.Status400BadRequest, "Ocorreu um erro ao tentar criar um novo produto.");

            AdicionaMensagemSucesso("Novo Produto criado com sucesso.");

            return RespostaPersonalizada(StatusCodes.Status201Created);
        }

        [HttpPut]
        [Route("atualizar-produto-com-imagem")]
        public async Task<IActionResult> AtualizarProdutoComImagem([FromForm] ProdutoImagemViewModel produtoViewModel)
        {
            ModelState.Remove("QuantidadeEstoque");
            if (!ModelState.IsValid) return ProcessarRespostaMensagem(StatusCodes.Status400BadRequest, "Ocorreu um erro ao tentar atualizar o produto.");

            if (produtoViewModel.Imagem != null && produtoViewModel.Imagem.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                await produtoViewModel.Imagem.CopyToAsync(memoryStream);
                produtoViewModel.ImagemBase64String = Convert.ToBase64String(memoryStream.ToArray());
            }

            var response = await _produtoAppService.AtualizarProduto(produtoViewModel);

            if (response == false) return ProcessarRespostaMensagem(StatusCodes.Status400BadRequest, "Ocorreu um erro ao tentar atualizar o produto.");
            
            AdicionaMensagemSucesso("Produto atualizado com sucesso.");
            
            return RespostaPersonalizada(StatusCodes.Status200OK);
        }

    }
}
