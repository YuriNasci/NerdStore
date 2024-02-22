using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NerdStore.WebApp.MVC.Services.Interfaces;

namespace NerdStore.WebApp.MVC.Controllers
{
    public class VitrineController : Controller
    {
        private readonly ICatalogoService _catalogoService;

        public VitrineController(ICatalogoService catalogoService)
        {
            _catalogoService = catalogoService;
        }

        [HttpGet]
        [Route("")]
        [Route("vitrine")]
        public async Task<IActionResult> Index()
        {
            return View(await _catalogoService.ObterTodos());
        }

        [HttpGet]
        [Route("produto-detalhe/{id}")]
        public async Task<IActionResult> ProdutoDetalhe(Guid id)
        {
            return View(await _catalogoService.ObterPorId(id));
        }
    }
}