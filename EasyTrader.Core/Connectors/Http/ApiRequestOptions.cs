using EasyTrader.Core.Configuration;

namespace EasyTrader.Core.Connectors.Http
{
    public class ApiRequestOptions
    {
        public ApiRequestOptions(ExchangeConfiguration? exchangeConfiguration = null)
        {
            if(exchangeConfiguration is not null)
            {
                ResponseHeaderWeightKeyName =
                    exchangeConfiguration.ApiConfiguration.ResponseHeaderWeightKeyName;

            }
        }


        public Dictionary<string, string>? ApiRequestQueryOptions { get; set; }
        public Dictionary<string, string>? ApiRequestHeadersOptions { get; set; }
        public object? ApiRequestContentOptions { get; set; }
        public string ResponseHeaderWeightKeyName { get; set; }

    }
}
