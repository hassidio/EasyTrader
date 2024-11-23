using EasyTrader.Tests.Models;
using EasyTrader.Tests.Models.MockClientApi;

namespace EasyTrader.Tests.UnitTests.Entities
{
    [TestClass]
    public class ExchangeTests : UnitTestBase
    {
        [TestMethod()]
        public void SetExchangeInfo_Test()
        {
            var exchange = new MockExchange();

            Assert.IsTrue(exchange.MarketsInfoList.Count == 0);

            exchange.SetEntity(
                id: ExchangeNameTest,
                rawClientData: new MockExchangeClientInfo(NewMockApiHttpRequest(exchange)));

            Assert.IsTrue(exchange.EntityId == ExchangeNameTest);
            Assert.IsTrue(exchange.MarketsInfoList.Count > 0);
        }

        [TestMethod()]
        public void GetMarketInfoById_Test()
        {
            var marketInfo = ExchangeTest.GetMarketInfoById(MarketIdTest);

            Assert.IsTrue(marketInfo is not null);
            Assert.IsTrue(marketInfo.Id == MarketIdTest);

            var marketInfo2 = ExchangeTest.GetMarketInfoById("wrong market id");
            Assert.IsTrue(marketInfo2 is null);
        }
    }
}
