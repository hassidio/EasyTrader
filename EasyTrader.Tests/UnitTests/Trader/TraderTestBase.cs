using EasyTrader.Core.Views.OutputView;
using EasyTrader.Tests.Models;

namespace EasyTrader.Tests.UnitTests.Trader
{
    [TestClass]
    public class TraderTestBase : UnitTestBase
    {
        public TraderTestBase()
        {
            MockTraderData = new MockTraderData(MockExchangeConfiguration, MockClientOutput);
        }

        protected MockTraderData MockTraderData { get; private set; }
    }
}