using EasyTrader.Core.Common;
using EasyTrader.Tests.Models;
using EasyTrader.Core.Configuration;
using EasyTrader.Tests.Models.MockClientApi;
using EasyTrader.Core.Connectors.Http;
using EasyTrader.Tests.UnitTests.Sandbox;
using EasyTrader.Core;
using EasyTrader.Core.Connectors;
using System.Diagnostics;

namespace EasyTrader.Tests.UnitTests.Process
{
    [TestClass]
    public class ProcessTests : ProcessTestBase
    {
        public ProcessTests() 
        {
            MockApiController.Error += ApiController_Error;
        }


        [TestMethod]
        public void ProcessBase_Test()
        {
            Log.Debug("Start Test: ProcessBase_Test");

            Assert.IsTrue(MockConfigurations is not null);

            Assert.IsTrue(MockTraderProcess.ApiController.Api.GetType() == typeof(MockApi));
        }

        #region ExchangeProcesses

        [TestMethod]
        public void UpdateExchangeInfo_Test()
        {
            Log.Debug("Start Test: UpdateExchangeInfo_Test");
            // Setup
            var apiHttpRequest = NewMockApiHttpRequest(ExchangeTest);

            var mockClientInfo = new MockExchangeClientInfo(apiHttpRequest);

            ExchangeTest.SetEntity(rawClientData: mockClientInfo);

            //Test
            var exchangeBefore = MockTraderProcess.UpdateExchangeInfoProcess(NewApiRequestOptions());

            var exchangeAfter = MockTraderProcess.PersistenceService.Get<MockExchange>(ExchangeTest.EntityId);

            Assert.IsTrue(exchangeAfter.MarketNames.Count > 0);

            Assert.IsTrue(exchangeAfter.MarketNames.First() == MarketIdTest);

            Assert.IsTrue(exchangeAfter.Created.Ticks > 0);
        }

        [Ignore("Todo Archive")]
        [TestMethod]
        public void ArchiveExchange_Test()
        {
            //Log.Debug("Start Test: UpdateExchangeInfo_Test");

            //// Setup
            //var exchangeBefore = MockTraderProcess.UpdateExchangeInfoProcess(NewApiRequestOptions());

            //var servertime = 123;
            
            //// Test
            //MockTraderProcess.DefaultExchange.ServerTime = servertime;
            //MockTraderProcess.ArchiveExchange();

            //var exchangeAfter = 

            //Assert.IsTrue(exchangeAfter.ServerTime == servertime);

            //// Cleanup
            //FileManager.DeleteFile(Path.Combine(
            //    MockConfigurations.DefaultExchangeArchiveDirectoryPath, fileName)); //*/ Archive
        }

        #endregion

        #region MarketProcesses

        [TestMethod]
        public void UpdateMarketCandlesFromApi_UnitTest_Test()
        {
            Log.Debug("Start Test: UpdateMarketCandles_UnitTest_Test");

            // Setup
            var marketFile = RefreshDefaultDataFile();

            var marketBefore = MockTraderProcess.GetMarket(MarketIdTest);

            // Test
            var marketAfter =
                MockTraderProcess.UpdateMarketCandlesProcess(MarketIdTest, NewApiRequestOptions());

            // Print Debug
            var freshCandles = NewCandlesDefaultList("BaseObject Candle");
            var updateWithCandles = GetCandlesIncreasedList(freshCandles.Last(), "DerivedObject Candle");

            Print.DebugMarket<MockMarket, MockCandle>(marketBefore, "Market before update:", true);
            Print.DebugCandles(updateWithCandles, title: "Update with candles:");
            Print.DebugMarket<MockMarket, MockCandle>(marketAfter, "Market after update:", true);

            // Assert
            int candlesBeforeCount = marketBefore.GetCandles<MockCandle>().Count;
            int candlesAfterCount = marketAfter.GetCandles<MockCandle>().Count;

            Assert.IsTrue(candlesBeforeCount < candlesAfterCount);

            // Cleanup
            var cleanMarketFile = RefreshDefaultDataFile();
            Assert.IsTrue(
                cleanMarketFile.GetCandles<MockCandle>().Count
                == marketFile.GetCandles<MockCandle>().Count);

        }

        #endregion

        public ErrorEventArgs ErrorResultEventArgs { get; set; }
        private void ApiController_Error(object sender, ErrorEventArgs e)
        {
            Debug.WriteLine(e.GetException().Message);
            ErrorResultEventArgs = e;
        }


