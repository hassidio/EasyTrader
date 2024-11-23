using EasyTrader.Core.Common;
using EasyTrader.Core.Views.OutputView.OutputWindowView;

namespace EasyTrader.Core.Views.OutputView.ApiThrottleWindowView
{
    public interface IClientApiThrottleWindow
    {
        public string Title { get; set; }

        ApiThrottleWindow ApiThrottleWindow { get; set; }

        void UpdateApiThrottleWindow(Throttle throttle);
    }
}
