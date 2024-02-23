using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalogo.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NerdStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogoController: ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;

        public CatalogoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpPost]
        [Route("adicionar-produto")]
        public IActionResult AddProduto([FromBody]object produto)
        {
            return null;
        }

        [HttpGet("{id}")]
        [Route("produto-detalhe")]
        public IActionResult GetProduto(int id)
        {
            return null;
        }

        [HttpGet]
        [Route("produtos")]
        public IActionResult GetProdutos()
        {
            return null;
        }

        [HttpGet]
        [Route("categorias")]
        public IActionResult GetCategorias()
        {
            return null;
        }

        [HttpGet("{codigoCategoria}")]
        [Route("produto-categoria")]
        public IActionResult GetProdutosByCategoria(int codigoCategoria)
        {
            return null;
        }

        [HttpGet("repor-estoque")]
        public IActionResult ReporEstoque(int id, int quantidade)
        {
            return null;
        }

        [HttpGet("debitar-estoque")]
        public IActionResult DebitarEstoque(int id, int quantidade)
        {
            return null;
        }
    }
}
