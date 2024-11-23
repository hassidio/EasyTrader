using EasyTrader.Tests.Models;
using EasyTrader.Core.Common;
using Newtonsoft.Json;
using EasyTrader.Core.PersistenceServices.IO;

namespace EasyTrader.Tests.UnitTests.PersistenceServices.IO
{
    [TestClass()]
    public class JsonDataFilesPersistenceServiceTests : UnitTestBase
    {
        [TestMethod()]
        public void JsonDataFilesPersistenceServiceTest()
        {
            var ps = new JsonDataFilesPersistenceService(DataDirectoryPathTest);

            Assert.IsTrue(ps.DataDirectoryPath == DataDirectoryPathTest);
        }

        [TestMethod()]
        public void GetTest()
        {
            // Setup
            var ps = new JsonDataFilesPersistenceService(DataDirectoryPathTest);
            var mockEntity = new MockEntity();
            mockEntity.SetEntity(id: "Object1");

            var dirPath = Path.Combine(DataDirectoryPathTest, "Object1");
            var fileName = "Object1.json";

            FileManager.SaveJsonFile(dirPath, fileName, mockEntity);

            //Test
            var mockEntityResult = ps.Get<MockEntity>(id: "Object1");
            Assert.IsTrue(mockEntityResult is not null);

            // Clean up
            var filePath = Path.Combine(dirPath, fileName);
            FileManager.DeleteFolder(dirPath);
            Assert.IsFalse(FileManager.IsFileExists(filePath));
        }

        [TestMethod()]
        public void GetWithParentTest()
        {
            // Setup
            var ps = new JsonDataFilesPersistenceService(DataDirectoryPathTest);
            var mockEntity = new MockEntity();
            mockEntity.SetEntity(id: "Object", parentId: "Parent");

            var dirPathParent = Path.Combine(DataDirectoryPathTest, "Parent");
            var dirPath = Path.Combine(dirPathParent, "Object");
            var fileName = "Parent_Object.json";

            FileManager.SaveJsonFile(dirPath, fileName, mockEntity);

            //Test
            var mockEntityResult = ps.Get<MockEntity>(id: "Object", parentId: "Parent");
            Assert.IsTrue(mockEntityResult is not null);

            // Clean up
            var filePath = Path.Combine(dirPath, fileName);
            FileManager.DeleteFolder(dirPathParent);
            Assert.IsFalse(FileManager.IsFileExists(filePath));
        }

        [TestMethod()]
        public void SaveTest()
        {
            // Setup
            var ps = new JsonDataFilesPersistenceService(DataDirectoryPathTest);
            var mockEntity = new MockEntity();
            mockEntity.SetEntity(id: "Object1");

            Assert.IsTrue(mockEntity.EntityId == "Object1");
            Assert.IsTrue(mockEntity.EntityParentId == null);

            //Test
            ps.Save(mockEntity);

            var dirPath = Path.Combine(DataDirectoryPathTest, "Object1");
            var filePath = Path.Combine(dirPath, "Object1.json");
            Assert.IsTrue(FileManager.IsFileExists(filePath));

            // Clean up
            FileManager.DeleteFolder(dirPath);
            Assert.IsFalse(FileManager.IsFileExists(filePath));
        }

        [TestMethod()]
        public void SaveWithParentTest()
        {
            // Setup
            var ps = new JsonDataFilesPersistenceService(DataDirectoryPathTest);
            var mockEntity = new MockEntity();
            mockEntity.SetEntity(id: "Object", parentId: "Parent");

            Assert.IsTrue(mockEntity.EntityId == "Object");
            Assert.IsTrue(mockEntity.EntityParentId == "Parent");

            //Test
            ps.Save(mockEntity);

            var dirPathParent = Path.Combine(DataDirectoryPathTest, "Parent");
            var dirPath = Path.Combine(dirPathParent, "Object");
            var fileName = "Parent_Object.json";
            var filePath = Path.Combine(dirPath, fileName);

            Assert.IsTrue(FileManager.IsFileExists(filePath));

            // Clean up
            FileManager.DeleteFolder(dirPathParent);
            Assert.IsFalse(FileManager.IsFileExists(filePath));
        }

