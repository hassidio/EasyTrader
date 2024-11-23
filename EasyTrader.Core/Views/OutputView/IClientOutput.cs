using EasyTrader.Core.Views.OutputView.ApiThrottleWindowView;
using EasyTrader.Core.Views.OutputView.OutputWindowView;

namespace EasyTrader.Core.Views.OutputView
{
    public interface IClientOutput //*/ dont need this
    {
        IClientOutputWindow ClientOutputWindow { get; set; }

        IClientApiThrottleWindow ClientApiThrottleWindow { get; set; }
    }
}