        [TestMethod]
        public void GetApiRequest_Client_Test()
        {
            Log.Debug("Start Test: GetApiRequest_Client_Test");

            // Setup
            var market = new MockMarket();
            market.SetEntity(
                id: MarketIdTest, 
                parentId: ExchangeTest.EntityId, 
                rawClientData: NewCandlesDefaultList());

            // Test
            ApiHttpRequest request = market.GetApiRequest(
                MockExchangeConfiguration.ApiConfiguration.ApiBaseUri,
                "PathUri");

            Assert.IsTrue(request.Query is not null);
            Assert.IsTrue(request.Query["symbol"] == MarketIdTest);
            Assert.IsTrue(request.Query["interval"] == Interval_mString);

            Assert.IsTrue(request.Headers is not null);
            Assert.IsTrue(request.Headers["Header1"] == "Header Test");

            Assert.IsTrue(request.Content is not null);

            Assert.IsTrue(request.Content.GetType() == typeof(string));

            Assert.IsTrue(request.Content.ToString() == ApiRequestContentTest.ToString());
        }

        [TestMethod]
        public void GetApiRequest_RequestOptions_ResponseHeaderWeightKeyName_Success_Test()
        {
            // Setup
            var requestOptions = NewApiRequestOptions();
            int weight = 20;
            IEnumerable<string> weightIEnumerable = [weight.ToString()];
            MockTraderProcess.ApiController.Api.ApiHttpResponse.HttpResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            
            // Test ResponseHeaderWeightKeyName with value  (Success)
            MockTraderProcess.ApiController.Api.ApiHttpResponse.HttpResponse.Headers.Add(ResponseHeaderWeightKeyNameTest, weightIEnumerable);
            requestOptions.ResponseHeaderWeightKeyName = ResponseHeaderWeightKeyNameTest;
            MockExchange mockExchange = MockTraderProcess.UpdateExchangeInfoProcess(requestOptions);

            Assert.IsTrue(MockTraderProcess.ApiController.ApiHttpResponse.ResponseCurrentWeightUsed == weight);
            Assert.IsTrue(MockTraderProcess.ApiController.Api.ApiHttpResponse.ResponseCurrentWeightUsed == weight);
            Assert.IsTrue(MockTraderProcess.ApiController.Throttle.CurrentWeightUsed == weight);
            Assert.IsTrue(MockTraderProcess.ApiController.Throttle.MaximumMessageWeightSoFar == weight);

            // Test ResponseHeaderWeightKeyName with additional value (Success)
            int addWeight = 30;

            MockTraderProcess.ApiController.Api.ApiHttpResponse.HttpResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            MockTraderProcess.ApiController.Api.ApiHttpResponse.HttpResponse.Headers.Add(ResponseHeaderWeightKeyNameTest, [addWeight.ToString()]);

            mockExchange = MockTraderProcess.UpdateExchangeInfoProcess(requestOptions);

            Assert.IsTrue(MockTraderProcess.ApiController.ApiHttpResponse.ResponseCurrentWeightUsed == weight + addWeight);
            Assert.IsTrue(MockTraderProcess.ApiController.Api.ApiHttpResponse.ResponseCurrentWeightUsed == weight + addWeight);
            Assert.IsTrue(MockTraderProcess.ApiController.Throttle.CurrentWeightUsed == weight + addWeight);
            Assert.IsTrue(MockTraderProcess.ApiController.Throttle.MaximumMessageWeightSoFar == addWeight);

            // Test ResponseHeaderWeightKeyName null (null weight name - Success)
            MockTraderProcess.ApiController = new ApiController<MockApi>(new Throttle());
            MockTraderProcess.ApiController.Api.ApiHttpResponse.HttpResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            MockTraderProcess.ApiController.Api.ApiHttpResponse.HttpResponse.Headers.Add(ResponseHeaderWeightKeyNameTest, [weight.ToString()]);
            requestOptions.ResponseHeaderWeightKeyName = null;
            mockExchange = MockTraderProcess.UpdateExchangeInfoProcess(requestOptions);

            Assert.IsTrue(MockTraderProcess.ApiController.ApiHttpResponse.ResponseCurrentWeightUsed == 0);
            Assert.IsTrue(MockTraderProcess.ApiController.Api.ApiHttpResponse.ResponseCurrentWeightUsed == 0);
            Assert.IsTrue(MockTraderProcess.ApiController.Throttle.CurrentWeightUsed == 0);
            Assert.IsTrue(MockTraderProcess.ApiController.Throttle.MaximumMessageWeightSoFar == 0);

            // Test ResponseHeaderWeightKeyName string.Empty (empty weight name - Success)
            MockTraderProcess.ApiController.Api.ApiHttpResponse.HttpResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            MockTraderProcess.ApiController.Api.ApiHttpResponse.HttpResponse.Headers.Add(ResponseHeaderWeightKeyNameTest, [weight.ToString()]);
            requestOptions.ResponseHeaderWeightKeyName = string.Empty;
            mockExchange = MockTraderProcess.UpdateExchangeInfoProcess(requestOptions);

            Assert.IsTrue(MockTraderProcess.ApiController.ApiHttpResponse.ResponseCurrentWeightUsed == 0);
            Assert.IsTrue(MockTraderProcess.ApiController.Api.ApiHttpResponse.ResponseCurrentWeightUsed == 0);
            Assert.IsTrue(MockTraderProcess.ApiController.Throttle.CurrentWeightUsed == 0);
            Assert.IsTrue(MockTraderProcess.ApiController.Throttle.MaximumMessageWeightSoFar == 0);

        }

