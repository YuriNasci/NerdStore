using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalogo.Application.Services;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Extensions;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.Core.Services.WebAPI.Controllers;
using NerdStore.Vendas.Application.Commands;
using NerdStore.Vendas.Application.Queries;
using NerdStore.Vendas.Application.Queries.ViewModels;

namespace NerdStore.API.Controllers
{
    [Route("api/[controller]")]
    public class CarrinhoController : MainController
    {
        private readonly IProdutoAppService _produtoAppService;
        private readonly IPedidoQueries _pedidoQueries;
        private readonly IMediatorHandler _mediatorHandler;

        public CarrinhoController(INotificationHandler<DomainNotification> notifications,
                                  IProdutoAppService produtoAppService,
                                  IMediatorHandler mediatorHandler,
                                  IPedidoQueries pedidoQueries) : base(notifications, mediatorHandler)
        {
            _produtoAppService = produtoAppService;            
            _pedidoQueries = pedidoQueries;
            _mediatorHandler = mediatorHandler;
        }

        [HttpGet]
        [Route("meu-carrinho/{clientId}")]
        public async Task<IActionResult> Index(Guid clientId)
        {
            var vmCarrinho = await _pedidoQueries.ObterCarrinhoCliente(ClienteId);

            if (vmCarrinho == null) return ProcessarRespostaMensagem(StatusCodes.Status404NotFound, "Carrinho de compra não encontrado");

            return RespostaPersonalizada(vmCarrinho);
        }

        [HttpPost]
        [Route("{id}/adicionar-item/{quantidade}")]
        public async Task<IActionResult> AdicionarItem(Guid id, int quantidade)
        {
            var vmProduto = await _produtoAppService.ObterPorId(id);
            if (vmProduto == null) return ProcessarRespostaMensagem(StatusCodes.Status404NotFound, "Item não encontrado");

            if (vmProduto.QuantidadeEstoque < quantidade)
            {                
                return ProcessarRespostaMensagem(StatusCodes.Status400BadRequest, "Item com estoque insuficiente");
            }

            var command = new AdicionarItemPedidoCommand(ClienteId, vmProduto.Id, vmProduto.Nome, quantidade, vmProduto.Valor);
            await _mediatorHandler.EnviarComando(command);

            if (!TemNotificacao())
            {
                ObterNotificacoesErro().ForEach(m => AdicionarErroProcessamento(m));                
                return RespostaPersonalizada(StatusCodes.Status400BadRequest);
            }            

            AdicionaMensagemSucesso("Item adicionado com sucesso");
            return RespostaPersonalizada(StatusCodes.Status201Created);
        }

        [HttpDelete]
        [Route("remover-item/{id}")]
        public async Task<IActionResult> RemoverItem(Guid id)
        {
            var vmProduto = await _produtoAppService.ObterPorId(id);
            if (vmProduto == null) return RespostaPersonalizada(StatusCodes.Status404NotFound);

            var command = new RemoverItemPedidoCommand(ClienteId, id);
            await _mediatorHandler.EnviarComando(command);

            if (!TemNotificacao())
            {
                ObterNotificacoesErro().ForEach(m => AdicionarErroProcessamento(m));
                return RespostaPersonalizada(StatusCodes.Status400BadRequest);
            }

            AdicionaMensagemSucesso("Item removido com sucesso");
            return RespostaPersonalizada(StatusCodes.Status200OK);
        }

        [HttpPut]
        [Route("{id}/atualizar-item/{quantidade}")]
        public async Task<IActionResult> AtualizarItem(Guid id, int quantidade)
        {
            var vmProduto = await _produtoAppService.ObterPorId(id);
            if (vmProduto == null) return RespostaPersonalizada(StatusCodes.Status404NotFound);

            var command = new AtualizarItemPedidoCommand(ClienteId, id, quantidade);
            await _mediatorHandler.EnviarComando(command);

            if (!TemNotificacao())
            {
                ObterNotificacoesErro().ForEach(m => AdicionarErroProcessamento(m));
                return RespostaPersonalizada(StatusCodes.Status400BadRequest);
            }

            AdicionaMensagemSucesso("Item atualizado com sucesso");
            return RespostaPersonalizada(StatusCodes.Status200OK);
        }

        [HttpPost]
        [Route("aplicar-voucher/{voucherCodigo}")]
        public async Task<IActionResult> AplicarVoucher(string voucherCodigo)
        {
            var command = new AplicarVoucherPedidoCommand(ClienteId, voucherCodigo);
            await _mediatorHandler.EnviarComando(command);

            if (!TemNotificacao())
            {
                ObterNotificacoesErro().ForEach(m => AdicionarErroProcessamento(m));
                return RespostaPersonalizada(StatusCodes.Status400BadRequest);
            }

            AdicionaMensagemSucesso("Voucher aplicado com sucesso");
            return RespostaPersonalizada(await _pedidoQueries.ObterCarrinhoCliente(ClienteId));
        }

        [HttpGet]
        [Route("resumo-da-compra/{clientId}")]
        public async Task<IActionResult> ResumoDaCompra(Guid clientId)
        {
            var vmCarrinho = await _pedidoQueries.ObterCarrinhoCliente(clientId);

            if (vmCarrinho == null) return ProcessarRespostaMensagem(StatusCodes.Status400BadRequest, "Falha ao tentar obter resumo da compra");

            return RespostaPersonalizada(vmCarrinho);
        }

        [HttpPost]
        [Route("iniciar-pedido")]
        public async Task<IActionResult> IniciarPedido(CarrinhoViewModel request)
        {
            var vmCarrinho = await _pedidoQueries.ObterCarrinhoCliente(ClienteId);

            var command = new IniciarPedidoCommand(vmCarrinho.PedidoId, ClienteId, vmCarrinho.ValorTotal, request.Pagamento.NomeCartao,
                request.Pagamento.NumeroCartao, request.Pagamento.ExpiracaoCartao, request.Pagamento.CvvCartao);

            await _mediatorHandler.EnviarComando(command);

            if (!TemNotificacao())
            {
                ObterNotificacoesErro().ForEach(m => AdicionarErroProcessamento(m));
                return RespostaPersonalizada(StatusCodes.Status400BadRequest);
            }

            AdicionaMensagemSucesso("Pedido iniciado com sucesso");
            return RespostaPersonalizada(vmCarrinho);
        }
    }
}