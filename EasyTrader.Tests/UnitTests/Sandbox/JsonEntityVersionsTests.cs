using Newtonsoft.Json;
using System.Diagnostics;
using System.ComponentModel;
using EasyTrader.Core.Common;
using EasyTrader.Core.Common.JsonEntityVersions;

namespace EasyTrader.Tests.UnitTests.Sandbox
{
    [TestClass]
    public class JsonEntityVersionsTests
    {
        public static string TestsOutputDirectoryPath = @"..\..\..\TestsOutput";

        public JsonEntityVersionsTests() { }

        [TestMethod]
        public void TransformObjectVersion_Test()
        {
            //setup
            var fileName = "TransformObjectVersion_TestFile.json";
            var oldObj = new OldMockObject();
            FileManager.SaveJsonFile(TestsOutputDirectoryPath, fileName, oldObj);

            //Test
            NewMockObject newObj =
                FileManager.TransformJson<OldMockObject, NewMockObject>(
                    TestsOutputDirectoryPath, fileName);

        }

    }



    // new object
    public class NewMockObject : EntityProertiesConvertor, IObjectVersionTransform
    {
        #region Old object

        [DisplayName("Dog EntityId")]
        public string DogName { get; set; }

        public long Age { get; set; }

        [DisplayName("Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        public object? DogTagObj { get; set; }
        public object? OldPropertyChanged { get; set; }

        [JsonIgnore]
        public string JsonIgnoreObject { get; set; }

        #endregion 

        public string NewProperty1WithValue { get; set; }
        public string NewProperty2WithoutValue { get; set; }

        public void UpdateNewVersionObjects(dynamic? oldVersion = null) // <TDerived> instead of dynamic
        {
            var oldObj = (OldMockObject)oldVersion;
            NewProperty1WithValue = "Nee Doh";
            OldPropertyChanged = "Gold Coast";
        }
    }

    public class OldMockObject : EntityProertiesConvertor
    {
        public OldMockObject()
        {
            DogName = "Trevor";
            Age = 8;
            DateOfBirth = DateTime.Now;
            DogTagObj = null;
            OldPropertyChanged = "Wellington";
            PropertyToDelete = "Delete this!";
        }


        [DisplayName("Dog EntityId")]
        public string DogName { get; set; }

        public long Age { get; set; }

        [DisplayName("Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        public object? DogTagObj { get; set; }
        public object? OldPropertyChanged { get; set; }

        public string PropertyToDelete { get; set; }

        [JsonIgnore]
        public string JsonIgnoreObject { get; set; }
    }



}
