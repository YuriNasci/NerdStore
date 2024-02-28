using System;
using System.Net;

namespace NerdStore.Core.DomainObjects
{
    public class DomainException : Exception
    {
        public HttpStatusCode StatusCode;

        public DomainException()
        { }

        public DomainException(string message) : base(message)
        { }

        public DomainException(string message, Exception innerException) : base(message, innerException)
        { }

        public DomainException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}