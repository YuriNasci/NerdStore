using NerdStore.Core.Communication;
using NerdStore.WebApp.MVC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NerdStore.WebApp.MVC.Services.Interfaces
{
    public interface IVendasService
    {
        Task<ResponseResult> AdicionarItem(Guid id, int quantidade);
        Task<CarrinhoViewModel> AplicarVoucher(string voucherCodigo);
        Task<ResponseResult> AtualizarItem(Guid id, int quantidade);
        Task<CarrinhoViewModel> IniciarPedido(CarrinhoViewModel request);
        Task<CarrinhoViewModel> ObterCarrinhoCliente(Guid clientId);
        Task<IEnumerable<PedidoViewModel>> ObterPedidosCliente();
        Task<ResponseResult> RemoverItem(Guid id);
        Task<CarrinhoViewModel> ResumoDaCompra(Guid clientId);
    }
}