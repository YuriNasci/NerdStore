using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NerdStore.Core.Communication;
using NerdStore.WebApp.MVC.Models;


namespace NerdStore.WebApp.MVC.Services.Interfaces
{
    public interface ICatalogoService
    {
        Task<IEnumerable<ProdutoViewModel>> ObterPorCategoria(int codigo);
        Task<ProdutoViewModel> ObterPorId(Guid id);
        Task<IEnumerable<ProdutoViewModel>> ObterTodos();
        Task<IEnumerable<CategoriaViewModel>> ObterCategorias();
        Task<ResponseResult> AdicionarProduto(ProdutoViewModel request);
        Task<ResponseResult> AtualizarProduto(ProdutoViewModel request);
        Task<ResponseResult> AtualizarEstoque(Guid id, int quantidade);
        
        //Task<ProdutoViewModel> DebitarEstoque(Guid id, int quantidade);
        //Task<ProdutoViewModel> ReporEstoque(Guid id, int quantidade);
    }
}