        [TestMethod]
        public void GetApiRequest_RequestOptions_ResponseHeaderWeightKeyName_Fail_Test()
        {
            // Setup
            var requestOptions = NewApiRequestOptions();
            int weight = 20;
            var commonException = CommonException.InvalidResponseHeaderWeight(ResponseHeaderWeightKeyNameTest, "", null);


            // Terst No weight header (fail)
            ErrorResultEventArgs = null;
            MockTraderProcess.ApiController.Api.ApiHttpResponse.HttpResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            requestOptions.ResponseHeaderWeightKeyName = ResponseHeaderWeightKeyNameTest;

            var mockExchange = MockTraderProcess.UpdateExchangeInfoProcess(requestOptions);
            Exception exception = ErrorResultEventArgs.GetException();

            Assert.IsTrue(mockExchange == null);
            Assert.IsTrue(exception.GetType() == typeof(System.AggregateException));
            Assert.IsTrue(exception.InnerException.GetType() == typeof(CommonException));
            Assert.IsTrue(exception.InnerException.Message == commonException.Message);


            // Test when ResponseHeaderWeightKeyNameTest header returns a value that is not an integer (fail)
            ErrorResultEventArgs = null;
            MockTraderProcess.ApiController.Api.ApiHttpResponse.HttpResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            MockTraderProcess.ApiController.Api.ApiHttpResponse.HttpResponse.Headers.Add(ResponseHeaderWeightKeyNameTest, ["abc"]);
            requestOptions.ResponseHeaderWeightKeyName = ResponseHeaderWeightKeyNameTest;
            commonException = CommonException.InvalidResponseHeaderWeight(ResponseHeaderWeightKeyNameTest, "abc", null);

            mockExchange = MockTraderProcess.UpdateExchangeInfoProcess(requestOptions); 
            exception = ErrorResultEventArgs.GetException();

            Assert.IsTrue(mockExchange == null);
            Assert.IsTrue(exception.GetType() == typeof(System.AggregateException));
            Assert.IsTrue(exception.InnerException.GetType() == typeof(CommonException));
            Assert.IsTrue(exception.InnerException.Message == commonException.Message);
        }


        [TestMethod]
        public void GetApiRequest_RequestOptions_Test()
        {
            Log.Debug("Start Test: GetApiRequest_RequestOptions_Test");

            // Setup
            var requestOptions = new ApiRequestOptions
            {
                ApiRequestQueryOptions = new Dictionary<string, string> { { "BaseQuery1", "Base Query 1" } },
                ApiRequestHeadersOptions = new Dictionary<string, string> { { "BaseHeader1", "Base Header 1" } },
                ApiRequestContentOptions = "Base Content"
            };

            var market = new MockMarket();
            market.SetEntity(
                id: MarketIdTest, 
                parentId: ExchangeTest.EntityId,
                rawClientData: NewCandlesDefaultList());

            // Test
            ApiHttpRequest request = market.GetApiRequest(
                MockExchangeConfiguration.ApiConfiguration.ApiBaseUri,
                "PathUri",
                requestOptions);

            Assert.IsTrue(request.Query is not null);
            Assert.IsTrue(request.Query["symbol"] == MarketIdTest);
            Assert.IsTrue(request.Query["interval"] == Interval_mString);
            Assert.IsTrue(request.Query["BaseQuery1"] == "Base Query 1");

            Assert.IsTrue(request.Headers is not null);
            Assert.IsTrue(request.Headers["Header1"] == "Header Test");
            Assert.IsTrue(request.Headers["BaseHeader1"] == "Base Header 1");

            Assert.IsTrue(request.Content is not null);

            Assert.IsTrue(request.Content.GetType() == typeof(string));

            Assert.IsTrue(request.Content.ToString() == "Base Content" + ApiRequestContentTest.ToString());
        }

    }
}