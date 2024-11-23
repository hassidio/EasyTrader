using EasyTrader.Core;
using EasyTrader.Core.Common;
using EasyTrader.Core.Connectors;
using EasyTrader.Core.Connectors.Http;
using EasyTrader.Core.Configuration;
using EasyTrader.Tests.Models;
using EasyTrader.Tests.Models.MockClientApi;
using EasyTrader.Tests.Models.Views;
using EasyTrader.Core.PersistenceServices.IO;
using Newtonsoft.Json.Linq;
using EasyTrader.Core.Models;
using EasyTrader.Core.PersistenceServices;
using EasyTrader.Core.Views.OutputView;
using Newtonsoft.Json;

namespace EasyTrader.Tests.UnitTests
{
    public class UnitTestBase
    {
        public UnitTestBase()
        {
            TraderGlobals.AppConfigFileName = AppConfigFileName;

            MockConfigurations = TraderGlobals.ReadConfigurations(DataDirectoryPathTest);

            if (MockConfigurations is null)
            { throw CommonException.ConfigurationNull(); }

            Portfolio = new Portfolio(MockConfigurations.ExchangeConfigurations);

            MockExchangeConfiguration = Portfolio.GetExchangeConfiguration(ExchangeNameTest);

            MockPersistenceService = new JsonDataFilesPersistenceService(DataDirectoryPathTest);

            ExchangeTest = NewExchange();

            Log.Clear();
        }


        protected Configurations MockConfigurations { get; private set; }
        public Portfolio Portfolio { get; private set; }
        protected IPersistenceService MockPersistenceService { get; private set; }
        public ExchangeConfiguration MockExchangeConfiguration { get; private set; }

        public static ClientOutput MockClientOutput
        {
            get
            {
                return new ClientOutput
                {
                    ClientOutputWindow = new MockClientOutputWindowControl(),
                    ClientApiThrottleWindow = new MockClientApiThrottleWindowControl() ,
                };
            }
        }
        
        protected static MockExchange ExchangeTest;


        // Test Parameters:

        //MockConfigurations
        public static string DataDirectoryPathTest = @"..\..\..\TestData";
        public static string AppConfigFileName = "TestAppConfig.json";
        public static string ExchangeNameTest = "Mock Exchange Name";
        public static string EntityStampTest = "StampTest";


        // Globals
        public static string MarketIdTest = "UnitTestBase Market1";
        public static string CandleIdTest = "UnitTestBase Candle1";

        public static long IntervalNum_10m = 10;
        public static string Interval_mString = IntervalNum_10m.ToString() + "m";

        public static string BaseUrlTest = "http://TestBaseUrl/";
        public static string PathUriTest = "/TestPathUri/";


        public static Dictionary<string, string> ApiRequestQueryTest
            = new Dictionary<string, string>
            {
                { "symbol", MarketIdTest  },
                { "interval", Interval_mString }
            };

        public static Dictionary<string, string> ApiRequestHeadersTest
            = new Dictionary<string, string>()
        {
            { "Header1", "Header Test"  },
            { "Header2", "apiKey" }
        };

        public static object? ApiRequestContentTest = "the requestor (client) api request content";

        public static string ResponseHeaderWeightKeyNameTest = "weight-Key-xxx";
        public static int ResponseHeaderWeightValueTest = 20;

        public static ApiConfiguration NewtApiConfiguration(
            int? callsPerSecondPolicy = null,
            int? weightCooldownSecondsPolicy = null,
            int? maximumTotalWeight = null)
        {
            return new ApiConfiguration
            {
                CallsPerSecondPolicy = callsPerSecondPolicy,
                WeightCooldownSecondsPolicy = weightCooldownSecondsPolicy,
                MaximumTotalWeight = maximumTotalWeight,
            };
        }

        public static ApiRequestOptions NewApiRequestOptions()
        {
            var requestOptions = new ApiRequestOptions
            {
                ApiRequestQueryOptions = ApiRequestQueryTest,
                ApiRequestHeadersOptions = ApiRequestHeadersTest,
                ApiRequestContentOptions = ApiRequestContentTest,
                ResponseHeaderWeightKeyName = ResponseHeaderWeightKeyNameTest,
            };
            return requestOptions;
        }

