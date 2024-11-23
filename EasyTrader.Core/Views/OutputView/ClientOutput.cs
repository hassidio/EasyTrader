using EasyTrader.Core.Views.OutputView.ApiThrottleWindowView;
using EasyTrader.Core.Views.OutputView.OutputWindowView;

namespace EasyTrader.Core.Views.OutputView
{
    public class ClientOutput : IClientOutput
    {
        public ClientOutput(
            IClientOutputWindow? clientOutputWindow = null,
            IClientApiThrottleWindow? clientApiThrottleWindow = null) 
        {
            ClientOutputWindow = clientOutputWindow;
            ClientApiThrottleWindow = clientApiThrottleWindow;
        }

        public IClientOutputWindow ClientOutputWindow { get; set; }
        public IClientApiThrottleWindow ClientApiThrottleWindow { get; set; }

    }
}
