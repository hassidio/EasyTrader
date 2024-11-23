using EasyTrader.Core.Views.OutputView;
using EasyTrader.Core.Connectors;
using EasyTrader.Core.Models.Entities;
using EasyTrader.Core.Configuration;
using EasyTrader.Core.PersistenceServices;
using EasyTrader.Core.PersistenceServices.IO;

namespace EasyTrader.Core.DataServices
{
    public class TraderData<TExchange, TClientExchange, TMarket, TClientMarket, TCandle, TApi>
        : ITraderData<TExchange, TClientExchange, TMarket, TClientMarket, TCandle, TApi>
        where TExchange : IExchange, new()
        where TMarket : IMarket, new()
        where TCandle : ICandle, new()
        where TApi : IApi, new()
    {
        public TraderData(
            ExchangeConfiguration exchangeConfiguration,
            IClientOutput clientOutput)
        {
            ExchangeConfiguration = exchangeConfiguration;

            if (_persistenceService is null)
            {
                _persistenceService = new JsonDataFilesPersistenceService(ExchangeConfiguration.DataDirectoryPath);
            }

            Trader = new Trader<TExchange, TClientExchange, TMarket, TClientMarket, TCandle, TApi>(
                _persistenceService, exchangeConfiguration, clientOutput);
        }

        private IPersistenceService _persistenceService;
        public ExchangeConfiguration ExchangeConfiguration { get; set; }

        public Trader<TExchange, TClientExchange, TMarket, TClientMarket, TCandle, TApi> Trader { get; private set; }

        //public MarketView GetMarketView(string marketId)
        //{
        //    var market = Trader.Tasks.GetMarket(marketId);
        //    var marketView = new MarketView(market);

        //    return marketView;
        //}

    }

    //public class MarketView
    //{
    //    public IMarket Market { get; private set; }

    //    public MarketView(IMarket market)
    //    {
    //        Market = market;
    //    }
    //}


}
