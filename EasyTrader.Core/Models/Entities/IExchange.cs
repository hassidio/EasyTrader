using EasyTrader.Core.Connectors;

namespace EasyTrader.Core.Models.Entities
{
    public interface IExchange : IApiRequestor, IEntity
    {
        string EntityId { get; set; }

        long ServerTime { get; set; }

        ICollection<MarketInfo> MarketsInfoList { get; set; }

        ICollection<string> MarketNames { get; }

        MarketInfo GetMarketInfoById(string id);

    }
}