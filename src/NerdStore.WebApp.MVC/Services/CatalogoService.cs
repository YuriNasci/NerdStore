using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NerdStore.Core.Communication;
using NerdStore.Core.Extensions;
using NerdStore.WebApp.MVC.Models;
using NerdStore.WebApp.MVC.Services.Interfaces;

namespace NerdStore.WebApp.MVC.Services
{
    public class CatalogoService : TextSerializerService, ICatalogoService
    {
        private readonly HttpClient _httpClient;

        public CatalogoService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            settings.Value.CatalogoUrl = "https://localhost:6001";
            _httpClient.BaseAddress = new Uri(settings.Value.CatalogoUrl);
        }

        public async Task<ResponseResult> AdicionarProduto(ProdutoViewModel request)
        {
            var content = GetContent(request);
            var response = await _httpClient.PostAsync("/api/catalogo/adicionar-produto", content);
            if(!HandlerResponseErrors(response)) 
                return await DeserializeResponseObject<ResponseResult>(response);
            return ReturnOk();
        }
 
        public async Task<ResponseResult> AtualizarProduto(ProdutoViewModel request)
        {
            var content = GetContent(request);
            var response = await _httpClient.PutAsync("/api/catalogo/editar-produto", content);
            if(!HandlerResponseErrors(response)) 
                return await DeserializeResponseObject<ResponseResult>(response);
            return ReturnOk();
        } 

        public async Task<ProdutoViewModel> ObterPorId(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/catalogo/produto-detalhe/{id}");
            if (response.StatusCode == HttpStatusCode.NotFound) return null;
            return await DeserializeResponseObject<ProdutoViewModel>(response);
        }

        public async Task<IEnumerable<ProdutoViewModel>> ObterTodos()
        {
            var response = await _httpClient.GetAsync($"/api/catalogo/produtos");
            if(response.StatusCode == HttpStatusCode.NotFound) return null;
            HandlerResponseErrors(response);
            return await DeserializeResponseObject<IEnumerable<ProdutoViewModel>>(response);
        }

        public async Task<IEnumerable<CategoriaViewModel>> ObterCategorias()
        {
            var response = await _httpClient.GetAsync($"/api/catalogo/categorias");
            if(response.StatusCode == HttpStatusCode.NotFound) return null;
            HandlerResponseErrors(response);
            return await DeserializeResponseObject<IEnumerable<CategoriaViewModel>>(response);
        }

        public async Task<IEnumerable<ProdutoViewModel>> ObterPorCategoria(int codigo)
        {
             var response = await _httpClient.GetAsync($"/api/catalogo/produto-categoria/{codigo}");
            if (response.StatusCode == HttpStatusCode.NotFound) return null;
            return await DeserializeResponseObject<IEnumerable<ProdutoViewModel>>(response);
        }

        public async Task<ProdutoViewModel> ReporEstoque(Guid id, int quantidade)
        {
            var response = await _httpClient.GetAsync($"/api/catalogo/repor-estoque?id={id}&quantidade={quantidade}");
            if(response.StatusCode == HttpStatusCode.NotFound) return null;
            HandlerResponseErrors(response);
            return await DeserializeResponseObject<ProdutoViewModel>(response);
        }

        public async Task<ProdutoViewModel> DebitarEstoque(Guid id, int quantidade)
        {
            var response = await _httpClient.GetAsync($"/api/catalogo/debitar-estoque?id={id}&quantidade={quantidade}");
            if(response.StatusCode == HttpStatusCode.NotFound) return null;
            HandlerResponseErrors(response);
            return await DeserializeResponseObject<ProdutoViewModel>(response);
        }
    }
}