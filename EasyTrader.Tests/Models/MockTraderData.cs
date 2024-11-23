using EasyTrader.Core.Configuration;
using EasyTrader.Core.DataServices;
using EasyTrader.Core.Views.OutputView;
using EasyTrader.Tests.Models.MockClientApi;

namespace EasyTrader.Tests.Models
{
    public class MockTraderData
        : TraderData<MockExchange, MockExchangeClientInfo, MockMarket, IList<MockCandle>, MockCandle,  MockApi>
    {
        public MockTraderData(
            ExchangeConfiguration exchangeConfiguration,
            IClientOutput? clientOutput = null)
            : base(exchangeConfiguration, clientOutput)
        {

        }




    }
}



