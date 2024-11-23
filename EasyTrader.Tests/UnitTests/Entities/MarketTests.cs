using EasyTrader.Core.Common;
using EasyTrader.Tests.Models;
using EasyTrader.Tests.Models.MockClientApi;
using Newtonsoft.Json;

namespace EasyTrader.Tests.UnitTests.Entities
{
    [TestClass]
    public class MarketTests : UnitTestBase
    {
        public MarketTests() : base() { }


        [TestMethod()]
        public void SetMarket_Test()
        {
            Log.Debug("Start Test: SetMarket_Test");

            // Setup
            var market = new MockMarket();

            var clientCandlesDataArray = NewCandlesDefaultList("clientCandles SetMarketTest");

            Assert.IsTrue(market.MarketInfo is null);
            Assert.IsTrue(market.Exchange is null);
            Assert.IsTrue(market.RawClientData is null);


            // Test
            market.SerializedEntityParent = JsonConvert.SerializeObject(ExchangeTest);
            market.SetEntity(id: MarketIdTest, parentId: ExchangeTest.EntityId, rawClientData: clientCandlesDataArray);

            Assert.IsTrue(market.MarketInfo is not null);
            Assert.IsTrue(market.MarketInfo.Id == MarketIdTest);
        }

        [TestMethod()]
        public void GetClientData_Test()
        {
            Log.Debug("Start Test: GetClientData_Test");

            var clientCandlesDataArray = NewCandlesDefaultList(CandleIdTest);

            var market = NewMarket();
            IList<dynamic> clientCandles = market.ClientDataList;

            Assert.IsTrue(clientCandles is not null);
            Assert.IsTrue(clientCandles.Count > 0);

            var c = clientCandles.FirstOrDefault();
            Assert.IsTrue(c.Id == CandleIdTest);
        }

        [TestMethod()]
        public void GetCandles_Test()
        {
            Log.Debug("Start Test: GetCandles_Test");

            var clientCandlesDataArray = NewCandlesDefaultList(CandleIdTest);

            var market = NewMarket();
            var candles = market.GetCandles<MockCandle>();

            Assert.IsTrue(candles is not null);
            Assert.IsTrue(candles.Count > 0);

            MockCandle c = candles.FirstOrDefault();
            Assert.IsTrue(c.Id == CandleIdTest);
            Assert.IsTrue(c.OpenDateTime < c.CloseDateTime);
        }

        [TestMethod()]
        public void AddCandle_Test()
        {
            Log.Debug("Start Test: AddCandle_Test");

            var clientCandlesDataArray = NewCandlesDefaultList(CandleIdTest);

            var market = NewMarket();

            var candlesCountBefore = market.GetCandles<MockCandle>().Count;

            var newMockCandle = NewDefaultCandle("DerivedObject Candle EntityNameId", DateTime.Now);
            newMockCandle.SetCandle(MarketIdTest, newMockCandle);
            market.AddCandle(newMockCandle);
            var newMockCandles = market.GetCandles<MockCandle>();
            var candlesCountAfter = newMockCandles.Count;

            var newCandle = newMockCandles.Where(c => c.Id == "DerivedObject Candle EntityNameId").First();

            Assert.IsTrue(candlesCountAfter - 1 == candlesCountBefore);
            Assert.IsTrue(newCandle is not null);
            Assert.IsTrue(newCandle.Id == "DerivedObject Candle EntityNameId");
        }

        [TestMethod()]
        public void RefreshCandles_Test()
        {
            Log.Debug("Start Test: RefreshCandles_Test");

            var market = NewMarket();
            var candleCountBefore = market.GetCandles<MockCandle>().Count;

            var clientDataList = new List<dynamic>(market.RawClientData);
            MockCandle currentLastCandle = (MockCandle)clientDataList.Last();

            var candle = NewDefaultCandle();
            candle.IncreaseCandleTime(currentLastCandle, IntervalNum_10m);

            market.RawClientData.Add(candle);

            Assert.IsTrue(market.GetCandles<MockCandle>().Count == candleCountBefore);

            market.RefreshCandles<MockCandle>();

            Assert.IsTrue(market.GetCandles<MockCandle>().Count == candleCountBefore + 1);

        }

