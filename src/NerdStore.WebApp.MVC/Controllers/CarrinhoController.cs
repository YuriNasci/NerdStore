using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Core.Communication;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.WebApp.MVC.Models;
using NerdStore.WebApp.MVC.Services.Interfaces;

namespace NerdStore.WebApp.MVC.Controllers
{
    public class CarrinhoController : ControllerBase
    {
        private readonly IVendasService _vendasService;
        

        public CarrinhoController(INotificationHandler<DomainNotification> notifications,
                                  IMediatorHandler mediatorHandler,
                                  IVendasService vendasService) : base(notifications, mediatorHandler)
        {            
            _vendasService = vendasService;
        }

        [HttpGet]
        [Route("meu-carrinho")]
        public async Task<IActionResult> Index()
        {
            var response = await _vendasService.ObterCarrinhoCliente(ClienteId);            
            return View(response);
        }

        [HttpPost]        
        [Route("adicionar-item")]
        public async Task<IActionResult> AdicionarItem(Guid id, int quantidade)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            var response = await _vendasService.AdicionarItem(id, quantidade);
            if (response == null) return RedirectToAction("Error", "Home", new { id = StatusCodes.Status400BadRequest}); 

            if (!OperacaoValida())
            {
                TempData["Erros"] = ObterNotificacoesErro();
                return RedirectToAction("ProdutoDetalhe", "Vitrine", new { id });
            }

            TempData["Sucesso"] = response.SuccessMessage;
            return RedirectToAction("Index", await _vendasService.ObterCarrinhoCliente(ClienteId));
        }

        [HttpPost]
        [Route("remover-item")]
        public async Task<IActionResult> RemoverItem(Guid id)
        {
            var response = await _vendasService.RemoverItem(id);
            if (response == null) return RedirectToAction("Error", "Home", new { id = StatusCodes.Status400BadRequest });

            if (!OperacaoValida())
            {
                //TempData["Erros"] = ObterNotificacoesErro();
                return View("Index", await _vendasService.ObterCarrinhoCliente(ClienteId));
            }

            TempData["Sucesso"] = response.SuccessMessage;
            return View("Index", await _vendasService.ObterCarrinhoCliente(ClienteId));
        }

        [HttpPost]
        [Route("atualizar-item")]
        public async Task<IActionResult> AtualizarItem(Guid id, int quantidade)
        {
            var response = await _vendasService.AtualizarItem(id, quantidade);
            if (response == null) return RedirectToAction("Error", "Home", new { id = StatusCodes.Status400BadRequest });

            if (!OperacaoValida())
            {
                TempData["Erros"] = ObterNotificacoesErro();
                return RedirectToAction("Index");
            }

            TempData["Sucesso"] = response.SuccessMessage;
            return View("Index", await _vendasService.ObterCarrinhoCliente(ClienteId));
        }

        [HttpPost]
        [Route("aplicar-voucher")]
        public async Task<IActionResult> AplicarVoucher(string voucherCodigo)
        {
            var response = await _vendasService.AplicarVoucher(voucherCodigo);
            if (response == null) return View("Index", await _vendasService.ObterCarrinhoCliente(ClienteId));           

            if (!OperacaoValida())
            {
                TempData["Erros"] = ObterNotificacoesErro();
                return RedirectToAction("Index");
            }

            TempData["Sucesso"] = ObterNotificacoesErro();
            return View("Index", response);
        }

        [HttpGet]
        [Route("resumo-da-compra")]
        public async Task<IActionResult> ResumoDaCompra()
        {
            var response = await _vendasService.ResumoDaCompra(ClienteId);
            if (response == null) return View("Index", await _vendasService.ObterCarrinhoCliente(ClienteId));

            return View(response);
        }

        [HttpPost]
        [Route("iniciar-pedido")]
        public async Task<IActionResult> IniciarPedido(CarrinhoViewModel carrinhoViewModel)
        {
            var response = await _vendasService.IniciarPedido(carrinhoViewModel);
            if (response == null) return BadRequest();            

            if (!OperacaoValida())
            {
                return View("ResumoDaCompra", await _vendasService.ObterCarrinhoCliente(ClienteId));
            }

            return RedirectToAction("Index", "Pedido");
            
        }
    }
}