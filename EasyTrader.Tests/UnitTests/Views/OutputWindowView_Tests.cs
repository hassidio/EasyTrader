using EasyTrader.Core.Views.OutputView.OutputWindowView;
using EasyTrader.Tests.Models.Views;

namespace EasyTrader.Tests.UnitTests.Views
{
    [TestClass()]
    public class OutputWindowView_Tests
    {
        public static string TestsOutputDirectoryPath = @"..\..\..\TestsOutput";

        [TestMethod]
        public void Test_OutputWindow()
        {
            // Setup
            var clientOutput = new MockClientOutputWindowControl();

            // Test
            var outputWindow = new OutputWindow(clientOutput);

            Assert.IsTrue(outputWindow.ClientOutputWindow == clientOutput);
        }

        [TestMethod]
        public void Test_NextId()
        {
            // Setup
            var clientOutput = new MockClientOutputWindowControl();
            var outputWindow = new OutputWindow(clientOutput);

            // Test
            Assert.IsTrue(outputWindow.NextId == (outputWindow.Count + 1).ToString());
        }

        [TestMethod]
        public void Test_GetOutputItemById()
        {
            // Setup
            var dateTimeNow = DateTime.Now;
            var clientOutput = new MockClientOutputWindowControl();
            var outputItem = new OutputWindowItem();
            var id = clientOutput.OutputWindow.NextId;

            // Test
            var noItem = clientOutput.OutputWindow.GetOutputItemById(id);
            Assert.IsTrue(noItem is null);

            clientOutput.OutputWindow.Add(outputItem);

            var itemResult = clientOutput.OutputWindow.GetOutputItemById(id);

            Assert.IsTrue(itemResult is not null);
            Assert.IsTrue(itemResult.Id == id);
        }

        [TestMethod]
        public void Test_OnAdd_New()
        {
            // Setup
            var dateTimeNow = DateTime.Now;
            var clientOutput = new MockClientOutputWindowControl();
            var outputItem = new OutputWindowItem();
            var expectedId = clientOutput.OutputWindow.NextId;
            // Test
            clientOutput.OutputWindow.Add(outputItem);

            Assert.IsTrue(clientOutput.OutputWindow.Count == 1);
            Assert.IsTrue(clientOutput.OutputWindow[0].Id == expectedId);
            Assert.IsTrue(clientOutput.OutputWindow[0].DateTime >= dateTimeNow);
            Assert.IsTrue(clientOutput.OutputWindow[0].Status == OutputItemStatusEnum.InProgress);
        }

        [TestMethod]
        public void Test_OnAdd_Existing()
        {
            // Setup
            var dateTimeNow = DateTime.Now;
            var clientOutput = new MockClientOutputWindowControl();
            var outputItem = new OutputWindowItem();
            var existingId = clientOutput.OutputWindow.NextId;

            clientOutput.OutputWindow.Add(outputItem);
            Assert.IsTrue(clientOutput.OutputWindow[0].Status == OutputItemStatusEnum.InProgress);

            // Test
            outputItem.Status = OutputItemStatusEnum.Success;
            clientOutput.OutputWindow.Add(outputItem);

            Assert.IsTrue(clientOutput.OutputWindow.Count == 1);
            Assert.IsTrue(clientOutput.OutputWindow[0].Id == existingId);
            Assert.IsTrue(clientOutput.OutputWindow[0].DateTime >= dateTimeNow);
            Assert.IsTrue(clientOutput.OutputWindow[0].Status == OutputItemStatusEnum.Success);
        }

        [TestMethod]
        public void Test_OnAdd_UpdateClient()
        {
            // Setup
            var dateTimeNow = DateTime.Now;
            var clientOutput = new MockClientOutputWindowControl();
            var outputItem = new OutputWindowItem();
            var expectedId = clientOutput.OutputWindow.NextId;

            // Test
            Assert.IsTrue(clientOutput.OutputWindow.Count == 0);

            clientOutput.OutputWindow.Add(outputItem);

            Assert.IsTrue(clientOutput.OutputWindow.Count == 1);
            Assert.IsTrue(clientOutput.OutputWindow[0].Id == expectedId);
            Assert.IsTrue(clientOutput.OutputWindow[0].DateTime >= dateTimeNow);
            Assert.IsTrue(clientOutput.OutputWindow[0].Status == OutputItemStatusEnum.InProgress);
        }

        [TestMethod]
        public void Test_AddOutputItem()
        {
            // Setup
            var dateTimeNow = DateTime.Now;
            var clientOutput = new MockClientOutputWindowControl();
            var outputItem = new OutputWindowItem();
            var expectedId = clientOutput.OutputWindow.NextId;

            // Test
            var outputResult = clientOutput.OutputWindow.WriteOutputWindowItem(outputItem);

            Assert.IsTrue(outputResult.Id == expectedId);
            Assert.IsTrue(outputResult.DateTime >= dateTimeNow);
            Assert.IsTrue(outputResult.Status == OutputItemStatusEnum.InProgress);
        }



        [TestMethod]
        public void Test_Clear()
        {
            // Setup
            var clientOutput = new MockClientOutputWindowControl();
            var outputItem = new OutputWindowItem();

            clientOutput.OutputWindow.Add(outputItem);
            Assert.IsTrue(clientOutput.OutputWindow.Count == 1);

            // Test
            clientOutput.OutputWindow.Clear();

            Assert.IsTrue(clientOutput.OutputWindow.Count == 0);
        }

        [TestMethod]
        public void Test_WriteOutputWindowItem()
        {
            // Setup
            var clientOutput = new MockClientOutputWindowControl();

            Assert.IsTrue(clientOutput.OutputWindow.Count == 0);

            var outputItem = clientOutput.OutputWindow.WriteOutputWindowItem();

            Assert.IsTrue(clientOutput.OutputWindow.Count == 1);
            Assert.IsTrue(clientOutput.OutputWindow[0].DateTime > DateTime.Now.AddSeconds(-1));
            Assert.IsTrue(clientOutput.OutputWindow[0].Status == OutputItemStatusEnum.InProgress);
            Assert.IsTrue(clientOutput.OutputWindow[0].Id == "1");
            Assert.IsTrue(clientOutput.OutputWindow[0].Text == null);

            outputItem.Text = "Test";
            outputItem.Status = OutputItemStatusEnum.Success;
            var _now = DateTime.Now;
            outputItem.DateTime = _now;

            clientOutput.OutputWindow.WriteOutputWindowItem(outputItem);

            Assert.IsTrue(clientOutput.OutputWindow[0].DateTime == _now);
            Assert.IsTrue(clientOutput.OutputWindow[0].Id == "1");
            Assert.IsTrue(clientOutput.OutputWindow[0].Status == OutputItemStatusEnum.Success);
            Assert.IsTrue(clientOutput.OutputWindow[0].Text == "Test");

        }
    }
}

