﻿using Microsoft.AspNetCore.Http;
using Polly.CircuitBreaker;
using System.Net;
using System.Threading.Tasks;
using System;
using Refit;
using Grpc.Core;
using System.Net.Http;

namespace NerdStore.WebApp.MVC.Extensions
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
            catch (CustomHttpRequestException ex)
            {
                HandleRequestExceptionAsync(httpContext, ex.StatusCode);
            }
            catch (ValidationApiException ex)
            {
                HandleRequestExceptionAsync(httpContext, ex.StatusCode);
            }
            catch (ApiException ex)
            {
                HandleRequestExceptionAsync(httpContext, ex.StatusCode);
            }
            catch (BrokenCircuitException)
            {
                HandleCircuitBreakerExceptionAsync(httpContext);
            }
            catch (RpcException ex)
            {
                //400 Bad Request	    INTERNAL
                //401 Unauthorized      UNAUTHENTICATED
                //403 Forbidden         PERMISSION_DENIED
                //404 Not Found         UNIMPLEMENTED

                var statusCode = ex.StatusCode switch
                {
                    StatusCode.Internal => 400,
                    StatusCode.Unauthenticated => 401,
                    StatusCode.PermissionDenied => 403,
                    StatusCode.Unimplemented => 404,
                    _ => 500
                };

                var httpStatusCode = (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), statusCode.ToString());
                HandleRequestExceptionAsync(httpContext, httpStatusCode);
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
            if (statusCode == HttpStatusCode.Unauthorized)
            {
                //if (_authService.TokenExpirado())
                //{
                //    if (_authService.RefreshTokenValido().Result)
                //    {
                //        context.Response.Redirect(context.Request.Path);
                //        return;
                //    }
                //}

                //_authService.Logout();
                context.Response.Redirect($"/login?ReturnUrl={context.Request.Path}");
                return;
            }

            context.Response.StatusCode = (int)statusCode;
        }

        private static void HandleCircuitBreakerExceptionAsync(HttpContext context)
        {
            context.Response.Redirect("/sistema-indisponivel");
        }
    }
}
