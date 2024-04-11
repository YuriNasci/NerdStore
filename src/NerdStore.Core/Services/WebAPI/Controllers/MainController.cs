using MediatR;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NerdStore.Core.Communication;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.Notifications;


namespace NerdStore.Core.Services.WebAPI.Controllers
{
    [ApiController]
    public abstract class MainController : Controller
    {
        protected ICollection<string> Erros = new List<string>();
        protected ValidationResult ValidationResult { get; set; }        
        protected string MensagemSucesso { get; set; }        

        protected Guid ClienteId = Guid.Parse("4885e451-b0e4-4490-b959-04fabc806d32");

        private readonly DomainNotificationHandler _notifications;

        private readonly IMediatorHandler _mediatorHandler;

        protected MainController(INotificationHandler<DomainNotification> notifications,
                                 IMediatorHandler mediatorHandler)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediatorHandler = mediatorHandler;
        }

        protected bool TemNotificacao()
        {
            return !_notifications.TemNotificacao();
        }

        protected IEnumerable<string> ObterNotificacoesErro()
        {
            return _notifications.ObterNotificacoes().Select(c => c.Value).ToList();
        }

        protected void NotificarErro(string codigo, string mensagem)
        {
            _mediatorHandler.PublicarNotificacao(new DomainNotification(codigo, mensagem));
        }

        protected ActionResult RespostaPersonalizada(object resultado = null)
        {
            if (OperacaoValida())
            {
                if (resultado is int) return TratarMensagensRetorno(resultado);
                return Ok(resultado);
            }

            if (resultado is int) return TratarMensagensRetorno(resultado);

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Mensagens", Erros.ToArray() }
            }));
        }

        protected ActionResult RespostaPersonalizada(ModelStateDictionary modelState)
        {
            var modelErrorCollection = modelState.Values.Select(x => x.Errors);
            foreach (var modelErrors in modelErrorCollection)
            {
                foreach (var modelError in modelErrors)
                {
                    AdicionarErroProcessamento(modelError.ErrorMessage);
                }
            }
            return RespostaPersonalizada();
        }

        protected ActionResult RespostaPersonalizada(ValidationResult validationResult)
        {
            foreach (var erro in validationResult.Errors)
            {
                AdicionarErroProcessamento(erro.ErrorMessage);
            }
            return RespostaPersonalizada();
        }

        protected ActionResult RespostaPersonalizada(ResponseResult resposta)
        {
            RespostaPossuiErros(resposta);
            return RespostaPersonalizada();
        }

        protected ActionResult ProcessarRespostaMensagem(int statusCode, string mensagem)
        {
            AdicionarErroProcessamento(mensagem);
            return RespostaPersonalizada(statusCode);
        }

        protected bool RespostaPossuiErros(ResponseResult resposta)
        {
            if (resposta == null || !resposta.Errors.Messages.Any()) return false;
            foreach (var mensagem in resposta.Errors.Messages)
            {
                AdicionarErroProcessamento(mensagem);
            }
            return true;
        }

        protected bool OperacaoValida()
        {
            return !Erros.Any();
        }

        protected void AdicionarErroProcessamento(string mensagem)
        {
            Erros.Add(mensagem);
        }

        protected void AdicionaMensagemSucesso(string mensagem)
        {
            MensagemSucesso = mensagem;
        }

        protected void LimparErrosProcessamento()
        {
            Erros.Clear();
        }
        private ActionResult TratarMensagensRetorno(object resultado)
        {
            switch (resultado)
            {
                case 200:
                    return Ok(new ResponseResult
                    {
                        Title = "Opa! Sucesso.",
                        Status = StatusCodes.Status200OK,
                        SuccessMessage = MensagemSucesso
                    });

                case 201:
                    return Created(new Uri(Request.Path, UriKind.Relative), new ResponseResult
                    {
                        Title = "Opa! Sucesso.",
                        Status = StatusCodes.Status201Created,
                        SuccessMessage = MensagemSucesso
                    });

                case 204:
                    return StatusCode(StatusCodes.Status204NoContent, new ResponseResult
                    {
                        Title = "Opa! Sucesso.",
                        Status = StatusCodes.Status204NoContent,
                        SuccessMessage = MensagemSucesso
                    });

                case 400:
                    return BadRequest(new ResponseResult
                    {
                        Title = "Opa! Ocorreu um erro.",
                        Status = StatusCodes.Status400BadRequest,
                        Errors = new ResponseErrorMessages { Messages = Erros.ToList() }
                    });

                case 401:
                    return Unauthorized(new ResponseResult
                    {
                        Title = "Opa! Ocorreu um erro.",
                        Status = StatusCodes.Status401Unauthorized,
                        Errors = new ResponseErrorMessages { Messages = Erros.ToList() }
                    });

                case 404:
                    return NotFound(new ResponseResult
                    {
                        Title = "Opa! Ocorreu um erro.",
                        Status = StatusCodes.Status404NotFound,
                        Errors = new ResponseErrorMessages { Messages = Erros.ToList() }
                    });

                default:
                    return StatusCode(StatusCodes.Status500InternalServerError, new ResponseResult
                    {
                        Title = "Opa! Ocorreu um erro.",
                        Status = StatusCodes.Status500InternalServerError,
                        Errors = new ResponseErrorMessages { Messages = Erros.ToList() }
                    });
            }

        }

    }
}
