using EasyTrader.Core.Models.Exceptions;

namespace EasyTrader.Core.Connectors
{
    public interface IApiHttpExceptionHandler
    {
        Task<HttpException> GetApiHttpException(HttpResponseMessage response);
    }
}