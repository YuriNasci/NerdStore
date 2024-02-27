using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using NerdStore.Core.Communication;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Extensions;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.WebApp.MVC.Models;
using NerdStore.WebApp.MVC.Services.Interfaces;

namespace NerdStore.WebApp.MVC.Services
{
    public class VendasService : TextSerializerService, IVendasService
    {
        private readonly HttpClient _httpClient;


        public VendasService(HttpClient httpClient,
                               IOptions<AppSettings> settings,
                               INotificationHandler<DomainNotification> notifications,
                               IMediatorHandler mediatorHandler) : base(notifications, mediatorHandler)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CatalogoUrl);
        }

        public async Task<ResponseResult> AdicionarItem(Guid id, int quantidade)
        {
            var response = await _httpClient.PostAsync($"/api/carrinho/{id}/adicionar-item/{quantidade}", null);
            if (!HandlerResponseErrors(response))
            {
                var result = await DeserializeResponseObject<ResponseResult>(response);
                foreach (var item in result.Errors.Messages)
                {
                    NotificarErro(result.Status.ToString(), item);
                }
                //return null;
            }
            return await DeserializeResponseObject<ResponseResult>(response);
        }

        public async Task<ResponseResult> AtualizarItem(Guid id, int quantidade)
        {
            var response = await _httpClient.PutAsync($"/api/carrinho/{id}/atualizar-item/{quantidade}", null);
            if (!HandlerResponseErrors(response))
            {
                var result = await DeserializeResponseObject<ResponseResult>(response);
                foreach (var item in result.Errors.Messages)
                {
                    NotificarErro(result.Status.ToString(), item);
                }
                //return null;

            }
            return await DeserializeResponseObject<ResponseResult>(response);
        }

        public async Task<ResponseResult> RemoverItem(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/api/carrinho/remover-item/{id}");
            //if (response.StatusCode == HttpStatusCode.NotFound) return null;
            if (!HandlerResponseErrors(response))
            {
                var result = await DeserializeResponseObject<ResponseResult>(response);
                foreach (var item in result.Errors.Messages)
                {
                    NotificarErro(result.Status.ToString(), item);
                }
                //return null;
            }
            return await DeserializeResponseObject<ResponseResult>(response);
        }

        public async Task<CarrinhoViewModel> AplicarVoucher(string voucherCodigo)
        {
            var response = await _httpClient.PostAsync($"/api/carrinho/aplicar-voucher/{voucherCodigo}", null);
            if (response.StatusCode == HttpStatusCode.NotFound) return null;
            if (!HandlerResponseErrors(response))
            {
                var result = await DeserializeResponseObject<ResponseResult>(response);
                foreach (var item in result.Errors.Messages)
                {
                    NotificarErro(result.Status.ToString(), item);
                }
                return null;
            }
            return await DeserializeResponseObject<CarrinhoViewModel>(response);
        }

        public async Task<CarrinhoViewModel> IniciarPedido(CarrinhoViewModel request)
        {
            var content = GetContent(request);
            var response = await _httpClient.PostAsync($"/api/carrinho/iniciar-pedido/", content);
            if (response.StatusCode == HttpStatusCode.NotFound) return null;
            if (!HandlerResponseErrors(response))
            {
                var result = await DeserializeResponseObject<ResponseResult>(response);
                foreach (var item in result.Errors.Messages)
                {
                    NotificarErro(result.Status.ToString(), item);
                }
                return null;
            }
            return await DeserializeResponseObject<CarrinhoViewModel>(response);
        }

        public async Task<CarrinhoViewModel> ResumoDaCompra(Guid clientId)
        {
            var response = await _httpClient.GetAsync($"/api/carrinho/resumo-da-compra/{clientId}");
            if (response.StatusCode == HttpStatusCode.NotFound) return null;
            return await DeserializeResponseObject<CarrinhoViewModel>(response);
        }

        public async Task<CarrinhoViewModel> ObterCarrinhoCliente(Guid clientId)
        {
            var response = await _httpClient.GetAsync($"/api/carrinho/meu-carrinho/{clientId}");

            if (!HandlerResponseErrors(response))
            {
                var result = await DeserializeResponseObject<ResponseResult>(response);
                foreach (var item in result.Errors.Messages)
                {
                    NotificarErro(result.Status.ToString(), item);
                }
                return null;
            }
            return await DeserializeResponseObject<CarrinhoViewModel>(response);
        }

        public async Task<IEnumerable<PedidoViewModel>> ObterPedidosCliente(Guid clientId)
        {
            var response = await _httpClient.GetAsync($"/api/pedido/meus-pedidos/{clientId}");

            if (!HandlerResponseErrors(response))
            {
                var result = await DeserializeResponseObject<ResponseResult>(response);
                foreach (var item in result.Errors.Messages)
                {
                    NotificarErro(result.Status.ToString(), item);
                }
                return null;
            }
            return await DeserializeResponseObject<IEnumerable<PedidoViewModel>>(response);
        }

    }
}