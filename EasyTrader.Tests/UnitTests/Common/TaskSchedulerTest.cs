using EasyTrader.Core.Common.Tasks;
using EasyTrader.Tests.UnitTests.Sandbox;
using System.Diagnostics;

namespace EasyTrader.Tests.UnitTests.Common
{
    [TestClass]
    public partial class TaskSchedulerTest //: //FixtureBase   //:ProcessTestBase // //:UnitTestBase //
    {
        //[TestMethod]
        public void TraderTaskScheduler_Sandbox1_Fail_Test()
        {
            var ts = new TraderTaskScheduler_Dev();
            //Debug.WriteLine($"{DateTime.Now.ToString("mm:ss.fff")}: start test");

            var task1 = new Task(() =>
                Debug.WriteLine($"{DateTime.Now.ToString("mm:ss.fff")}: run task1"));
            var task2 = new Task(() =>
                Debug.WriteLine($"{DateTime.Now.ToString("mm:ss.fff")}: run task2"));

            task1.Start();
            task2.Start();

            Thread.Sleep(100);
        }

        //[TestMethod]
        public void TraderTaskScheduler_Sandbox2_Dev_Test()
        {
            TaskIdResult = new List<string>();
            int? sleep = 100;// 500; //10
            int numberofTasks = 5;
            int baseId = 100;
            var taskScheduler = new TraderTaskScheduler_Dev();
            Debug.WriteLine($"{TimeStamp}: start test *******************");

            //var task = new Task(() => { MockTask(200, sleep); });
            //taskScheduler.QueueTask(task, 200);

            //var task2 = new Task(() => { MockTask(300, sleep); });
            //taskScheduler.QueueTask(task2, 300);

            //var task0 = new Task(() => { MockTask(0, sleep); });
            //taskScheduler.QueueTask(task0, 0);


            for (int i = baseId; i < baseId + numberofTasks; i++)
            {
                //Thread.Sleep(1);
                Debug.WriteLine($"{TimeStamp}: i={i}");

                var task = new Task(() => { MockTask(i.ToString(), sleep); });
                taskScheduler.QueueTask(task, i);
            }

            Debug.WriteLine($"{TimeStamp}: end test *******************");

            Debug.WriteLine($"{TimeStamp}: ");
            Debug.WriteLine($"");
            taskScheduler.WaitAll();

            //if (sleep is not null) { Thread.Sleep(numberofTasks * (int)sleep); }

            foreach (var item in TaskIdResult)
            {
                Debug.WriteLine($"{TimeStamp}: Expected result TaskIdResult <<<>>> This is task no.{item}");
            }
        }


        [TestMethod]
        public void TraderTaskScheduler_Test()
        {
            // Setup
            TaskIdResult = new List<string>();
            MockTasks = new List<Task>();

            int? sleep = 100;// 500; //10
            int numberofTasks = 20;
            int baseId = 100;
            var taskScheduler = new CommonTaskScheduler();

            Debug.WriteLine($"{TimeStamp}: start test *******************");

            // Test
            for (int i = baseId; i < baseId + numberofTasks; i++)
            {
                var task = new Task(() => { MockTask(i.ToString(), sleep); });
                MockTasks.Add(task);
                taskScheduler.QueueTask(task);
            }

            Debug.WriteLine($"{TimeStamp}: end test *******************");

            Debug.WriteLine($"{TimeStamp}: ");
            Debug.WriteLine($"");

            taskScheduler.WaitAll();

            Assert.IsTrue(!taskScheduler.IsRunning);

            foreach (var task in MockTasks)
            {
                Assert.IsTrue(task.IsCompleted);
                Assert.IsTrue(task.IsCompletedSuccessfully);
            }


        }


        public string TimeStamp { get { return $"{DateTime.Now.ToString("mm:ss.fff")} Thread[{Thread.CurrentThread.ManagedThreadId}]"; } }

        public List<string> TaskIdResult { get; set; }
        public List<Task> MockTasks { get; set; }

        private void MockTask(string i, int? sleep)
        {
            TaskIdResult.Add(i);

            if (sleep is not null)
            {
                //Debug.WriteLine($"{TimeStamp}: Going to sleep - i[{i}]<-----------------");
                Thread.Sleep((int)sleep);
                //Debug.WriteLine($"{TimeStamp}: MockTask Sleept for {sleep} - i[{i}] ----------------->");
            }

            Console.WriteLine($"{TimeStamp}: Result >>> This is task no.{i}");
            Debug.WriteLine($"{TimeStamp}: Result >>> This is task no.{i}");
        }

    }
}