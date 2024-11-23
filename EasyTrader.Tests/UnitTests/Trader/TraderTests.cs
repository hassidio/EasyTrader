using EasyTrader.Core.Common;
using EasyTrader.Tests.Models;
using EasyTrader.Core;
using EasyTrader.Tests.Models.Views;
using EasyTrader.Core.Views.OutputView.OutputWindowView;
using EasyTrader.Core.Views.OutputView.ApiThrottleWindowView;

namespace EasyTrader.Tests.UnitTests.Trader
{
    [TestClass()]
    public class TraderTests : TraderTestBase
    {
        public IClientOutputWindow TestClientOutputWindow
        { get { return MockTraderData.Trader.ClientOutput.ClientOutputWindow; } }

        public IClientApiThrottleWindow TestClientApiThrottleWindow
        { get { return MockTraderData.Trader.ClientOutput.ClientApiThrottleWindow; } }


        [TestMethod()]
        public void TraderTest()
        {
            Assert.IsTrue(MockConfigurations is not null);

            Assert.IsTrue(MockTraderData.Trader.ApiController.Api.GetType() == typeof(MockApi));

            Assert.IsTrue(TestClientOutputWindow.OutputWindow is not null);
        }

        #region ApiController Error

        [TestMethod]
        public void UpdateExchangeInfoTask_ApiError_Test()
        {
            var code = 429;
            // Setup
            MockTraderData.Trader.ApiController.Api.ExceptionCode = code;

            //Test
            Task<MockExchange> task = MockTraderData.Trader.Tasks.UpdateExchangeInfoAsync(NewApiRequestOptions());

            task.Wait();

            Assert.IsTrue(TestClientOutputWindow.OutputWindow.Count == 2);
            Assert.IsTrue(TestClientOutputWindow.OutputWindow[0].Status == OutputItemStatusEnum.Fail);
            Assert.IsTrue(TestClientOutputWindow.OutputWindow[1].Status == OutputItemStatusEnum.Fail);
            Assert.IsTrue(TestClientOutputWindow.OutputWindow[1].Text.Contains(code.ToString()));
        }

        #endregion

        #region Trader Tasks View

        [TestMethod]
        public void UpdateExchangeInfoTask_Test()
        {
            Log.Debug("Start Test: UpdateExchangeInfoTask_Test");
            // Setup
            MockTraderData.Trader.ApiController.Api.OnSendSleepMilliseconds = 1000;

            //Test
            Task<MockExchange> task = MockTraderData.Trader.Tasks.UpdateExchangeInfoAsync(NewApiRequestOptions());

            Thread.Sleep(10);
            Assert.IsTrue(TestClientOutputWindow.OutputWindow.Count == 1);
            Assert.IsTrue(TestClientOutputWindow.OutputWindow[0].Status == OutputItemStatusEnum.InProgress);

            //Thread.Sleep(2000);
            task.Wait();

            Assert.IsTrue(TestClientOutputWindow.OutputWindow[0].Status == OutputItemStatusEnum.Success);
        }

        [TestMethod]
        public void UpdateExchangeInfoTask_MockTraderView_Test() //*/
        {
            // Setup

            MockTraderData.Trader.ApiController.Api.ApiHttpResponse.HttpResponse =
                new HttpResponseMessage(System.Net.HttpStatusCode.OK);

            MockTraderData.Trader.ApiController.Api.ApiHttpResponse.HttpResponse.Headers
                .Add(ResponseHeaderWeightKeyNameTest, ["20"]);

            MockTraderData.Trader.ApiController.Api.OnSendSleepMilliseconds = 1000;

            //Test
            Task<MockExchange> task = MockTraderData.Trader.Tasks.UpdateExchangeInfoAsync(NewApiRequestOptions());

            task.Wait();

            Assert.IsTrue(TestClientOutputWindow.OutputWindow[0].Status == OutputItemStatusEnum.Success);
            Assert.IsTrue(TestClientApiThrottleWindow.ApiThrottleWindow.Throttle.CurrentWeightUsed == 20);
        }

        //*/ Market Tasks tests todo here



        #endregion
    }
}