        [TestMethod()]
        public void DeleteTest()
        {
            // Setup
            var ps = new JsonDataFilesPersistenceService(DataDirectoryPathTest);
            var mockEntity = new MockEntity();
            mockEntity.SetEntity(id: "Object", parentId: "Parent");

            Assert.IsTrue(mockEntity.EntityId == "Object");
            Assert.IsTrue(mockEntity.EntityParentId == "Parent");

            ps.Save(mockEntity);

            var dirPathParent = Path.Combine(DataDirectoryPathTest, "Parent");
            var dirPath = Path.Combine(dirPathParent, "Object");
            var fileName = "Parent_Object.json";
            var filePath = Path.Combine(dirPath, fileName);

            Assert.IsTrue(FileManager.IsFileExists(filePath));

            //Test
            ps.Delete(mockEntity);
            Assert.IsFalse(FileManager.IsFileExists(filePath));

            // Clean up
            FileManager.DeleteFolder(dirPathParent);
        }

        [TestMethod()]
        public void GetParentTest()
        {

            // Setup
            var parentName = "Parent";
            var objectName = "Object";

            var ps = new JsonDataFilesPersistenceService(DataDirectoryPathTest);

            // Setup Parent
            var dirPathParent = Path.Combine(DataDirectoryPathTest, parentName);
            var filePathParent = Path.Combine(dirPathParent, $"{parentName}.json");
            var mockEntityParent = new MockEntity();
            mockEntityParent.SetEntity(id: parentName);

            Assert.IsTrue(mockEntityParent.EntityId == parentName);
            Assert.IsTrue(mockEntityParent.EntityParentId == null);

            ps.Save(mockEntityParent);
            Assert.IsTrue(FileManager.IsFileExists(filePathParent));

            // Setup Object
            var dirPathObject = Path.Combine(dirPathParent, objectName);
            var filePathObject = Path.Combine(dirPathObject, $"{parentName}_{objectName}.json");
            var mockEntityObject = new MockEntity();
            mockEntityObject.SetEntity(id: objectName, parentId: parentName);

            Assert.IsTrue(mockEntityObject.EntityId == objectName);
            Assert.IsTrue(mockEntityObject.EntityParentId == parentName);

            ps.Save(mockEntityObject);
            Assert.IsTrue(FileManager.IsFileExists(filePathObject));

            //Test object with parent
            var resultObject = ps.Get<MockEntity>(objectName, parentName);
            Assert.IsNotNull(resultObject);

            var resultParent = resultObject.GetDeserializedEntityParent<MockEntity>();
            Assert.IsNotNull(resultParent);
            Assert.IsTrue(resultObject.EntityId == objectName);
            Assert.IsTrue(resultParent.EntityId == parentName);

            //Test object without parent
            var resultParent2 = ps.Get<MockEntity>(parentName);

            Assert.IsNotNull(resultParent2);

            // Clean up
            FileManager.DeleteFolder(dirPathParent);
        }

        [TestMethod()]
        public void ArchiveFullFileTest() //*/ Archive
        {
            
            //// Setup Parent
            //var ps = new JsonDataFilesPersistenceService(DataDirectoryPathTest);
            //var dirPathParent = Path.Combine(DataDirectoryPathTest, "Parent");
            //var filePathParent = Path.Combine(dirPathParent, "Parent.json");
            //var mockEntityParent = new MockEntity();
            //mockEntityParent.SetEntity(id: "Parent");

            //Assert.IsTrue(mockEntityParent.EntityId == "Parent");
            //Assert.IsTrue(mockEntityParent.EntityParentId == null);

            //ps.Save(mockEntityParent);
            //Assert.IsTrue(FileManager.IsFileExists(filePathParent));

            //// Setup Object
            //var dirPathObject = Path.Combine(dirPathParent, "Object");
            //var filePathObject = Path.Combine(dirPathObject, "Parent_Object.json");
            //var mockEntityObject = new MockEntity();
            //mockEntityObject.SetEntity(id: "Object", parentId: "Parent");

            //Assert.IsTrue(mockEntityObject.EntityId == "Object");
            //Assert.IsTrue(mockEntityObject.EntityParentId == "Parent");

            //ps.Save(mockEntityObject);
            //Assert.IsTrue(FileManager.IsFileExists(filePathObject));

            ////Test object with parent
            //var resultObject = ps.GetParent<MockEntity>(mockEntityObject);
            //Assert.IsTrue(resultObject.EntityId == "Parent");

            ////Test object without parent
            //var result2 = ps.GetParent<MockEntity>(mockEntityParent);

            //Assert.IsTrue(result2 is null);

            //// Clean up
            //FileManager.DeleteFolder(dirPathParent);
        }


