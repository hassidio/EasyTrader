using EasyTrader.Tests.Models;

namespace EasyTrader.Tests.UnitTests.Entities
{
    [TestClass()]
    public class CandleTests : UnitTestBase
    {


        [TestMethod()]
        public void SetCandle_Test()
        {
            var candle = new MockCandle();
            Assert.IsTrue(candle.MarketId is null);
            Assert.IsTrue(candle.ClientCandleData is null);

            candle.SetCandle(MarketIdTest, NewDefaultCandle("Candle test id", DateTime.Now));

            Assert.IsTrue(candle.MarketId == MarketIdTest);
            Assert.IsTrue(candle.ClientCandleData is not null);
            Assert.IsTrue(((MockCandle)candle.ClientCandleData).Id == "Candle test id");
        }
    }
}