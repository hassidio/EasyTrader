using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTrader.Tests.UnitTests.Sandbox
{
    [TestClass()]
    public class EventsTests
    {
        public static string TestsOutputDirectoryPath = @"..\..\..\TestsOutput";

        private static bool _thresholdReached = false;

        //[Ignore("Event Test")]
        [TestMethod()]
        public void Event1_Test()
        {
            _thresholdReached = false;

            Counter1 c1 = new Counter1(2);
            c1.ThresholdReached += c_ThresholdReached;

            c1.Add(1);
            Assert.IsTrue(!_thresholdReached);

            c1.Add(1);
            Assert.IsTrue(_thresholdReached);

        }

        //[Ignore("Event Test")]
        [TestMethod()]
        public void Event2_Test()
        {
            _thresholdReached = false;

            Counter2 c2 = new Counter2(2);
            c2.ThresholdReached += c_ThresholdReached;

            c2.Add(1);
            Assert.IsTrue(!_thresholdReached);

            c2.Add(1);
            Assert.IsTrue(_thresholdReached);

        }

        static void c_ThresholdReached(object sender, EventArgs e)
        {
            _thresholdReached = true;
            Debug.WriteLine("The threshold was reached.");
        }

        static void c_ThresholdReached(object sender, ThresholdReachedEventArgs e)
        {
            _thresholdReached = true;
            Debug.WriteLine($"The threshold of {e.Threshold} was reached at {e.TimeReached}.");
        }

    }

    class Counter1
    {
        private int threshold;
        private int total;

        public Counter1(int passedThreshold)
        {
            threshold = passedThreshold;
        }

        public void Add(int x)
        {
            total += x;
            if (total >= threshold)
            {
                ThresholdReached?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler ThresholdReached;
    }


    class Counter2
    {
        private int threshold;
        private int total;

        public Counter2(int passedThreshold)
        {
            threshold = passedThreshold;
        }

        public void Add(int x)
        {
            total += x;
            if (total >= threshold)
            {
                ThresholdReachedEventArgs args = new ThresholdReachedEventArgs();
                args.Threshold = threshold;
                args.TimeReached = DateTime.Now;
                OnThresholdReached(args);
            }
        }

        protected virtual void OnThresholdReached(ThresholdReachedEventArgs e)
        {
            ThresholdReached?.Invoke(this, e);

            //EventHandler<ThresholdReachedEventArgs> handler = ThresholdReached;
            //if (handler != null)
            //{
            //    handler(this, e);
            //}
        }

        public event EventHandler<ThresholdReachedEventArgs> ThresholdReached;
    }

    public class ThresholdReachedEventArgs : EventArgs
    {
        public int Threshold { get; set; }
        public DateTime TimeReached { get; set; }
    }

}

