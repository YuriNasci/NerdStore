using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.Core.Services.WebAPI.Controllers;
using NerdStore.Vendas.Application.Queries;

namespace NerdStore.API.Controllers
{
    [Route("api/pedido")]
    public class PedidoController : MainController
    {
        private readonly IPedidoQueries _pedidoQueries;

        public PedidoController(IPedidoQueries pedidoQueries,
                                INotificationHandler<DomainNotification> notifications,
                                IMediatorHandler mediatorHandler) : base(notifications, mediatorHandler)
        {
            _pedidoQueries = pedidoQueries;
        }

        [HttpGet]
        [Route("meus-pedidos")]
        public async Task<IActionResult> Index()
        {
            var response = await _pedidoQueries.ObterPedidosCliente(ClienteId);

            if (response == null || !response.Any()) return ProcessarRespostaMensagem(StatusCodes.Status404NotFound, "Não existem dados para exibição.");

            return RespostaPersonalizada(response);            
        }
    }
}