using EasyTrader.Core.Configuration;
using EasyTrader.Core.Models;
using EasyTrader.Core.Models.Exceptions;
using System;
using System.Text;

namespace EasyTrader.Core.Common
{
    public class CommonException : Exception
    {
        /// <summary>
        /// Used for HttpException
        /// </summary>
        public CommonException()
            : base() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="severity">Error, Warrning, Information or Debug</param>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public CommonException(SeverityEnum severity, string message, Exception innerException)
        : base(message, innerException)
        {
            if (TraderGlobals.Configurations is not null)
            {
                Severity = severity;

                if (Severity == SeverityEnum.Error) { Log.Error(this); }
                if (Severity == SeverityEnum.Warning) { Log.Warning(this); }
                if (Severity == SeverityEnum.Information) { Log.Information(this); }
            }
        }

        /// <summary>
        /// Used for HttpException. Default Severity is Error.
        /// </summary>
        public CommonException(string message)
            : this(SeverityEnum.Error, message, null) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="severity"></param>
        /// <param name="message"></param>
        public CommonException(SeverityEnum severity, string message)
        : this(severity, message, null) { }

        public SeverityEnum Severity { get; set; }

        public string FullExceptionDescription
        {
            get { return GetFullExceptionDescription(this); }
        }

        public static string GetFullExceptionDescription(AggregateException aggregateException)
        {
            if (aggregateException is null) { return null; }

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(aggregateException.Message);

            if (aggregateException.StackTrace is not null)
            {
                sb.AppendLine($"{aggregateException.StackTrace}");
            }

            foreach (var exception in aggregateException.InnerExceptions)
            {
                sb.AppendLine($"Inner Exception: {GetFullExceptionDescription(exception)}");
            }

            return sb.ToString();
        }

        public static string GetFullExceptionDescription(Exception exception)
        {
            if (exception is null) { return null; }

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(GetExceptionMessage(exception));

            if (exception.StackTrace is not null)
            {
                sb.AppendLine($"{exception.StackTrace}");
            }

            if (exception.InnerException != null)
            {
                sb.AppendLine($"Inner Exception: {GetFullExceptionDescription(exception.InnerException)}");
            }

            return sb.ToString();
        }

        private static string GetExceptionMessage(Exception exception)
        {
            var message = exception.Message;
            if (exception.GetType() == typeof(ClientException))
            {
                message += $"(code: {((ClientException)exception).StatusCode})";
            }

            return message;
        }







        // Common Exception Messages
        public static CommonException ArchiveFailed()
        { return new CommonException(SeverityEnum.Warning, $"Could not archive _exchange file."); }

        public static CommonException ConfigurationNull()
        { return new CommonException(SeverityEnum.Error, $"Could not find configurations."); }

        public static CommonException CouldNotReadFile(string filename, Exception e)
        { return new CommonException(SeverityEnum.Error, $"\r\nCould not read file: '{filename}'.", e); }

        public static CommonException ExchangeConfigNotFound(string exchaneName, string appConfigFileName)
        {
            return new CommonException(SeverityEnum.Error, $"\r\nCould not find _exchange node named " +
            $"'{exchaneName}' in configuration file: '{appConfigFileName}'. Confirm _exchange name is correct.");
        }

        public static CommonException ExchangeFileNotFound(string exchangeName)
        {
            return new CommonException(
                SeverityEnum.Error, 
                $"\r\nCould not find _exchange file: '{exchangeName}' in data directory. " +
                $"Confirm exchange has been updated.");
        }

        public static CommonException MarketFileNotFound(string fileName)
        {
            return new CommonException(
                SeverityEnum.Error, 
                $"\r\nCould not find market file: '{fileName}' in data directory. " +
                $"Confirm market has been updated.");
        }

        public static CommonException UnknownApi(string apiType)
        { return new CommonException(SeverityEnum.Error, $"Unknown ApiController type: '{apiType}'."); }

        public static CommonException ApiCallFailed(string marketId)
        { return new CommonException(SeverityEnum.Error, $"ApiController call operation failed. Market id '{marketId}'."); }

        public static CommonException ThrottleConfigurationError(string field)
        { return new CommonException(SeverityEnum.Error, $"Throttle configuration error '{field}'."); }

        public static CommonException InvalidResponseHeaderWeight(
            string ResponseHeaderWeightKeyName,
            string? ResponseHeaderWeightKeyValue,
            Exception ex)
        {
            return new CommonException(
                SeverityEnum.Error,
                $"Invalid Response Header Weight Key EntityId: '{ResponseHeaderWeightKeyName}' Value: '{ResponseHeaderWeightKeyValue}'.",
                ex);
        }


        // Http/Client Exception Messages
        public static ClientException UnsuccessfulResponse(int code)
        { return new ClientException(SeverityEnum.Error, $"Unsuccessful response with no content.", code); }
    }
}
