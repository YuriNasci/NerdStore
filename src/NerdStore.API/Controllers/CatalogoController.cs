using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalogo.Application.Services;
using NerdStore.Core.Services.WebAPI.Controllers;

namespace NerdStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogoController : MainController
    {
        private readonly IProdutoAppService _produtoAppService;

        public CatalogoController(IProdutoAppService produtoAppService)
        {
            _produtoAppService = produtoAppService;
        }

        [HttpGet("produtos")]
        public async Task<IActionResult> GetProdutos()
        {
            var response = await _produtoAppService.ObterTodos();
            if (response == null || !response.Any()) ProcessarRespostaMensagem(StatusCodes.Status404NotFound, "Não existem dados para exibição.");
            return RespostaPersonalizada(response);
        }

        [HttpGet("produto-detalhe/{id}")]
        public async Task<IActionResult> GetProduto(Guid id)
        {
            var response = await _produtoAppService.ObterPorId(id);
            if (response == null) ProcessarRespostaMensagem(StatusCodes.Status404NotFound, "Produto não encontrado.");
            return RespostaPersonalizada(response);
        }        
        
    }
}
