using EasyTrader.Core.Connectors;
using EasyTrader.Core.Connectors.Http;
using EasyTrader.Core.Models.Binance;
using EasyTrader.Core.Models.Entities;
using EasyTrader.Tests.Models;
using EasyTrader.Tests.Models.MockClientApi;
using Newtonsoft.Json.Linq;

namespace EasyTrader.Core.DataServices
{
    public class TraderDataService : ITraderDataService
    {
        public TraderDataService(
            TraderData<
                BinanceExchange, 
                BinanceApi_ExchangeInfo, 
                BinanceMarket, 
                JArray, 
                BinanceCandle, 
                RestApi> traderData)
        {
            TraderData = traderData;
        }

        public TraderDataService(
            TraderData<
                MockExchange, 
                MockExchangeClientInfo, 
                MockMarket, 
                IList<MockCandle>, 
                MockCandle,
                MockApi> traderData)
        {
            TraderData = traderData;
        }

        public dynamic TraderData { get; private set; }

        public ITraderData<TdsExchange, TdsClientExchange, TdsMarket, TdsClientMarket, TdsCandle, TdsApi>
            GetTraderData<TdsExchange, TdsClientExchange, TdsMarket, TdsClientMarket, TdsCandle, TdsApi>()
                where TdsExchange : IExchange, new()
                where TdsMarket : IMarket, new()
                where TdsCandle : ICandle, new()
                where TdsApi : IApi, new()
        {
            return (ITraderData<TdsExchange, TdsClientExchange, TdsMarket, TdsClientMarket, TdsCandle, TdsApi>)TraderData;
        }
    }
}
