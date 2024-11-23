using EasyTrader.Core.Models.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EasyTrader.Core.Models.Binance
{
    public class BinanceCandle : Candle
    {
        public BinanceCandle() : base() { }

        public override void OnSetCandle(dynamic clientCandleData)
        {
            JToken klineArray = clientCandleData;

            PropertyBag = new Dictionary<string, object>
            {
                { BinanceCandlePropertyBagKey.QuoteAssetVolume.ToString(), null },
                { BinanceCandlePropertyBagKey.TakerBuyBaseAssetVolume.ToString(), null },
                { BinanceCandlePropertyBagKey.TakerBuyQuoteAssetVolume.ToString(), null },
            };

            var openTimeUnixEpoch = (long)klineArray[0];
            var openPrice = (string)klineArray[1];
            var highPrice = (string)klineArray[2];
            var lowPrice = (string)klineArray[3];
            var closePrice = (string)klineArray[4];
            var volume = (string)klineArray[5];
            var closeTimeUnixEpoch = (long)klineArray[6];
            var quoteAssetVolume = (string)klineArray[7];// i.e. volume of BTC in ETHBTC
            var numberOfTrades = (long)klineArray[8];
            var takerBuyBaseAssetVolume = (string)klineArray[9]; //
            var takerBuyQuoteAssetVolume = (string)klineArray[10]; //

            Id = MarketId + "_" + openTimeUnixEpoch;
            OpenPrice = Convert.ToDouble(openPrice);
            ClosePrice = Convert.ToDouble(closePrice);
            HighPrice = Convert.ToDouble(highPrice);
            LowPrice = Convert.ToDouble(lowPrice);
            OpenDateTime = DateTime.UnixEpoch.AddMilliseconds(openTimeUnixEpoch);
            CloseDateTime = DateTime.UnixEpoch.AddMilliseconds(closeTimeUnixEpoch);
            NumberOfTrades = numberOfTrades;
            Volume = Convert.ToDouble(volume);

            PropertyBag[BinanceCandlePropertyBagKey.QuoteAssetVolume.ToString()] = quoteAssetVolume;
            PropertyBag[BinanceCandlePropertyBagKey.TakerBuyBaseAssetVolume.ToString()] = takerBuyBaseAssetVolume;
            PropertyBag[BinanceCandlePropertyBagKey.TakerBuyQuoteAssetVolume.ToString()] = takerBuyQuoteAssetVolume;
        }
    }

    public enum BinanceCandlePropertyBagKey
    {
        QuoteAssetVolume,
        TakerBuyBaseAssetVolume,
        TakerBuyQuoteAssetVolume
    }
}
