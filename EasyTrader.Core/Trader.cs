using EasyTrader.Core.Common;
using EasyTrader.Core.Connectors;
using EasyTrader.Core.Configuration;
using EasyTrader.Core.Processes;
using EasyTrader.Core.Models.Entities;
using EasyTrader.Core.Views.OutputView;
using EasyTrader.Core.Views.OutputView.OutputWindowView;
using EasyTrader.Core.PersistenceServices;

namespace EasyTrader.Core
{
    public class Trader<TExchange, TClientExchange, TMarket, TClientMarket, TCandle, TApi>
        where TExchange : IExchange, new()
        where TMarket : IMarket, new()
        where TCandle : ICandle, new()
        where TApi : IApi, new()
    {
        public Trader(
            IPersistenceService persistenceService,
            ExchangeConfiguration exchangeConfiguration,
            IClientOutput clientOutput)
        {

            Throttle throttle = new Throttle(exchangeConfiguration.ApiConfiguration);

            ApiController = new ApiController<TApi>(throttle);

            ApiController.Error += ApiController_Error;
            
            ClientOutput = clientOutput;

            Tasks = new TraderTasks<TExchange, TClientExchange, TMarket, TClientMarket, TCandle, TApi>(
                        persistenceService: persistenceService,
                        exchangeConfigurations: exchangeConfiguration,
                        apiController: ApiController,
                        clientOutput: ClientOutput);

        }

        public IClientOutput ClientOutput { get; private set; }


        public ApiController<TApi> ApiController { get; private set; }

        public TraderTasks<TExchange, TClientExchange, TMarket, TClientMarket, TCandle, TApi> Tasks { get; private set; }

        private void ApiController_Error(object sender, ErrorEventArgs e)
        {
            AggregateException aggregateException = (AggregateException)e.GetException();

            ClientOutput.ClientOutputWindow.OutputWindow.WriteOutputWindowItem(
                    text: $"{CommonException.GetFullExceptionDescription(aggregateException)}",
                    dateTime: DateTime.Now,
                    status: OutputItemStatusEnum.Fail);
        }
    }
}
