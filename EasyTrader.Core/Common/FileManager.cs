using EasyTrader.Core.Common.JsonEntityVersions;
using Newtonsoft.Json;


namespace EasyTrader.Core.Common
{
    public static class FileManager
    {
        public static bool IsFileExists(string dirPath, string fileName)
        {
            return IsFileExists(Path.Combine(dirPath, fileName));
        }

        public static bool IsFileExists(string path)
        {
            return File.Exists(path);
        }



        public static T ReadJsonFile<T>(string filePath, string fileName)
        {
            var file = ReadFile(filePath, fileName);

            if (file is not null)
            {
                return JsonConvert.DeserializeObject<T>(file);
            }
            return default;
        }

        public static string ReadFile(string filePath, string fileName)
        {
            return ReadFile(Path.Combine(filePath, fileName));
        }

        public static string ReadFile(string filePath)
        {
            try { return File.ReadAllText(filePath); }
            catch (Exception e)
            {
                throw CommonException.CouldNotReadFile(filePath, e);
            }
        }

        public static IEnumerable<List<string>> ReadCsv(string directoryPath, string fileName)
        {
            var lines = File.ReadLines(Path.Combine(directoryPath, fileName));

            IEnumerable<List<string>> csv = lines.Select(line => line.Split(',').ToList());

            return csv;
        }



        /// <summary>
        /// Creates all directories and subdirectories in the specified path unless they already exist.
        ///  Creates a new file, writes the specified string to the file, and then closes
        ///   the file. If the target file already exists, it is overwritten.
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <param name="fileName"></param>
        /// <param name="content"></param>
        public static void SaveFile(string filePath, string fileName, string content)
        {
            Directory.CreateDirectory(filePath);
            File.WriteAllText(Path.Combine(filePath, fileName), content);
        }

        public static void SaveJsonFile<T>(string filePath, string fileName, T jsonObj)
        {
            SaveFile(filePath, fileName, JsonConvert.SerializeObject(jsonObj));
        }

        /// <summary>
        /// Creates all directories and subdirectories in the specified path unless they already exist.
        ///  Opens a file, appends the specified string to the file, and then closes the file.
        ///  If the file does not exist, this method creates a file, writes the specified string to the file, then closes the file.
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <param name="fileName"></param>
        /// <param name="content"></param>
        public static void UpdateFile(string filePath, string fileName, string content)
        {
            Directory.CreateDirectory(filePath);
            File.AppendAllText(Path.Combine(filePath, fileName), content);
        }


        public static TNew TransformJson<TOld, TNew>(string filePath, string fileName)
           where TNew : IEntityProertiesConvertor, IObjectVersionTransform//, new()
           where TOld : IEntityProertiesConvertor
        {
            TOld oldObj = ReadJsonFile<TOld>(filePath, fileName);

            TNew newObj = oldObj.CopyPropertiesTo<TNew>();

            newObj.UpdateNewVersionObjects(oldObj);

            SaveJsonFile(filePath, fileName, newObj);

            return newObj;
        }



        public static void DeleteFiles(string directoryPath)
        {
            foreach (var file in Directory.GetFiles(directoryPath))
            {
                File.Delete(file);
            }
        }

        public static void DeleteFile(string filePath)
        {
            File.Delete(filePath);
        }

        public static void DeleteFile(string directoryPath, string fileName)
        {
            DeleteFile(Path.Combine(directoryPath, fileName));
        }

        public static void DeleteFolder(string directoryPath)
        {
            Directory.Delete(directoryPath, true);
        }
    }
}
