using EasyTrader.Core.DataServices;
using EasyTrader.Core.Models.Entities;

namespace EasyTrader.Services.DataServices
{
    public class ExchangeDataManager
    {
        public ExchangeDataManager(ITraderDataService traderDataService)
        {
            TraderDataService = traderDataService;

        }

        public ITraderDataService TraderDataService { get; set; }

        public ICollection<string> MarketNames
        {
            get { return TraderDataService.TraderData.Trader.Tasks.DefaultExchange.MarketNames; }
        }

        public IMarket GetMarket(string marketName)
        {
            return TraderDataService.TraderData.Trader.Tasks.GetMarket(marketName);
        }
    }
}