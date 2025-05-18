using System.Net;

namespace Base_API.Infrastructure.Exceptions
{
    public class BusinessException(string message, HttpStatusCode statusCode) : Exception(message)
    {
        public HttpStatusCode StatusCode { get; } = statusCode;
    }
}
