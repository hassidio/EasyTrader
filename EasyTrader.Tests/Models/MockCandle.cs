using EasyTrader.Core.Models.Entities;
using EasyTrader.Tests.UnitTests;
using Newtonsoft.Json.Linq;

namespace EasyTrader.Tests.Models
{
    public class MockCandle : Candle
    {
        public MockCandle() : base() 
        {
        }

        public static explicit operator JToken(MockCandle candle)
        {
            return JToken.FromObject(candle);
        }

        public static explicit operator MockCandle(JToken token)
        {
            return token.ToObject<MockCandle>();
        }

        public override void OnSetCandle(dynamic clientCandleData)
        {
            var c = (MockCandle)clientCandleData;
            Id = c.Id;
            OpenPrice = c.OpenPrice;
            ClosePrice = c.ClosePrice;
            HighPrice = c.HighPrice;
            LowPrice = c.LowPrice;
            OpenDateTime = c.OpenDateTime;
            CloseDateTime = c.CloseDateTime;
            NumberOfTrades = c.NumberOfTrades;
            Volume = c.Volume;
            PropertyBag = c.PropertyBag;
        }

        public void IncreaseCandleTime(long interval)
        {
            OpenDateTime = OpenDateTime.AddMinutes(interval);
            CloseDateTime = CloseDateTime.AddMinutes(interval);
        }

        public void IncreaseCandleTime(MockCandle candle, long interval)
        {
            OpenDateTime = candle.OpenDateTime.AddMinutes(interval);
            CloseDateTime = candle.CloseDateTime.AddMinutes(interval);
        }

        public static dynamic NewMockCandle(
           string clientId,
           double openPrice,
           double closePrice,
           double highPrice,
           double lowPrice,
           DateTime dateTime,
           long numberOfTrades,
           double volume,
           object[] prop = null)
        {
            var mockCandle = new MockCandle()
            {
                Id = clientId,
                OpenPrice = openPrice,
                ClosePrice = closePrice,
                HighPrice = highPrice,
                LowPrice = lowPrice,
                OpenDateTime = dateTime,
                CloseDateTime = dateTime.AddMinutes(10),
                NumberOfTrades = numberOfTrades,
                Volume = volume
            };

            if (prop is not null)
            {
                mockCandle.PropertyBag[CandleMock_PropertyBagKey.QuoteAssetVolume.ToString()] = prop[0];
                mockCandle.PropertyBag[CandleMock_PropertyBagKey.Property2.ToString()] = prop[1];
            }
            
            return mockCandle;
        }
    }

    internal enum CandleMock_PropertyBagKey
    {
        QuoteAssetVolume,
        Property2
    }
}
