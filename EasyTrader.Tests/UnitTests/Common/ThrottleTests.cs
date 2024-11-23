using EasyTrader.Core.Common;
using EasyTrader.Core.Configuration;

namespace EasyTrader.Tests.UnitTests.Common
{
    [TestClass()]
    public class ThrottleTests
    {
        [TestMethod]
        public void Throttle_Test()
        {
            // Test success validation:
            var throttle = new Throttle(UnitTestBase.NewtApiConfiguration(1, 1, 1));

            Assert.IsTrue(throttle.IsReleased == true);
            Assert.IsTrue(throttle.LastCall <= DateTime.Now);

            // Test success validation all null:
            var throttleNullValue = new Throttle();

            Assert.IsTrue(throttleNullValue.IsReleased == true);
            Assert.IsTrue(throttleNullValue.LastCall <= DateTime.Now);

            // Test fail validation:
            try { Throttle throttleError = new Throttle(UnitTestBase.NewtApiConfiguration(0, 1, 1)); }
            catch (CommonException ex) { Assert.IsTrue(ex.Message == "Throttle configuration error 'CallsPerSecondPolicy: 0'."); }

            try { Throttle throttleError = new Throttle(UnitTestBase.NewtApiConfiguration(1, 0, 1)); }
            catch (CommonException ex) { Assert.IsTrue(ex.Message == "Throttle configuration error 'WeightCooldownSecondsPolicy: 0'."); }

            try { Throttle throttleError = new Throttle(UnitTestBase.NewtApiConfiguration(1, 1, 0)); }
            catch (CommonException ex) { Assert.IsTrue(ex.Message == "Throttle configuration error 'MaximumTotalWeight: 0'."); }
        }

        [TestMethod]
        public void CallEveryMilliseconds_Test()
        {
            // Setup
            var throttle = new Throttle(UnitTestBase.NewtApiConfiguration(1000));
            // Test
            Assert.IsTrue(throttle.CallEveryMilliseconds == 1);
        }

        [TestMethod]
        public void Start_ThrottleCallsPerSecond_Test()
        {
            // Setup
            var _callsPerSecondPolicy = 2;// every 500 milliseconds
            var expectedSleep = 1000 / _callsPerSecondPolicy;
            var sleepLastCall = 100;

            var throttle = new Throttle(
                UnitTestBase.NewtApiConfiguration(callsPerSecondPolicy: _callsPerSecondPolicy));

            Thread.Sleep(sleepLastCall); // sleep just a little before start to have a gap from LastCall 

            var lastCall = throttle.LastCall;

            Assert.IsTrue(throttle.IsReleased == true);
            Assert.IsTrue(throttle.LastCall <= DateTime.Now);

            // Test
            var task = Task.Run(() => throttle.Start());
            Thread.Sleep(10);

            Assert.IsTrue(throttle.IsReleased == false);
            Assert.IsTrue(throttle.LastCall == lastCall);

            Thread.Sleep(expectedSleep);
            Thread.Sleep(10);

            Assert.IsTrue(throttle.IsReleased == true);
            Assert.IsTrue(throttle.LastCall > lastCall && throttle.LastCall <= DateTime.Now);
        }

        [TestMethod]
        public void Start_ThrottleCallsPerSecond_Release_Test()
        {
            // Setup
            var _callsPerSecondPolicy = 2; // every 500 milliseconds
            var expectedSleep = 1000 / _callsPerSecondPolicy;

            var throttle = new Throttle(
                UnitTestBase.NewtApiConfiguration(callsPerSecondPolicy: _callsPerSecondPolicy));

            var lastCall = throttle.LastCall;

            Assert.IsTrue(throttle.IsReleased == true);
            Assert.IsTrue(throttle.LastCall <= DateTime.Now);

            // Test
            var task = Task.Run(() => throttle.Start());

            Thread.Sleep(100);

            Assert.IsTrue(throttle.IsReleased == false);
            Assert.IsTrue(throttle.LastCall == lastCall);

            throttle.Release();

            Assert.IsTrue(throttle.IsReleased == true);
            Assert.IsTrue(throttle.LastCall > lastCall && throttle.LastCall <= DateTime.Now);

            Assert.IsTrue(lastCall.AddMilliseconds(expectedSleep) > throttle.LastCall);
        }

        [TestMethod]
        public void Start_ThrottleWeight_Test()
        {
            // Setup
            var throttle = new Throttle(
                UnitTestBase.NewtApiConfiguration(
                    weightCooldownSecondsPolicy: 2,
                    maximumTotalWeight: 200));

            Assert.IsTrue(throttle.IsReleased == true);
            Assert.IsTrue(throttle.CurrentWeightUsed == 0);

            // Test 1: Current = 0; expected = 100; maximum = 200 => no sleep
            var task1 = Task.Run(() => throttle.Start());
            Thread.Sleep(10);

            Assert.IsTrue(throttle.IsReleased == true);
            Assert.IsTrue(throttle.CurrentWeightUsed == 0);

            // Test 2: Current = 100; expected = 100; maximum = 200 => no sleep
            throttle.Release();
            throttle.AddWeight(100);
            var task2 = Task.Run(() => throttle.Start());
            Thread.Sleep(10);

            Assert.IsTrue(throttle.IsReleased == true);
            Assert.IsTrue(throttle.CurrentWeightUsed == 100);

            // Test 3: Current = 101; expected = 100; maximum = 200 => sleep(WeightCooldownSecondsPolicy)
            throttle.Release();
            throttle.AddWeight(1);
            var task3 = Task.Run(() => throttle.Start());
            Thread.Sleep(10);

            Assert.IsTrue(throttle.IsReleased == false);
            Assert.IsTrue(throttle.CurrentWeightUsed == 101);

            Thread.Sleep((int)(throttle.WeightCooldownSecondsPolicy * 1000) + 10);

            Assert.IsTrue(throttle.IsReleased == true);
            Assert.IsTrue(throttle.CurrentWeightUsed == 0);
        }

        [TestMethod]
        public void Start_ThrottleWeight_Release_Test()
        {
            // Setup
            var throttle = new Throttle(
                UnitTestBase.NewtApiConfiguration(
                    weightCooldownSecondsPolicy: 2,
                    maximumTotalWeight: 200));

            Assert.IsTrue(throttle.IsReleased == true);
            Assert.IsTrue(throttle.CurrentWeightUsed == 0);

            throttle.AddWeight(150);
            var task3 = Task.Run(() => throttle.Start());
            Thread.Sleep(100);

            Assert.IsTrue(throttle.IsReleased == false);
            Assert.IsTrue(throttle.CurrentWeightUsed == 150);

            throttle.Release();

            Assert.IsTrue(throttle.IsReleased == true);
            Assert.IsTrue(throttle.CurrentWeightUsed == 150);
        }

        private string GetValidationMessage(string field)
        {
            var e = CommonException.ThrottleConfigurationError(field);
            return e.Message;
        }


    }
}


