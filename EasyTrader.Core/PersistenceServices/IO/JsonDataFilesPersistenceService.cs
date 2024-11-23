using EasyTrader.Core.Common;
using EasyTrader.Core.Models.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO.Compression;

namespace EasyTrader.Core.PersistenceServices.IO
{
    public class JsonDataFilesPersistenceService : IPersistenceService
    {
        public int ClientDataSetsDurationFragmentRowCountPolicy = 10000; //*/1 move to TraderConfiguration

        public JsonDataFilesPersistenceService(string dataDirectoryPath)
        {
            DataDirectoryPath = dataDirectoryPath;
        }

        public string DataDirectoryPath { get; private set; }

        private static string _FileExtension = "json";

        public T Get<T>(string id, string? parentId = null) where T : IEntity
        {
            var filePath = GetFullFilePath(id: id, parentId: parentId);


            var entity = GetEntity<T>(filePath);

            if (entity is not null && !string.IsNullOrEmpty(entity.EntityParentId))
            {
                var filePathParent = 
                    GetFileNameWithExtention(
                        GetFullFilePath(id: entity.EntityParentId));

                entity.SerializedEntityParent = GetFile(filePathParent);
            }

            //if (entityChild.IsRawClientDataTypeIEnumerable) //*/ do fragments
            //{
            //    var clientDataArray = GetFragments<TParent>(fileDirPath, fileName);
            //    entityChild.SetEntity(rawClientData: clientDataArray);
            //}

            return entity;
        }

        public void Save<T>(T item, bool? isArchive = false) where T : IEntity
        {
            string fileDirPath = GetFileDirectoryPath(id: item.EntityId, parentId: item.EntityParentId);
            string fileName = GetFileName(id: item.EntityId, parentId: item.EntityParentId);

            //if (item.IsRawClientDataTypeIEnumerable) //*/ fragments
            //{
            //    item = SplitFragmentedDurations(item);
            //}

            SaveEntity(fileDirPath, fileName, item);

            if (isArchive is not null && (bool)isArchive) { Archive(item); }

        }

        public void Delete<T>(T item) where T : IEntity
        {
            string fileDirPath = GetFileDirectoryPath(id: item.EntityId, parentId: item.EntityParentId);
            string fileName = GetFileName(item.EntityId, item.EntityParentId);
            fileName = $"{fileName}.{_FileExtension}";

            File.Delete(Path.Combine(fileDirPath, fileName));
        }

        public void Archive<T>(T item) where T : IEntity
        {
            // Save in Archive folder
            string fileDirPath = GetFileDirectoryPath(
                id: item.EntityId, parentId: item.EntityParentId, isArchive: true);

            string fileName = GetFileName(
                id: item.EntityId,
                parentId: item.EntityParentId,
                stamp: item.EntityStamp);

            // Delete older archive (i.e. 3 months policy) //*/ todo
        }


        // Privates

        private static dynamic GetFragments<T>(string fileDirPath, string fileName) where T : IEntity
        {
            var filePaths = Directory.GetFiles(fileDirPath);

            JArray clientDataArray = null;
            foreach (var fp in filePaths)
            {
                if (Path.GetFileName(fp).StartsWith(fileName))
                {
                    var entity = GetEntity<T>(fp);

                    if (clientDataArray is null)
                    {
                        clientDataArray = entity.RawClientData;
                    }
                    else
                    {
                        foreach (var item in entity.RawClientData)
                        {
                            clientDataArray.Add(item);
                        }
                    }
                }
            }

            return clientDataArray;
        }

        private T SplitFragmentedDurations<T>(T item) where T : IEntity
        {
            var dataSets = item.GetRawClientDataSets(ClientDataSetsDurationFragmentRowCountPolicy);

            if (dataSets is null) { return item; }

            var currentItemSet = dataSets.Last();

            dataSets.Remove(currentItemSet);
            string fileDirPath = GetFileDirectoryPath(id: item.EntityId, parentId: item.EntityParentId);


            foreach (var itemSet in dataSets)
            {
                item.SetEntity(rawClientData: itemSet);

                string fileName = GetFileName(id: item.EntityId, parentId: item.EntityParentId, stamp: item.EntityStamp);

                SaveEntity(fileDirPath, fileName, item);
            }

            item.SetEntity(rawClientData: currentItemSet);

            return item;
        }

        private string GetFullFilePath(string id, string? parentId = null, bool isArchive = false, string? stamp = null)
        {
            string fileDirPath = GetFileDirectoryPath(id: id, parentId: parentId, isArchive: isArchive);
            string fileName = GetFileName(id: id, parentId: parentId, stamp: stamp);
            var filePath = Path.Combine(fileDirPath, fileName);

            return filePath;
        }

        private string GetFileDirectoryPath(string id, string? parentId = null, bool isArchive = false)
        {
            string fileDirPath = DataDirectoryPath;

            if (!string.IsNullOrEmpty(parentId))
            {
                fileDirPath = Path.Combine(fileDirPath, parentId);
            }

            fileDirPath = Path.Combine(fileDirPath, id);

            if (isArchive) { fileDirPath = Path.Combine(fileDirPath, "Archive"); }

            return fileDirPath;
        }

        private string GetFileName(string id, string? parentId = null, string? stamp = null)
        {
            string fileName = $"{id}";

            if (!string.IsNullOrEmpty(parentId))
            {
                fileName = $"{parentId}_{fileName}";
            }

            if (!string.IsNullOrEmpty(stamp))
            {
                fileName = $"{fileName}_{stamp}";
            }

            return fileName;
        }

        private static string? GetFileNameWithExtention(string filePath)
        {
            var fileName = filePath;

            if (!fileName.EndsWith($".{_FileExtension}"))
            {
                fileName = $"{filePath}.{_FileExtension}";
            }

            return fileName;
        }

        private static string? GetFile(string filePath)
        {
            if (!File.Exists(filePath)) { return null; }

            try { return File.ReadAllText(filePath); }
            catch (Exception e)
            {
                throw CommonException.CouldNotReadFile(filePath, e);
            }
        }

        private static T GetEntity<T>(string filePath)
        {
            var fileName = GetFileNameWithExtention(filePath);

            var file = GetFile(fileName);

            if (file is not null)
            {
                return JsonConvert.DeserializeObject<T>(file);
            }
            return default;
        }

        private static void SaveEntity<T>(string dirPath, string fileName, T item)
        {
            var content = JsonConvert.SerializeObject(item);

            fileName = $"{fileName}.{_FileExtension}";

            Directory.CreateDirectory(dirPath);
            File.WriteAllText(Path.Combine(dirPath, fileName), content);
        }

    }
}
