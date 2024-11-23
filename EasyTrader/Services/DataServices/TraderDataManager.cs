using EasyTrader.Core;
using EasyTrader.Core.Connectors.Http;
using EasyTrader.Core.DataServices;
using EasyTrader.Core.Models.Binance;
using EasyTrader.Core.Views.OutputView;
using EasyTrader.Core.Views.OutputView.ApiThrottleWindowView;
using EasyTrader.Core.Views.OutputView.OutputWindowView;
using EasyTrader.Tests.Models;
using EasyTrader.Tests.Models.MockClientApi;
using EasyTrader.UserControls.ApiThrottle;
using Newtonsoft.Json.Linq;

namespace EasyTrader.Services.DataServices
{

    public class TraderDataManager
    {
        public TraderDataManager(List<string> exchangeNames, IClientOutputWindow outputWindows)
        {
            ClientOutputDictionary = new Dictionary<string, IClientOutput>();

            foreach (var exchangeName in TraderGlobals.Portfolio.ExchangeNames)
            {
                ClientOutputDictionary.Add(exchangeName, new ClientOutput(outputWindows, GetClientApiThrottleWindow(exchangeName)));
            }

            TraderDataServiceDictionary = new Dictionary<string, ITraderDataService>();

            foreach (var exchangeName in TraderGlobals.Portfolio.ExchangeNames)
            {
                ITraderDataService traderDataService = CreateTraderDataService(exchangeName, ClientOutputDictionary[exchangeName]);

                TraderDataServiceDictionary.Add(exchangeName, traderDataService);
            }
        }

        public Dictionary<string, IClientOutput> ClientOutputDictionary { get; set; }
        public Dictionary<string, ITraderDataService> TraderDataServiceDictionary { get; set; }


        private IClientApiThrottleWindow? GetClientApiThrottleWindow(string exchangeName)
        {
            var control = new ApiThrottleUserControl();
            control.Title = exchangeName;

            return control;
        }

        private ITraderDataService CreateTraderDataService(string exchangeName, IClientOutput clientOutput)
        {
            var exchangeConfiguration =
                    TraderGlobals.Portfolio.GetExchangeConfiguration(exchangeName);

            if (exchangeName == "Binance")
            {
                return
                    new TraderDataService(
                        new TraderData<BinanceExchange, BinanceApi_ExchangeInfo, BinanceMarket, JArray, BinanceCandle, RestApi>(
                            exchangeConfiguration: exchangeConfiguration,
                            clientOutput: clientOutput));
            }

            if (exchangeName == "Mock Exchange Name")
            {
                return
                    new TraderDataService(
                        new TraderData<MockExchange, MockExchangeClientInfo, MockMarket, IList<MockCandle>, MockCandle, MockApi>(
                            exchangeConfiguration: exchangeConfiguration,
                            clientOutput: clientOutput));
            }
            return null;
        }

        public ITraderDataService GetTraderDataService(string exchangeName)
        {
            return TraderDataServiceDictionary[exchangeName];
        }

        public dynamic GetTraderData(string exchangeName)
        {
            var traderDataService = TraderDataServiceDictionary[exchangeName];

            return traderDataService.TraderData;
        }
    }
}