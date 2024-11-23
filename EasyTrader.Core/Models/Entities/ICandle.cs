using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace EasyTrader.Core.Models.Entities
{
    public interface ICandle : IComparable<ICandle>
    {
        string Id { get; set; }

        string MarketId { get; set; }

        dynamic ClientCandleData { get; set; }

        double OpenPrice { get; set; }

        double ClosePrice { get; set; }

        double HighPrice { get; set; }

        double LowPrice { get; set; }

        DateTime OpenDateTime { get; set; }

        DateTime CloseDateTime { get; set; }

        long NumberOfTrades { get; set; }

        double Volume { get; set; }

        IDictionary<string, object> PropertyBag { get; set; }


        void SetCandle(string marketId, dynamic clientCandleData);

        void OnSetCandle(dynamic clientCandleData);
    }
}