        [TestMethod()]
        public void SaveFragmentedDurations_Entity_Test()
        {
            // Setup
            var parentName = "Parent";
            var objectName = "Object";

            var ps = new JsonDataFilesPersistenceService(DataDirectoryPathTest);
            var totalNumberOfRecords = 40000;

            // Setup Parent
            var dirPathParent = Path.Combine(DataDirectoryPathTest, parentName);
            var filePathParent = Path.Combine(dirPathParent, $"{parentName}.json");
            var mockEntityParent = new MockEntity();
            mockEntityParent.SetEntity(id: parentName);
            ps.Save(mockEntityParent);

            // Setup Object
            var dirPathObject = Path.Combine(dirPathParent, objectName);
            var filePathObject = Path.Combine(dirPathObject, $"{parentName}_{objectName}.json");
            var mockEntityObject = new MockEntity();
            dynamic clientData = NewRawClientData(totalNumberOfRecords);
            string clientDataString = JsonConvert.SerializeObject(clientData);
            mockEntityObject.SetEntity(id: objectName, parentId: parentName, rawClientData: clientDataString);
            ps.Save(mockEntityObject);

            Assert.IsTrue(FileManager.IsFileExists(filePathObject));
            string[] files = Directory.GetFiles(dirPathObject);

            var fragmentsCountExpected =
                totalNumberOfRecords / ps.ClientDataSetsDurationFragmentRowCountPolicy
                - (totalNumberOfRecords % ps.ClientDataSetsDurationFragmentRowCountPolicy == 0 ? 1 : 0);

            Assert.IsTrue(files.Length == fragmentsCountExpected + 1);

            foreach (var filePath in files)
            {
                var fileName = Path.GetFileName(filePath);
                Assert.IsTrue(fileName.StartsWith($"{parentName}_{objectName}"));
                Assert.IsTrue(fileName.EndsWith(".json"));
                fileName = fileName.Substring(0, fileName.LastIndexOf(".json"));

                var fileNameArray = fileName.Split('_');
                var file = FileManager.ReadFile(filePath);
                MockEntity entity = JsonConvert.DeserializeObject<MockEntity>(file);
                var clientDataResult = entity.ClientDataArray.ToObject<List<string>>();

                if (fileNameArray.Length > 2)
                {
                    Assert.IsTrue(fileNameArray.Length == 4);
                    Assert.IsTrue(clientDataResult.Count == ps.ClientDataSetsDurationFragmentRowCountPolicy);
                    Assert.IsTrue(clientDataResult.First() == fileNameArray[2]);
                    Assert.IsTrue(clientDataResult.Last() == fileNameArray[3]);
                }
                else
                {
                    Assert.IsTrue(fileNameArray.Length == 2);

                    var numberOfRecordsInCurrentEntity =
                        totalNumberOfRecords % ps.ClientDataSetsDurationFragmentRowCountPolicy == 0 ?
                        ps.ClientDataSetsDurationFragmentRowCountPolicy
                        : totalNumberOfRecords % ps.ClientDataSetsDurationFragmentRowCountPolicy;

                    Assert.IsTrue(clientDataResult.Count == numberOfRecordsInCurrentEntity);
                }
            }

            // Clean up
            FileManager.DeleteFolder(dirPathParent);

        }

