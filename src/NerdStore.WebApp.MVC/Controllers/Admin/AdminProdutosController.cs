using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NerdStore.WebApp.MVC.Models;
using NerdStore.WebApp.MVC.Services.Interfaces;

namespace NerdStore.WebApp.MVC.Controllers.Admin
{
    public class AdminProdutosController : Controller
    {
        private readonly ICatalogoService _catalogoService;

        public AdminProdutosController(ICatalogoService catalogoService)
        {
            _catalogoService = catalogoService;
        }

        [HttpGet]
        [Route("admin-produtos")]
        public async Task<IActionResult> Index()
        {
            return View(await _catalogoService.ObterTodos());
        }

        [HttpGet]
        [Route("novo-produto")]
        public async Task<IActionResult> NovoProduto()
        {
            return View(await PopularCategorias(new ProdutoViewModel()));
        }

        [HttpPost]
        [Route("novo-produto")]        
        public async Task<IActionResult> NovoProduto(ProdutoViewModel produtoViewModel)
        {
            if (!ModelState.IsValid) return View(await PopularCategorias(produtoViewModel));

            var response = await _catalogoService.AdicionarProduto(produtoViewModel);
            if(response.Status != 201) return View(await PopularCategorias(produtoViewModel));

            TempData["Sucesso"] = response.SuccessMessage;

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("editar-produto")]
        public async Task<IActionResult> AtualizarProduto(Guid id)
        {
            return View(await PopularCategorias(await _catalogoService.ObterPorId(id)));
        }

        [HttpPost]
        [Route("editar-produto")]
        public async Task<IActionResult> AtualizarProduto(Guid id, ProdutoViewModel produtoViewModel)
        {
            var produto = await _catalogoService.ObterPorId(id);
            if(produto == null) return View(await PopularCategorias(produtoViewModel));
            produtoViewModel.QuantidadeEstoque = produto.QuantidadeEstoque;

            ModelState.Remove("QuantidadeEstoque");
            if (!ModelState.IsValid) return View(await PopularCategorias(produtoViewModel));

            var response = await _catalogoService.AtualizarProduto(produtoViewModel);
            if (response.Status != 200) return View(await PopularCategorias(produtoViewModel));

            TempData["Sucesso"] = response.SuccessMessage;

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("produtos-atualizar-estoque")]
        public async Task<IActionResult> AtualizarEstoque(Guid id)
        {
            return View("Estoque", await _catalogoService.ObterPorId(id));
        }

        [HttpPost]
        [Route("produtos-atualizar-estoque")]
        public async Task<IActionResult> AtualizarEstoque(Guid id, int quantidade)
        {            
            var response = await _catalogoService.AtualizarEstoque(id, quantidade);
            
            if (response.Status != 200) return View("Estoque", await _catalogoService.ObterPorId(id));

            TempData["Sucesso"] = response.SuccessMessage;

            var produtos = await _catalogoService.ObterTodos();
            
            return RedirectToAction("Index", produtos);            
        }

        private async Task<ProdutoViewModel> PopularCategorias(ProdutoViewModel produto)
        {
            produto.Categorias = await _catalogoService.ObterCategorias();
            return produto;
        }
    }
}