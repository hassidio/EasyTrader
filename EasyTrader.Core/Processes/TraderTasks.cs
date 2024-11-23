using EasyTrader.Core.Connectors;
using EasyTrader.Core.Connectors.Http;
using EasyTrader.Core.Configuration;
using EasyTrader.Core.Common.Tasks;
using EasyTrader.Core.Models.Entities;
using EasyTrader.Core.PersistenceServices;
using EasyTrader.Core.Views.OutputView;

namespace EasyTrader.Core.Processes
{
    public class TraderTasks<TExchange, TClientExchange, TMarket, TClientMarket, TCandle, TApi>
        : TraderProcess<TExchange, TClientExchange, TMarket, TClientMarket, TCandle, TApi>
        where TExchange : IExchange, new()
        where TMarket : IMarket, new()
        where TCandle : ICandle, new()
        where TApi : IApi, new()
    {
        public TraderTasks(
            IPersistenceService persistenceService,
            ExchangeConfiguration exchangeConfigurations,
            ApiController<TApi> apiController,
            IClientOutput clientOutput,
            ApiRequestOptions? requestOptions = null)
            : base(persistenceService, exchangeConfigurations, apiController, clientOutput, requestOptions) { }

        CommonTaskScheduler _scheduler = new CommonTaskScheduler();

        public async Task<TExchange> UpdateExchangeInfoAsync(ApiRequestOptions requestOptions)
        {
            var task = new Task<TExchange>(() => UpdateExchangeInfoProcess(requestOptions)); //*/1
            _scheduler.QueueTask(task);

            return await task;
        }

        public async Task<TMarket> UpdateMarketCandlesAsync(
            string marketId, ApiRequestOptions requestOptions)
        {
            var task = new Task<TMarket>(() => UpdateMarketCandlesProcess(marketId, requestOptions));
            _scheduler.QueueTask(task);

            return await task;
        }
    }
}


