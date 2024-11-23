using EasyTrader.Core.Configuration;
using EasyTrader.Core.Connectors;
using EasyTrader.Core.Models.Entities;

namespace EasyTrader.Core.DataServices
{
    public interface ITraderData<TExchange, TClientExchange, TMarket, TClientMarket, TCandle, TApi>
        where TExchange : IExchange, new()
        where TMarket : IMarket, new()
        where TCandle : ICandle, new()
        where TApi : IApi, new()
    {
        ExchangeConfiguration ExchangeConfiguration { get; set; }

        Trader<TExchange, TClientExchange, TMarket, TClientMarket, TCandle, TApi> Trader { get; }

    }
}

