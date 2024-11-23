using EasyTrader.Core.Connectors;
using EasyTrader.Core.Configuration;
using EasyTrader.Core.Views.OutputView.OutputWindowView;
using EasyTrader.Core.Common;
using EasyTrader.Core.PersistenceServices;
using EasyTrader.Core.Views.OutputView;

namespace EasyTrader.Core.Processes
{
    public class ProcessBase<TApi>
        where TApi : IApi, new()
    {
        public ProcessBase(
            IPersistenceService  persistenceService,
            ExchangeConfiguration exchangeConfigurations,
            ApiController<TApi> apiController,
            IClientOutput clientOutput)
        {
            PersistenceService = persistenceService;
            ExchangeConfiguration = exchangeConfigurations;
            ApiController = apiController;
            ClientOutput = clientOutput;
        }

        public IPersistenceService PersistenceService { get; private set; }
        public ExchangeConfiguration ExchangeConfiguration { get; set; }
        public ApiController<TApi> ApiController { get; set; }
        public IClientOutput ClientOutput { get; private set; }

        protected OutputWindowItem? WriteOutputWindow (
            OutputWindowItem? outputItem = null,
            string? text = null,
            DateTime? dateTime = null,
            OutputItemStatusEnum? status = null)
        {
            if (ClientOutput.ClientOutputWindow is null) { return null; }

            return ClientOutput.ClientOutputWindow.OutputWindow.WriteOutputWindowItem(
                outputItem: outputItem, 
                text: text, 
                dateTime: dateTime, 
                status: status);
        }

        protected void WriteApiThrottleWindow(Throttle? throttle = null)
        {
            if (ClientOutput.ClientApiThrottleWindow is null) { return; }

            ClientOutput.ClientApiThrottleWindow.ApiThrottleWindow.WriteApiThrottleWindow(throttle);
        }
    }
}
