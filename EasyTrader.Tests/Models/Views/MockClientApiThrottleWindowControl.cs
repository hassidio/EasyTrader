using EasyTrader.Core.Common;
using EasyTrader.Core.Views.OutputView.ApiThrottleWindowView;
using System.Diagnostics;

namespace EasyTrader.Tests.Models.Views
{
    public class MockClientApiThrottleWindowControl : IClientApiThrottleWindow
    {
        public string Title { get; set; }


        public MockClientApiThrottleWindowControl() 
        {
            ApiThrottleWindow = new ApiThrottleWindow(this);
            IsTestResult = false;
        }

        public ApiThrottleWindow ApiThrottleWindow { get; set; }

        public bool IsTestResult { get; set; }
        public void UpdateApiThrottleWindow(Throttle throttle)
        {
            IsTestResult = true;

            Debug.WriteLine($"");
            Debug.WriteLine($"Api Throttle Window:");
            Debug.WriteLine($"LastCall: {throttle.LastCall.ToString("MM/dd/yyyy hh:mm:ss.fff tt")}");
            Debug.WriteLine($"CallsPerSecondPolicy: {throttle.CallsPerSecondPolicy}");
            Debug.WriteLine($"WeightCooldownSecondsPolicy: {throttle.WeightCooldownSecondsPolicy}");
            Debug.WriteLine($"MaximumTotalWeight: {throttle.MaximumTotalWeight}");
            Debug.WriteLine($"ResponseCurrentWeightUsed: {throttle.CurrentWeightUsed}");
            Debug.WriteLine($"MaximumMessageWeightSoFar: {throttle.MaximumMessageWeightSoFar}");
            Debug.WriteLine($"CallEveryMilliseconds: {throttle.CallEveryMilliseconds}");
            Debug.WriteLine($"IsReleased: {throttle.IsReleased}");

        }
    }
}

