using EasyTrader.Core.Views.DataTableView;
using EasyTrader.Core.Views.PropertyView;
using Newtonsoft.Json;
using System.Diagnostics;

namespace EasyTrader.Tests.UnitTests.Views
{
    [TestClass()]
    public class DataTableView_Tests
    {
        public static string TestsOutputDirectoryPath = @"..\..\..\TestsOutput";

        [Ignore]
        [TestMethod]
        public void Test_CommonDataTableAndPropertyDataTable()
        {
            // Setup
            var obj = new SomeObject()
            {
                StringProperty = "Some String",
                RefClassProperty = new SomeObject()
            };
            var jsondtobj = JsonConvert.SerializeObject(obj);
            Assert.IsTrue(jsondtobj == "{\"StringProperty\":\"Some String\",\"RefClassProperty\":{\"StringProperty\":null,\"RefClassProperty\":null}}");

            // Test
            var dtCommon = new CommonDataTable(obj, "obj");
            var dtProperty = new PropertyDataTable(obj, "obj");

            var jsondtCommon = JsonConvert.SerializeObject(dtCommon);
            var jsondtProperty = JsonConvert.SerializeObject(dtProperty);

            Assert.IsTrue(jsondtCommon == "");
            Assert.IsTrue(jsondtProperty == "");
        }

        [TestMethod]
        public void Test_PropertyDataValuesAndRefClass()
        {
            // Setup
            var obj = new SomeObject()
            {
                StringProperty = "Some String",
                RefClassProperty = new SomeObject()
            };
            var jsondtobj = JsonConvert.SerializeObject(obj);
            Assert.IsTrue(jsondtobj == "{\"StringProperty\":\"Some String\",\"RefClassProperty\":{\"StringProperty\":null,\"RefClassProperty\":null}}");

            // Test
            var pd = new PropertyData(obj);

            var jsondtProperty = JsonConvert.SerializeObject(pd);
            var expectedResult = "{\"PropertyDataAttribute\":null,\"Properties\":[{\"PropertyName\":\"StringProperty\",\"PropertyDisplayName\":\"StringProperty\",\"PropertyNameView\":\"StringProperty\",\"PropertyValue\":\"Some String\",\"PropertyType\":\"System.String, System.Private.CoreLib, Version=8.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\",\"IsValue\":true,\"IsClass\":false,\"IsSet\":false}],\"ChildObjects\":[{\"PropertyDataAttribute\":{\"DisplayName\":\"RefClassProperty\",\"TypeId\":\"EasyTrader.Core.Views.PropertyView.PropertyDataAttribute, EasyTrader.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\"},\"Properties\":[{\"PropertyName\":\"StringProperty\",\"PropertyDisplayName\":\"StringProperty\",\"PropertyNameView\":\"StringProperty\",\"PropertyValue\":null,\"PropertyType\":\"System.String, System.Private.CoreLib, Version=8.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\",\"IsValue\":true,\"IsClass\":false,\"IsSet\":false},{\"PropertyName\":\"RefClassProperty\",\"PropertyDisplayName\":\"RefClassProperty\",\"PropertyNameView\":\"RefClassProperty\",\"PropertyValue\":null,\"PropertyType\":\"System.Object, System.Private.CoreLib, Version=8.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\",\"IsValue\":false,\"IsClass\":true,\"IsSet\":false}],\"ChildObjects\":[],\"PropertyName\":\"RefClassProperty\",\"PropertyDisplayName\":null,\"PropertyNameView\":\"RefClassProperty\",\"PropertyValue\":null,\"PropertyType\":\"EasyTrader.Tests.UnitTests.Views.DataTableView_Tests+SomeObject, EasyTrader.Tests, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\",\"IsValue\":false,\"IsClass\":true,\"IsSet\":false}],\"PropertyName\":null,\"PropertyDisplayName\":null,\"PropertyNameView\":null,\"PropertyValue\":null,\"PropertyType\":\"EasyTrader.Tests.UnitTests.Views.DataTableView_Tests+SomeObject, EasyTrader.Tests, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\",\"IsValue\":false,\"IsClass\":true,\"IsSet\":false}";
            
            Debug.WriteLine(jsondtProperty);
            Assert.IsTrue(jsondtProperty == expectedResult);

        }

        [Ignore]
        [TestMethod]
        public void Test_PropertyDataCollections()
        {
            throw new NotImplementedException();
        }

        public class SomeObject
        {
            [PropertyData("StringProperty")]
            public string StringProperty { get; set; }

            [PropertyData("RefClassProperty")]
            public object RefClassProperty { get; set; }
        }
    }
}

