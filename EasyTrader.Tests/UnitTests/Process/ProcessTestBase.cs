using EasyTrader.Core.Common;
using EasyTrader.Tests.Models;
using EasyTrader.Core.Processes;
using EasyTrader.Tests.Models.MockClientApi;
using EasyTrader.Core.Connectors;
using EasyTrader.Core;
using System.Diagnostics;

namespace EasyTrader.Tests.UnitTests.Process
{
    [TestClass]
    public class ProcessTestBase : UnitTestBase
    {

        public ProcessTestBase()
        {
            MockApiController = new ApiController<MockApi>(new Throttle());

            MockTraderProcess =
                new TraderProcess<
                    MockExchange,
                    MockExchangeClientInfo,
                    MockMarket,
                    IList<MockCandle>,
                    MockCandle,
                     MockApi>(
                    persistenceService: MockPersistenceService,
                    exchangeConfiguration: MockExchangeConfiguration,
                    apiController: MockApiController,
                    clientOutput: MockClientOutput);

            MockTraderProcess.ApiController.Api.ApiHttpResponse.HttpResponse =
                new HttpResponseMessage(System.Net.HttpStatusCode.OK);

            MockTraderProcess.ApiController.Api.ApiHttpResponse.HttpResponse.Headers.Add(
                ResponseHeaderWeightKeyNameTest,
                [ResponseHeaderWeightValueTest.ToString()]);
        }

        protected ApiController<MockApi> MockApiController { get; private set; }

        protected TraderProcess<MockExchange, MockExchangeClientInfo, MockMarket, IList<MockCandle>, MockCandle, MockApi>
            MockTraderProcess
        { get; private set; }

        public MockMarket RefreshDefaultDataFile()
        {
            var market = NewMarket();

            MockPersistenceService.Save(market);

            return market;
        }

    }
}