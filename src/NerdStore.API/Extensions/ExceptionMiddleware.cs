using NerdStore.Core.DomainObjects;
using System.Net;

namespace NerdStore.API.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;       

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (InvalidOperationException)
            {
                HandleRequestExceptionAsync(httpContext, HttpStatusCode.InternalServerError);
            }
            catch (DomainException ex)
            {                             
                HandleRequestExceptionAsync(httpContext, HttpStatusCode.InternalServerError);
            }
            catch (CustomHttpRequestException ex)
            {
                HandleRequestExceptionAsync(httpContext, ex.StatusCode);
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
                HandleRequestExceptionAsync(httpContext, httpStatusCode);
            }

        }

        private static void HandleRequestExceptionAsync(HttpContext context, HttpStatusCode statusCode)
        {
            context.Response.StatusCode = (int)statusCode;
        }
       
    }
}
