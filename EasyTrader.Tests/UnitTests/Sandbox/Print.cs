using EasyTrader.Core.Models.Entities;
using System.Diagnostics;

namespace EasyTrader.Tests.UnitTests.Sandbox
{
    public static class Print
    {
        private static string _nulltxt = @"null";
        private static string _truetxt = @"true";
        private static int _candlePrintlimit = 10;

        internal static void DebugMarket<TMarket, TCandle>(TMarket market, string? title = null, bool isDebugCandles = false)
           where TMarket : IMarket
           where TCandle : ICandle, new()
        {
            var tabs = 1;
            if (title is not null) { WriteLine($"*** {title}", 0); }

            if (market is null)
            {
                WriteLine(_nulltxt, tabs);
                return;
            }

            var candles = market.GetCandles<TCandle>();
            WriteLine($"Default exchange configurations market name: {market.ExchangeName}", tabs);
            WriteLine($"Marke name: {market.MarketInfo.MarketName}", tabs);
            WriteLine($"First Open Date Time: {candles.First().OpenDateTime}", tabs);
            WriteLine($"Last Open Date Time: {candles.Last().OpenDateTime}", tabs);
            WriteLine($"Candle count: {candles.Count}", tabs);
            WriteLine($"", tabs);

            if (isDebugCandles)
            {
                tabs++;
                DebugCandles(candles, tabs);
            }
        }

        public static void DebugCandles<TCandle>(IList<TCandle> candles, int tabs = 0, string? title = null)
            where TCandle : ICandle, new()
        {
            if (title is not null) { WriteLine($"* {title}", tabs); tabs++; }

            if (candles is null)
            {
                WriteLine(_nulltxt, tabs);
                WriteLine("");
                return;
            }

            WriteLine($"Candles [{candles.Count}]:", tabs);

            var count = 0;
            foreach (var candle in candles)
            {
                count++;
                if (count <= _candlePrintlimit) { DebugCandle(candle, tabs + 1); }
            }
            WriteLine($"");
        }

        public static void DebugCandle<TCandle>(TCandle candle, int tabs = 0, string? title = null)
            where TCandle : ICandle, new()
        {
            if (title is not null) { WriteLine($"* {title}", tabs); }

            if (candle is null)
            {
                WriteLine(_nulltxt, tabs);
                return;
            }

            var candleDataTxt = candle.ClientCandleData is null ?
                _nulltxt : _truetxt;
            var propertyBagsTxt = candle.PropertyBag is null ?
                _nulltxt : candle.PropertyBag.Count.ToString();

            WriteLine($"Candle EntityNameId: {candle.Id}", tabs);
            WriteLine($"Market EntityNameId: {candle.MarketId}", tabs);
            WriteLine($"Client Candle Data: {candleDataTxt}", tabs);

            WriteLine($"Open date time: {candle.OpenDateTime}", tabs);
            WriteLine($"Close date time: {candle.CloseDateTime}", tabs);
            WriteLine($"Open price: {candle.OpenPrice}", tabs);
            WriteLine($"Close price: {candle.ClosePrice}", tabs);
            WriteLine($"High price: {candle.HighPrice}", tabs);
            WriteLine($"Low price: {candle.LowPrice}", tabs);
            WriteLine($"Volume: {candle.Volume}", tabs);
            WriteLine($"Number of trades: {candle.NumberOfTrades}", tabs);

            WriteLine($"Properties: {propertyBagsTxt}", tabs);

            foreach (var p in candle.PropertyBag)
            { WriteLine($"{p.Key}: {p.Value}", tabs + 1); }

            WriteLine($"");
        }

        public static void WriteLine(string txt, int tabs = 0)
        {
            var tabPrefix = string.Empty;

            for (int i = 0; i < tabs; i++)
            {
                tabPrefix += "\t";
            }
            Debug.WriteLine(tabPrefix + txt);
        }
    }

}