        [TestMethod()]
        public void SaveFragmentedDurations_Market_Test()
        {
            // Setup
            var parentName = "Exchange1";
            var objectName = "Market1";

            var ps = new JsonDataFilesPersistenceService(DataDirectoryPathTest);
            var totalNumberOfRecords = 40000;

            // Setup Parent
            var dirPathParent = Path.Combine(DataDirectoryPathTest, parentName);
            var filePathParent = Path.Combine(dirPathParent, $"{parentName}.json");
            var mockEntityParent = new MockEntity();
            mockEntityParent.SetEntity(id: parentName);
            ps.Save(mockEntityParent);

            // Setup Object
            var dirPathObject = Path.Combine(dirPathParent, objectName);
            var filePathObject = Path.Combine(dirPathObject, $"{parentName}_{objectName}.json");
            var mockEntityObject = new MockMarket();
            dynamic clientData = NewCandles(totalNumberOfRecords); //NewRawClientData(totalNumberOfRecords);
            string clientDataString = JsonConvert.SerializeObject(clientData);
            mockEntityObject.SetEntity(id: objectName, parentId: parentName, rawClientData: clientDataString);
            ps.Save(mockEntityObject);

            Assert.IsTrue(FileManager.IsFileExists(filePathObject));
            string[] files = Directory.GetFiles(dirPathObject);

            var fragmentsCountExpected =
                totalNumberOfRecords / ps.ClientDataSetsDurationFragmentRowCountPolicy
                - (totalNumberOfRecords % ps.ClientDataSetsDurationFragmentRowCountPolicy == 0 ? 1 : 0);

            Assert.IsTrue(files.Length == fragmentsCountExpected + 1);

            foreach (var filePath in files)
            {
                var fileName = Path.GetFileName(filePath);
                Assert.IsTrue(fileName.StartsWith($"{parentName}_{objectName}"));
                Assert.IsTrue(fileName.EndsWith(".json"));
                fileName = fileName.Substring(0, fileName.LastIndexOf(".json"));

                var fileNameArray = fileName.Split('_');
                var file = FileManager.ReadFile(filePath);
                var entity = JsonConvert.DeserializeObject<MockMarket>(file);

                List<MockCandle>? clientDataResult = entity.ClientDataArray.ToObject<List<MockCandle>>();

                if (fileNameArray.Length > 2)
                {
                    Assert.IsTrue(fileNameArray.Length == 4);
                    Assert.IsTrue(clientDataResult.Count == ps.ClientDataSetsDurationFragmentRowCountPolicy);
                    Assert.IsTrue(clientDataResult.First().OpenDateTime.Ticks.ToString() == fileNameArray[2]);
                    Assert.IsTrue(clientDataResult.Last().OpenDateTime.Ticks.ToString() == fileNameArray[3]);
                }
                else
                {
                    Assert.IsTrue(fileNameArray.Length == 2);

                    var numberOfRecordsInCurrentEntity =
                        totalNumberOfRecords % ps.ClientDataSetsDurationFragmentRowCountPolicy == 0 ?
                        ps.ClientDataSetsDurationFragmentRowCountPolicy
                        : totalNumberOfRecords % ps.ClientDataSetsDurationFragmentRowCountPolicy;

                    Assert.IsTrue(clientDataResult.Count == numberOfRecordsInCurrentEntity);
                }
            }

            // Clean up
            FileManager.DeleteFolder(dirPathParent);

        }


        public static dynamic NewRawClientData(int countRows)
        {
            dynamic rawClientdata = new List<dynamic>();

            var rows = NewClientDataRows(countRows);

            foreach (var item in rows)
            {
                rawClientdata.Add(item);
            }

            return rawClientdata;
        }

        private static IList<string> NewClientDataRows(int count, string? clientId = "")
        {
            var list = new List<string>();
            for (int i = 0; i < count; i++)
            {
                list.Add(i.ToString());
            }

            return list;
        }
    }
}