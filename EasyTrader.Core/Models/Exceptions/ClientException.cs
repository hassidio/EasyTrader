using EasyTrader.Core.Common;

namespace EasyTrader.Core.Models.Exceptions
{
    public class ClientException : HttpException
    {

        public ClientException()
        : base()
        {
        }

        public ClientException(string message, int code)
        : base(message)
        {
            Code = code;
            StatusCode = code;
            Message = message;
        }

        public ClientException(SeverityEnum severity, string message, int code, Exception innerException)
        : base(severity, message, innerException)
        {
            Code = code;
            StatusCode = code;
            Message = message;
        }

        public ClientException(SeverityEnum severity, string message, int code)
        : this(severity, message, code, null)
        {}



        [Newtonsoft.Json.JsonProperty("code")]
        public int Code { get; set; }

        [Newtonsoft.Json.JsonProperty("msg")]
        public new string Message { get; protected set; }
    }
}
