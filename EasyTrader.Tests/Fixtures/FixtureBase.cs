using EasyTrader.Core.Common;
using EasyTrader.Core.Connectors;
using EasyTrader.Core.Connectors.Http;
using EasyTrader.Core.Configuration;
using EasyTrader.Core.Models.Binance;
using EasyTrader.Core.Processes;
using Newtonsoft.Json.Linq;
using EasyTrader.Core.PersistenceServices.IO;
using EasyTrader.Core;
using EasyTrader.Core.Models;
using EasyTrader.Core.Views.OutputView.OutputWindowView;
using EasyTrader.Tests.Models.Views;
using EasyTrader.Tests.Models;
using EasyTrader.Tests.UnitTests;

namespace EasyTrader.Tests.Fixtures
{
    public class FixtureBase
    {
        public static string TestDataDirectoryPath = @"..\..\..\FixtureData";
        public static string AppConfigFileName = "FixtureAppConfig.json";

        protected Configurations Configurations { get; private set; }

        protected ExchangeConfiguration ExchangeConfiguration { get; private set; }
        protected ApiController<RestApi> ApiController { get; private set; }

        protected TraderProcess<
            BinanceExchange,
            BinanceApi_ExchangeInfo,
            BinanceMarket,
            JArray,
            BinanceCandle,
            RestApi>
            Process
        { get; set; }

        public FixtureBase(string exchangeName = "Binance")
        {
            TraderGlobals.AppConfigFileName = AppConfigFileName;

            Configurations = TraderGlobals.ReadConfigurations(TestDataDirectoryPath);

            if (Configurations is null) { throw CommonException.ConfigurationNull(); }

            ExchangeConfiguration = new Portfolio(Configurations.ExchangeConfigurations).GetExchangeConfiguration(exchangeName);

            Throttle throttle = new Throttle(ExchangeConfiguration.ApiConfiguration);

            ApiController = new ApiController<RestApi>(throttle);

            var persistenceService = new JsonDataFilesPersistenceService(TestDataDirectoryPath);

            Process = new TraderProcess<
                BinanceExchange,
                BinanceApi_ExchangeInfo,
                BinanceMarket,
                JArray,
                BinanceCandle,
                RestApi>(
                persistenceService,
                exchangeConfiguration: ExchangeConfiguration,
                apiController: ApiController,
                UnitTestBase.MockClientOutput);

            Log.Clear();
        }
    }
}