using EasyTrader.Core.Connectors;

namespace EasyTrader.Core.Models.Entities
{
    public interface IMarket : IApiRequestor, IEntity
    {
        MarketInfo MarketInfo { get; set; }

        string ExchangeName { get; set; }

        IExchange Exchange { get; }

        IList<dynamic> ClientDataList { get; }

        IList<ICandle> Candles { get; }

        IList<TCandle> GetCandles<TCandle>() where TCandle : ICandle, new();

        void RefreshCandles<TCandle>() where TCandle : ICandle, new();

        void AppendClientCandlesData<TMarket, TCandle>(TMarket newMarket)
            where TMarket : IMarket, new()
            where TCandle : ICandle, new();

        void AddCandle<TCandle>(TCandle candle) where TCandle : ICandle, new();

        void OnAddCandle<TCandle>(TCandle candle) where TCandle : ICandle, new();

    }
}