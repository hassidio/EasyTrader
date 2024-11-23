using Newtonsoft.Json;
using EasyTrader.Tests.Models;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using EasyTrader.Tests.UnitTests.PersistenceServices.IO;

namespace EasyTrader.Tests.UnitTests.Sandbox
{

    //public class MyJArray : JArray
    //{

    //}
    [TestClass]
    public class SandboxTests //: FixtureBase   //:ProcessTestBase // //:UnitTestBase //
    {
        public static string TestsOutputDirectoryPath = @"..\..\..\TestsOutput";


        public SandboxTests()
        {
        }
        private int _count = 0;

        [TestMethod]
        public void Sandbox2_Test()
        {
            //JArray mySet = JsonDataFilesPersistenceServiceTests.NewRawClientDataJArray(1);
            var mySet = JsonDataFilesPersistenceServiceTests.NewRawClientData(1);
            string mySetString = JsonConvert.SerializeObject(mySet);

            MockEntity entity = new MockEntity();
            entity.SetEntity(rawClientData: mySetString);

            //string content = JsonConvert.SerializeObject(entity.RawClientData);
            //var contentResult = JsonConvert.DeserializeObject<dynamic>(content);


            string content1 = JsonConvert.SerializeObject(entity);
            var contentResult1 = JsonConvert.DeserializeObject<MockEntity>(content1);

            var filePath = Path.Combine(TestsOutputDirectoryPath, "mySet.json");

            File.WriteAllText(filePath, content1);

            var file = File.ReadAllText(filePath);

            //var converter = new ExpandoObjectConverter();
            
            var entityResult = JsonConvert.DeserializeObject<MockEntity>(file);

        }




        private void PrintHeaders(
            string taskName,
            IDictionary<string, IEnumerable<string>> headres)
        {
            _count++;
            //
            var date = ((string[])headres["Date"])[0];
            var usedWeight = ((string[])headres["x-mbx-used-weight"])[0];
            var usedWeight1m = ((string[])headres["x-mbx-used-weight-1m"])[0];

            Debug.WriteLine($"{taskName}:\tUsed Weight: {usedWeight}\tUsed Weight 1m: {usedWeight1m}\tDate: {date}");
        }



    }

}