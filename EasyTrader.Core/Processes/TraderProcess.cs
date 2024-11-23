using EasyTrader.Core.Common;
using EasyTrader.Core.Connectors;
using EasyTrader.Core.Connectors.Http;
using EasyTrader.Core.Configuration;
using EasyTrader.Core.Models.Entities;
using EasyTrader.Core.Views.OutputView.OutputWindowView;
using EasyTrader.Core.PersistenceServices;
using EasyTrader.Core.Views.OutputView;

namespace EasyTrader.Core.Processes
{
    public class TraderProcess<TExchange, TClientExchange, TMarket, TClientMarket, TCandle, TApi>
        : ProcessBase<TApi>
        where TExchange : IExchange, new()
        where TMarket : IMarket, new()
        where TCandle : ICandle, new()
        where TApi : IApi, new()
    {
        public TraderProcess(
            IPersistenceService persistenceService,
            ExchangeConfiguration exchangeConfiguration,
            ApiController<TApi> apiController,
            IClientOutput clientOutput,
            ApiRequestOptions? exchangeRequestOptions = null)
            : base(persistenceService, exchangeConfiguration, apiController, clientOutput)
        {
            try
            {
                DefaultExchange =
                    PersistenceService.Get<TExchange>(exchangeConfiguration.ExchangeName);

                if (DefaultExchange is null) { throw new Exception(); }
            }
            catch (Exception)
            {
                throw CommonException.ExchangeFileNotFound(exchangeConfiguration.ExchangeName);
            }
        }

        public TExchange DefaultExchange { get; private set; }

        //*/ refactor Api related processes, eliminate async calls on ReadDefaultExchange

        #region Exchange Processes

        public TExchange UpdateExchangeInfoProcess(ApiRequestOptions requestOptions)
        {
            var outputItem = WriteOutputWindow(text: $"Updating _exchange information...");

            var exchange = new TExchange();

            var request = exchange.GetApiRequest(
                ExchangeConfiguration.ApiConfiguration.ApiBaseUri,
                ExchangeConfiguration.ApiConfiguration.ApiExchangeInfoUri,
                requestOptions);

            // Get _exchange info from ApiController call
            Log.Debug("Call ApiController for: " + typeof(TClientExchange).FullName);
            var apiExchangeData = ApiController.Send<TClientExchange>(request);             //<<<

            if (apiExchangeData is null)
            {
                WriteOutputWindow(
                    outputItem: outputItem,
                    text: $"Update exchange information failed {DefaultExchange.EntityId}",
                    dateTime: DateTime.Now,
                    status: OutputItemStatusEnum.Fail);

                return default;
            }

            WriteApiThrottleWindow(ApiController.Throttle);

            // create new _exchange object from api result
            Log.Debug("Set _exchange info from ApiController response: " + typeof(TExchange).FullName);

            exchange.SetEntity(id: DefaultExchange.EntityId, rawClientData: apiExchangeData);

            // Save new _exchange to current _exchange file
            Log.Debug("Save _exchange file: " + exchange.EntityId);

            PersistenceService.Save(exchange);

            DefaultExchange = exchange;

            WriteOutputWindow(
                outputItem: outputItem,
                text: "Update _exchange information completed successfully.",
                dateTime: DateTime.Now,
                status: OutputItemStatusEnum.Success);

            return exchange;
        }



        #endregion


        #region Market Processes

        /// <summary>
        /// Update From another file
        /// </summary>
        /// <typeparam name="TCandle"></typeparam>
        /// <param name="marketId">The market id to be updated</param>
        /// <param name="directoryPath">Source market file directory name</param>
        /// <param name="fileName">Source market file name</param>
        /// <returns></returns>
        public TMarket UpdateMarketCandlesProcess(TMarket marketNewData)
        {
            var market = PersistenceService.Get<TMarket>(
                marketNewData.MarketInfo.Id, marketNewData.EntityParentId);

            market.AppendClientCandlesData<TMarket, TCandle>(marketNewData);

            PersistenceService.Save(market);

            return market;
        }

        /// <summary>
        /// Update From api
        /// </summary>
        /// <typeparam name="TCandle"></typeparam>
        /// <param name="marketId"The market id to be updated></param>
        /// <param name="queryData"></param>
        /// <returns></returns>
        public TMarket UpdateMarketCandlesProcess(string marketId, ApiRequestOptions requestOptions)
        {
            var outputItem = WriteOutputWindow(text: $"Updating market candles information...");

            var market = new TMarket();

            var request = market.GetApiRequest(
                ExchangeConfiguration.ApiConfiguration.ApiBaseUri,
                ExchangeConfiguration.ApiConfiguration.ApiMarketCandlesUri,
                requestOptions);

            // Get market info from ApiController
            Log.Debug("Call ApiController for: " + typeof(TClientMarket).FullName);
            var apiMarketData = ApiController.Send<TClientMarket>(request);

            if (apiMarketData is not null)
            {
                market.SetEntity(marketId, DefaultExchange.EntityId, apiMarketData);

                // Load market file
                var marketDb = GetMarket(marketId);

                // Update new market info
                if (marketDb is not null)
                {
                    // Append new market info to market file
                    marketDb.AppendClientCandlesData<TMarket, TCandle>(market);
                    Log.Debug("Appended new market info to current market: "
                        + (marketDb.GetCandles<TCandle>().Count - market.GetCandles<TCandle>().Count)
                        + " records appended.");
                }
                else
                {
                    // not throwing exception e, for logging
                    var e = CommonException.MarketFileNotFound(marketId);
                    marketDb = market;
                }

                // Save
                Log.Debug("Save market db: " + marketId);

                PersistenceService.Save(marketDb);

                WriteOutputWindow(
                    outputItem: outputItem,
                    text: $"Update market candles information completed. - {marketId}",
                    dateTime: DateTime.Now,
                    status: OutputItemStatusEnum.Success);

                return marketDb;
            }

            throw CommonException.ApiCallFailed(marketId);
        }

        public TMarket GetMarket(string marketId)
        {
            TMarket market = PersistenceService.Get<TMarket>(marketId, DefaultExchange.EntityId);
            market.RefreshCandles<TCandle>();
            return market;
        }

        #endregion
    }


}
