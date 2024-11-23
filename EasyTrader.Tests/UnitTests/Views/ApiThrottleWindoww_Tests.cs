using EasyTrader.Core.Common;
using EasyTrader.Core.Views.OutputView.ApiThrottleWindowView;
using EasyTrader.Core.Views.OutputView.OutputWindowView;
using EasyTrader.Tests.Models.Views;

namespace EasyTrader.Tests.UnitTests.Views
{
    [TestClass()]
    public class ApiThrottleWindoww_Tests
    {
        public static string TestsOutputDirectoryPath = @"..\..\..\TestsOutput";

        [TestMethod]
        public void Test_ApiThrottleWindow()
        {
            // Setup
            var clientOutput = new MockClientApiThrottleWindowControl();

            // Test
            var apiThrottleWindow = new ApiThrottleWindow(clientOutput);

            Assert.IsTrue(apiThrottleWindow.ClientApiThrottleWindow == clientOutput);
        }

        [TestMethod]
        public void Test_WriteApiThrottleWindow()
        {
            // Setup
            var clientOutput = new MockClientApiThrottleWindowControl();
            var apiThrottleWindow = new ApiThrottleWindow(clientOutput);
            var throttle = new Throttle();

            // Test
            clientOutput.ApiThrottleWindow.WriteApiThrottleWindow(throttle);

            Assert.IsTrue(clientOutput.IsTestResult);
        }

       
    }
}

