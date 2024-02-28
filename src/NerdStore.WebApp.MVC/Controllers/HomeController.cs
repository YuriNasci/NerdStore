using Microsoft.AspNetCore.Mvc;
using NerdStore.WebApp.MVC.Models;

namespace NerdStore.WebApp.MVC.Controllers
{
    public class HomeController : Controller
    {
        [Route("sistema-indisponivel")]
        public IActionResult SistemaIndisponivel()
        {
            var modelErro = new ErrorViewModel
            {
                Message = "O sistema está temporariamente indisponível, isto pode ocorrer em momentos de sobrecarga de usuários.",
                Title = "Sistema indisponível.",
                ErroCode = 500
            };

            return View("Error", modelErro);
        }

        [Route("erro/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var vmErro = new ErrorViewModel();

            if (id == 500)
            {
                vmErro.Message = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
                vmErro.Title = "Erro interno do servidor";
                vmErro.ErroCode = id;
            }
            else if (id == 404)
            {
                vmErro.Message =
                    "A página que está procurando não existe! <br />Em caso de dúvidas entre em contato com nosso suporte";
                vmErro.Title = "Página não encontrada";
                vmErro.ErroCode = id;
            }
            else if (id == 403)
            {
                vmErro.Message = "Você não tem permissão para fazer isto.";
                vmErro.Title = "Acesso Negado";
                vmErro.ErroCode = id;
            }
            else
            {
                vmErro.Message = "Ocorreu um erro durante sua solicitação! Tente novamente mais tarde ou contate nosso suporte.";
                vmErro.Title = "Falha na requisição";
                vmErro.ErroCode = id;
            }

            return View("Error", vmErro);
        }
    }
}