        public static MockExchange NewExchange()
        {
            var exchange = new MockExchange();

            exchange.SetEntity(
                id: ExchangeNameTest,
                rawClientData:
                    new MockExchangeClientInfo(NewMockApiHttpRequest(exchange)));
            return exchange;
        }


        public static MockMarket NewMarket(string marketId, dynamic clientCandlesDataArray)
        {
            var market = new MockMarket();
            market.SerializedEntityParent = JsonConvert.SerializeObject(ExchangeTest);
            market.SetEntity(id: marketId, parentId: ExchangeNameTest, rawClientData: clientCandlesDataArray);
            return market;
        }

        public static MockMarket NewMarket()
        {
            return NewMarket(MarketIdTest, NewCandlesDefaultList(CandleIdTest));
        }

        /// <summary>
        /// Returns a List of MockCandle with 3 items. 
        /// </summary>
        /// <param name="clientId">Optional</param>
        /// <returns></returns>
        public static IList<MockCandle> NewCandlesDefaultList(string clientId = "")
        {
            var now = DateTime.Now;
            object[] prop = { 2000, "prop 2 value" };

            var clientDataList = new List<MockCandle>();
            clientDataList.Add(NewDefaultCandle(clientId, now));
            clientDataList.Add(MockCandle.NewMockCandle((clientId != string.Empty ? clientId : "2"), 5, 4, 8, 3, now.AddMinutes(10), 20, 500, prop));
            clientDataList.Add(MockCandle.NewMockCandle((clientId != string.Empty ? clientId : "3"), 2, 7, 8, 1, now.AddMinutes(20), 30, 100, prop));

            return clientDataList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cliendCandleId"></param>
        /// <param name="lastCandle"></param>
        /// <returns></returns>
        public static IList<MockCandle> GetCandlesIncreasedList(MockCandle lastCandle, string cliendCandleId = "")
        {
            var cliendDataList = NewCandlesDefaultList(cliendCandleId);

            cliendDataList[0].IncreaseCandleTime(lastCandle, IntervalNum_10m);
            cliendDataList[1].IncreaseCandleTime(lastCandle, IntervalNum_10m * 2);
            cliendDataList[2].IncreaseCandleTime(lastCandle, IntervalNum_10m * 3);

            cliendDataList.Add(NewDefaultCandle(CandleIdTest, lastCandle.OpenDateTime)); // Candle Already exists. Adding to test.
            cliendDataList.Add(NewDefaultCandle(CandleIdTest, lastCandle.OpenDateTime));
            cliendDataList[4].IncreaseCandleTime(lastCandle, (-1) * IntervalNum_10m);

            return cliendDataList;
        }

        public static MockCandle NewDefaultCandle(string clientId, DateTime time)
        {
            return MockCandle.NewMockCandle((clientId != string.Empty ? clientId : "1"), 1, 2, 3, 0.5, time, 10, 1000);
        }

        public static MockCandle NewDefaultCandle()
        {
            return NewDefaultCandle(CandleIdTest, DateTime.Now);
        }

        public static ApiHttpRequest NewMockApiHttpRequest(IApiRequestor requestor)
        {
            return new ApiHttpRequest(requestor, BaseUrlTest, PathUriTest);
        }

        public static IList<MockCandle> NewCandles(int count, string? clientId = "")
        {
            var now = DateTime.Now;
            object[] prop = { 2000, "prop 2 value" };

            var candleList = new List<MockCandle>();
            for (int i = 0; i < count; i++)
            {
                MockCandle candle = MockCandle.NewMockCandle(
                    (string.IsNullOrEmpty(clientId) ? (i + 1).ToString() : clientId),
                    5, 4, 8, 3,
                    now.AddMinutes(i * 10), i * 10, i * 100);

                candle.MarketId = MarketIdTest;
                candle.SetCandle(marketId: MarketIdTest, clientCandleData: (JToken)candle);

                candleList.Add(candle);
            }

            return candleList;
        }

    }
}