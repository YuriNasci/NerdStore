using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalogo.Application.Services;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.Core.Services.WebAPI.Controllers;

namespace NerdStore.API.Controllers
{
    [Route("api/[controller]")]   
    public class CatalogoController : MainController
    {
        private readonly IProdutoAppService _produtoAppService;

        public CatalogoController(IProdutoAppService produtoAppService, 
                                  INotificationHandler<DomainNotification> notifications,
                                  IMediatorHandler mediatorHandler) : base(notifications, mediatorHandler)
        {
            _produtoAppService = produtoAppService;
        }

        [HttpGet("produtos")]
        public async Task<IActionResult> ObterProdutos()
        {
            var response = await _produtoAppService.ObterTodos();

            if (response == null || !response.Any()) return ProcessarRespostaMensagem(StatusCodes.Status404NotFound, "Não existem dados para exibição.");
            
            return RespostaPersonalizada(response);
        }

        [HttpGet("produto-detalhe/{id}")]
        public async Task<IActionResult> ObterProduto(Guid id)
        {
            var response = await _produtoAppService.ObterPorId(id);

            if (response == null) return ProcessarRespostaMensagem(StatusCodes.Status404NotFound, "Produto não encontrado.");
            
            return RespostaPersonalizada(response);
        }        
        
    }
}