        [TestMethod()]
        public void AppendClientCandlesData_Test()
        {
            Log.Debug("Start Test: AppendClientCandlesData_Test");
            var market = NewMarket();

            var clientDataList = new List<dynamic>(market.RawClientData);
            MockCandle currentLastCandle = (MockCandle)clientDataList.Last();
            var newClientCandlesDataArray = GetCandlesIncreasedList(currentLastCandle, "Append");

            var newMarket = NewMarket(MarketIdTest, newClientCandlesDataArray);

            market.AppendClientCandlesData<MockMarket, MockCandle>(newMarket);

            Assert.IsTrue(newMarket.GetCandles<MockCandle>().Count == market.GetCandles<MockCandle>().Count - 1);
        }

        [TestMethod()]
        public void GetCandles_OrderByOpenDateTime_Test()
        {
            Log.Debug("Start Test: GetCandles_OrderByOpenDateTime_Test");
            var market = NewMarket();

            var clientDataList = new List<dynamic>(market.RawClientData);
            MockCandle currentLastCandle = (MockCandle)clientDataList.Last();

            var newClientCandlesDataArray = NewCandlesDefaultList("Append");
            newClientCandlesDataArray[1].IncreaseCandleTime(currentLastCandle, IntervalNum_10m);
            newClientCandlesDataArray[2].IncreaseCandleTime(currentLastCandle, IntervalNum_10m * 2);
            newClientCandlesDataArray[0].IncreaseCandleTime(currentLastCandle, IntervalNum_10m * 3);

            var isInOrder = true;
            for (var i = 0; i < newClientCandlesDataArray.Count - 1; i++)
            {
                if (newClientCandlesDataArray[i].OpenDateTime > newClientCandlesDataArray[i + 1].OpenDateTime)
                { isInOrder = false; }
            }

            Assert.IsFalse(isInOrder);

            var newMarket = NewMarket(MarketIdTest, newClientCandlesDataArray);

            market.AppendClientCandlesData<MockMarket, MockCandle>(newMarket);

            // Test order
            for (var i = 0; i < market.GetCandles<MockCandle>().Count - 1; i++)
            {
                Assert.IsTrue(market.GetCandles<MockCandle>()[i].OpenDateTime < market.GetCandles<MockCandle>()[i + 1].OpenDateTime);
            }
        }

        [TestMethod()]
        public void GetApiHttpClientRequestQuery_Test()
        {
            Log.Debug("Start Test: GetApiHttpClientRequestQuery_Test");

            var market = NewMarket();

            var dic = market.ApiRequestClientQuery;

            Assert.IsTrue(dic is not null);
            Assert.IsTrue(dic["symbol"] == MarketIdTest);
            Assert.IsTrue(dic["interval"] == Interval_mString);
        }

        [TestMethod()]
        public void GetApiHttpClientRequestHeaders_Test()
        {
            Log.Debug("Start Test: GetApiHttpClientRequestHeaders_Test");
            var market = NewMarket();

            var dic = market.ApiRequestClientHeaders;

            Assert.IsTrue(dic is not null);
            Assert.IsTrue(dic["Header1"] == "Header Test");
        }

        [TestMethod()]
        public void GetApiHttpClientRequestContent_Test()
        {
            Log.Debug("Start Test: GetApiHttpClientRequestContent_Test");
            var market = NewMarket();

            var obj = market.GetApiRequestClientContent(/* todo with content*/);  //*/

            Assert.IsTrue(obj is not null);
            Assert.IsTrue(obj.GetType() == typeof(string));

            Assert.IsTrue(obj.ToString() == ApiRequestContentTest.ToString());
        }

    }
}
