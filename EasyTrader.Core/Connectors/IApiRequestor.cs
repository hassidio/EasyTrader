using EasyTrader.Core.Configuration;
using EasyTrader.Core.Connectors.Http;
using EasyTrader.Core.Models.Exceptions;
using System;

namespace EasyTrader.Core.Connectors
{
    public interface IApiRequestor : IApiHttpExceptionHandler
    {
        ApiHttpRequest GetApiRequest(
         string baseUrl,
         string? pathUri = null,
         ApiRequestOptions? requestOptions = null);


        Dictionary<string, string>? ApiRequestClientQuery { get; }
        Dictionary<string, string>? ApiRequestClientHeaders { get; }
        object? GetApiRequestClientContent(object? requestContent = null);
        int GetResponseWeight(ApiHttpRequest apiHttpRequest, ApiHttpResponse apiHttpResponse);
    }
}
