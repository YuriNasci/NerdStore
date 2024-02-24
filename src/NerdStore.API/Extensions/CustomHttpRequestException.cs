using System;
using System.Net;

namespace NerdStore.API.Extensions
{
    public class CustomHttpRequestException : Exception
    {
        public HttpStatusCode StatusCode;        

        public CustomHttpRequestException()
        {
        }

        public CustomHttpRequestException(string message) : base(message)
        {
        }

        public CustomHttpRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public CustomHttpRequestException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
    }
}