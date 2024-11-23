using EasyTrader.Core.Views.PropertyView;
using Newtonsoft.Json;
using System.ComponentModel;

namespace EasyTrader.Core.Models.Entities
{
    public abstract class Candle : ICandle
    {
        public Candle()
        {
            PropertyBag = new Dictionary<string, object>();
        }

        [PropertyData("Id")]
        public string Id { get; set; }

        [PropertyData("Market Id")]
        public string MarketId { get; set; }

        [PropertyData("Open Price")]
        public double OpenPrice { get; set; }

        [PropertyData("Close Price")]
        public double ClosePrice { get; set; }

        [PropertyData("High Price")]
        public double HighPrice { get; set; }

        [PropertyData("Low Price")]
        public double LowPrice { get; set; }

        [PropertyData("Open Date Time")]
        public DateTime OpenDateTime { get; set; }

        [PropertyData("Close Date Time")]
        public DateTime CloseDateTime { get; set; }

        [PropertyData("Number of Trades")]
        public long NumberOfTrades { get; set; }

        [PropertyData("Volume")]
        public double Volume { get; set; }

        [DisplayName("Properties")]
        public IDictionary<string, object> PropertyBag { get; set; }

        //[DisplayName ("")]
        //public double AvaragePrice { get { return (HighPrice + LowPrice) / 2; } }

        [JsonIgnore]
        public dynamic ClientCandleData { get; set; }


        public void SetCandle(string marketId, dynamic clientCandleData)
        {
            MarketId = marketId;
            ClientCandleData = clientCandleData;

            OnSetCandle(clientCandleData);
        }

        public abstract void OnSetCandle(dynamic clientCandleData);

        //
        // Summary:
        //     Compares the current instance with another object of the same type and returns
        //     an integer that indicates whether the current instance precedes, follows, or
        //     occurs in the same position in the sort order as the other object.
        //
        // Parameters:
        //   other:
        //     An object to compare with this instance.
        //
        // Returns:
        //     A value that indicates the relative order of the objects being compared. The
        //     return value has these meanings:
        //
        //     Value – Meaning
        //     Less than zero – This instance precedes other in the sort order.
        //     Zero – This instance occurs in the same position in the sort order as other.
        //
        //     Greater than zero – This instance follows other in the sort order.
        public int CompareTo(ICandle? otherCandle)
        {
            if(otherCandle is null) { return 0; }
            if (this.OpenDateTime > otherCandle.OpenDateTime) { return 1; }
            if (this.OpenDateTime < otherCandle.OpenDateTime) { return -1; }
            return 0;
        }
    }
}
