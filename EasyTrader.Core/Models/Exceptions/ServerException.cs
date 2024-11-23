using EasyTrader.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTrader.Core.Models.Exceptions
{
    public class ServerException : HttpException
    {
        public ServerException()
        : base()
        {
        }

        public ServerException(string message)
        : base(message)
        {
            Message = message;
        }

        public ServerException(SeverityEnum severity, string message, Exception innerException)
        : base(severity, message, innerException)
        {
            Message = message;
        }

        public new string Message { get; protected set; }
    }


}
