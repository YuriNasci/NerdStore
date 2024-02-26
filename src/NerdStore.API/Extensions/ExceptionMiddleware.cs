using Microsoft.Data.SqlClient;
using NerdStore.Core.Communication;
using NerdStore.Core.DomainObjects;
using System.Net;
using System.Text;
using System.Text.Json;

namespace NerdStore.API.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;        

        private ICollection<string> Erros { get; set; }


        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
            Erros = new List<string>();
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ArgumentNullException ex)
            {
                HandleRequestExceptionAsync(httpContext, HttpStatusCode.InternalServerError, ex);
            }
            catch (NullReferenceException ex)
            {
                HandleRequestExceptionAsync(httpContext, HttpStatusCode.InternalServerError, ex);
            }
            catch (InvalidOperationException ex)
            {
                HandleRequestExceptionAsync(httpContext, HttpStatusCode.InternalServerError, ex);
            }
            catch (SqlException ex)
            {
                HandleRequestExceptionAsync(httpContext, HttpStatusCode.InternalServerError, ex);
            }
            catch (DomainException ex)
            {                             
                HandleRequestExceptionAsync(httpContext, HttpStatusCode.BadRequest, ex);
            }
            catch (AggregateException ex)
            {
                HandleRequestExceptionAsync(httpContext, HttpStatusCode.BadRequest, ex);
            }
            catch (CustomHttpRequestException ex)
            {
                HandleRequestExceptionAsync(httpContext, ex.StatusCode, ex);
            }            
            catch (HttpRequestException ex)
            {
                var statusCode = ex.StatusCode switch
                {
                    HttpStatusCode.BadRequest => 400,
                    HttpStatusCode.Unauthorized => 401,
                    HttpStatusCode.Forbidden => 403,
                    HttpStatusCode.NotFound => 404,
                    _ => 500
                };

                var httpStatusCode = (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), statusCode.ToString());
                HandleRequestExceptionAsync(httpContext, httpStatusCode, ex);
            }

        }

        private void HandleRequestExceptionAsync(HttpContext context, HttpStatusCode statusCode, Exception exception)
        {
            if (PossuiErroProcessamento()) LimparErroProcessamento();            
            
            AdicionarErroProcessamento(exception.Message);

            var responseResult = TratarMensagensRetorno((int)statusCode, exception);
            var responseObject = JsonSerializer.Serialize(responseResult);
            var data = Encoding.UTF8.GetBytes(responseObject);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = responseResult.Status;
            context.Response.Body.WriteAsync(data, 0, data.Length, CancellationToken.None);

        }

        private void AdicionarErroProcessamento(string mensagem)
        {
            Erros.Add(mensagem);
        }

        private void LimparErroProcessamento()
        {
            Erros.Clear();
        }

        private bool PossuiErroProcessamento()
        {
            if (Erros == null || !Erros.Any()) return false;            
            return true;
        }

        private ResponseResult TratarMensagensRetorno(int resultado, Exception exception)
        {
            switch (resultado)
            {
                case 200:
                    return new ResponseResult
                    {
                        Title = "Opa! Sucesso.",
                        Status = StatusCodes.Status200OK,
                        SuccessMessage = exception.Message
                    };

                case 201:
                    return new ResponseResult
                    {
                        Title = "Opa! Sucesso.",
                        Status = StatusCodes.Status201Created,
                        SuccessMessage = exception.Message
                    };

                case 204:
                    return new ResponseResult
                    {
                        Title = "Opa! Sucesso.",
                        Status = StatusCodes.Status204NoContent,
                        SuccessMessage = exception.Message
                    };

                case 400:
                    return new ResponseResult
                    {
                        Title = "Opa! Ocorreu um erro.",
                        Status = StatusCodes.Status400BadRequest,
                        Errors = new ResponseErrorMessages { Messages = Erros.ToList() }
                    };

                case 401:
                    return new ResponseResult
                    {
                        Title = "Opa! Ocorreu um erro.",
                        Status = StatusCodes.Status401Unauthorized,
                        Errors = new ResponseErrorMessages { Messages = Erros.ToList() }
                    };

                case 403:
                    return new ResponseResult
                    {
                        Title = "Opa! Ocorreu um erro.",
                        Status = StatusCodes.Status403Forbidden,
                        Errors = new ResponseErrorMessages { Messages = Erros.ToList() }
                    };

                case 404:
                    return new ResponseResult
                    {
                        Title = "Opa! Ocorreu um erro.",
                        Status = StatusCodes.Status404NotFound,
                        Errors = new ResponseErrorMessages { Messages = Erros.ToList() }
                    };

                default:
                    return new ResponseResult
                    {
                        Title = "Opa! Ocorreu um erro.",
                        Status = StatusCodes.Status500InternalServerError,
                        SuccessMessage = "Sistema indisponível. Tente mais tarde."
                    };
            }

        }

    }
}
