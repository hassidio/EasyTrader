using System.Diagnostics;

namespace EasyTrader.Tests.UnitTests.Sandbox
{
    [TestClass()]
    public class HelpersTests
    {
        public static string TestsOutputDirectoryPath = @"..\..\..\TestsOutput";

        [TestMethod()]
        public void BuildQueryStringTest()
        {
            var querystring = Core.Common.Helpers.BuildQueryString(new Dictionary<string, string> { { "EntityNameId", "123" }, { "MarketName", "Trevor" } });

            Assert.IsTrue(querystring == "EntityNameId=123&MarketName=Trevor");
        }

        #region TaskTests

        [Ignore("ViewTasks Test")]
        [TestMethod]
        public void Tasks_Test()
        {
            // Setup
            int milliseconds1 = 1500;
            int milliseconds2 = 500;

            Debug.WriteLine(GetMessage("Starting Sandbox1_Test..."));

            //Test
            Process(milliseconds1, milliseconds2);
            Debug.WriteLine(GetMessage("End Sandbox1_Test!!!"));

            Thread.Sleep(milliseconds1 + milliseconds2 + 1000);
        }

        protected void Process(int milliseconds1, int milliseconds2)
        {
            Debug.WriteLine(GetMessage("Starting [void] Tasks..."));
            MyClass mayclass1 = Send(milliseconds1, milliseconds2);
            Debug.WriteLine(GetMessage($"Result Task 1: {mayclass1.Name}"));
            Debug.WriteLine(GetMessage("End Tasks!"));
        }

        private MyClass Send(int milliseconds1, int milliseconds2)
        {
            Debug.WriteLine(GetMessage("Starting [Task] Send..."));

            var task1 = AsyncCall(milliseconds1, "Task1");
            var task2 = AsyncCall(milliseconds2, "Task2");

            Debug.WriteLine(GetMessage("End Send"));

            return task1.Result;
        }

        private async Task<MyClass> AsyncCall(int milliseconds, string txt)                                                         //UpdateExchangeInfoTask
        {
            Debug.WriteLine(GetMessage($"Starting [async Task] AsyncCall...{txt} with {milliseconds} milliseconds"));
            await Task.Delay(milliseconds);
            Debug.WriteLine(GetMessage($"End AsyncCall: {txt} completed after {milliseconds} milliseconds"));

            return new MyClass() { Name = txt };
        }
        private static string GetMessage(string txt)
        {
            //var time = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt");
            var time = DateTime.Now.ToString("mm:ss.fff");
            return $"{time}: {txt}";
        }

        private class MyClass
        {
            public string Name { get; set; }
        }

        #endregion

        #region SaveToCsvTests

        //[TestMethod]
        //public void Test_CSV()
        //{
        //    var saveToCsvMock = new MockObject
        //    {
        //        DogName = "Trevor",
        //        Age = 8,
        //        DateOfBirth = DateTime.Now,
        //        DogTagObj = new Animal(8, "Spider"),
        //        DogType = new Animal(4, "White"),
        //        MyEnumTest = MyEnum_Test.Enum1,

        //        StringArray_Test = ["Dog1 Array", "Dog Array2 test"],
        //        ObjectArray_Test = [new Animal(2, "Brown"), new Animal(3, "Green")],
        //        ClassArray_Test = [new Animal(10, "Black"), new Animal(9, "Yellow")],
        //        ClassList_Test = new List<Animal>([new Animal(13, "aaa"), new Animal(14, "bbb")]),
        //        ClassCollection_Test =
        //            new HashSet<Animal>([new Animal(20, "Magenta"), new Animal(21, "Cyan")]),


        //        CatOfAnimal_Property = new CatOfAnimal(4, "Grey") { CatName = "Sushi", },
        //        Cat_Property = new Cat() { CatOfAnimal_Property = new CatOfAnimal(44, "Grey44") { CatName = "Mizti", }, }
        //    };

        //   

        //    string path = TestsOutputDirectoryPath + @"\testcsvMock.csv";

        //    var objectToCsv = new ObjectToCsv();

        //    objectToCsv.SaveCsv(saveToCsvMock, path);

        //}

        ////[TestMethod]
        //public void Test_CSV_ExchangeInfo()
        //{
        //var exchangeInfo =
        //    FileManager.ReadJsonFile<BinanceApi_ExchangeInfo>(
        //        TestsOutputDirectoryPath,
        //        "BinanceApi_ExchangeInfo_Test.txt");

        //string path = TestsOutputDirectoryPath + @"\testcsv_ExchangeInfo.csv";

        //var objectToCsv = new ObjectToCsv();

        //objectToCsv.SaveCsv(exchangeInfo, path);
        //}
        #endregion

    }
}