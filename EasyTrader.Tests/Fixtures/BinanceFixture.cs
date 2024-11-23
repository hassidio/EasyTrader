using EasyTrader.Core.Common;
using EasyTrader.Core.Models.Binance;
using EasyTrader.Core.Configuration;
using EasyTrader.Core.Connectors.Http;
using EasyTrader.Core;

namespace EasyTrader.Tests.Fixtures
{
    [TestClass]
    public class BinanceFixture : FixtureBase
    {
        public BinanceFixture() { }

        [TestMethod]
        public void Fixture_UpdateExchangeInfo()
        {
            Log.Debug("Start Test: Fixture_UpdateExchangeInfo");
            var requestOptions = new ApiRequestOptions
            {
                ResponseHeaderWeightKeyName =
                    ExchangeConfiguration.ApiConfiguration.ResponseHeaderWeightKeyName,
            };

            var binanceExchange = Process.UpdateExchangeInfoProcess(requestOptions);

            Assert.IsTrue(binanceExchange.MarketNames.Count > 0);
            Assert.IsTrue(binanceExchange.Created.Ticks > 0);
        }

        [TestMethod]
        public void Fixture_UpdateMarketFromApi()
        {
            Log.Debug("Start Test: Fixture_UpdateMarketFromApi");

            // Setup
            var marketId = "ETHBTC";
            var interval = "15m";
            //var limit = "100";

            var requestOptions = new ApiRequestOptions
            {
                ApiRequestQueryOptions =
                new Dictionary<string, string>
                {
                    { "symbol", marketId  },
                    { "interval", interval },
                    //{ "limit", limit }
                },
                ResponseHeaderWeightKeyName =
                    ExchangeConfiguration.ApiConfiguration.ResponseHeaderWeightKeyName,
            };

            var marketBefore = Process.PersistenceService.Get<BinanceMarket>(
                    marketId, Process.DefaultExchange.EntityId);

            // Test
            var marketAfterAppend =
                Process.UpdateMarketCandlesProcess(marketId, requestOptions);

            Assert.IsTrue(marketAfterAppend.MarketInfo.Id == marketId);
            Assert.IsTrue(marketAfterAppend.MarketInfo.MarketName == marketId);
            Assert.IsTrue(marketAfterAppend.GetCandles<BinanceCandle>().Count > 0);

            if (marketBefore is not null)
            {
                var marketAfterCount = marketAfterAppend.GetCandles<BinanceCandle>().Count;
                var marketBeforeCount = marketBefore.GetCandles<BinanceCandle>().Count;

                Assert.IsTrue(marketAfterCount >= marketBeforeCount);
            }
        }
    }
}