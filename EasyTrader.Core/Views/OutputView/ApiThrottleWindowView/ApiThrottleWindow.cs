using EasyTrader.Core.Common;

namespace EasyTrader.Core.Views.OutputView.ApiThrottleWindowView
{
    public class ApiThrottleWindow
    {
        public ApiThrottleWindow(IClientApiThrottleWindow clientApiThrottleWindow)
        {
            ClientApiThrottleWindow = clientApiThrottleWindow;
        }
        public Throttle Throttle { get; set; }     

        public IClientApiThrottleWindow ClientApiThrottleWindow { get; }

        public void WriteApiThrottleWindow(Throttle throttle)
        {
            ClientApiThrottleWindow.UpdateApiThrottleWindow(throttle);
            Throttle = throttle;
        }
    }
}