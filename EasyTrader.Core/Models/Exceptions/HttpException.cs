using EasyTrader.Core.Common;

namespace EasyTrader.Core.Models.Exceptions
{
    public class HttpException : CommonException
    {
        public string HttpExceptionResponseContent { get; set; }

        public HttpException()
        : base()
        {
        }

        public HttpException(string message)
        : base(message)
        {
        }

        public HttpException(SeverityEnum severity, string message, Exception innerException)
        : base(severity, message, innerException)
        {
        }

        public int StatusCode { get; set; }

        public IDictionary<string, IEnumerable<string>> Headers { get; set; }
    }
}
