using System.Net;

namespace Commerce.Core.Exceptions;

public class AppException : Exception
{
    public HttpStatusCode StatusCode { get; }

    public AppException(string message, HttpStatusCode statusCode) : base(message)
    {
        this.StatusCode = statusCode;
    }
}
