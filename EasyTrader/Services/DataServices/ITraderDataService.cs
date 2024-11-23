using EasyTrader.Core.Connectors;
using EasyTrader.Core.Models.Entities;

namespace EasyTrader.Core.DataServices
{
    public interface ITraderDataService
    {
        dynamic TraderData { get; }

        ITraderData<TdsExchange, TdsClientExchange, TdsMarket, TdsClientMarket, TdsCandle, TdsApi>
            GetTraderData<TdsExchange, TdsClientExchange, TdsMarket, TdsClientMarket, TdsCandle, TdsApi>()
                where TdsExchange : IExchange, new()
                where TdsMarket : IMarket, new()
                where TdsCandle : ICandle, new()
                where TdsApi : IApi, new();


    }
}