using System.ComponentModel;

namespace EasyTrader.Core.Views.CSV
{
    public class ObjectToCsv
    {
        public void SaveCsv<T>(T obj, string path)
        {
            File.WriteAllLines(path, GetCsvObjectLines(obj, string.Empty, null));
        }

        private IList<string> GetCsvObjectLines<T>(T obj, string objPropertyFullName, int? index)
        {
            var lines = new List<string>();

            // Add Header line
            var header = GetCsvHeaderLine(obj, objPropertyFullName);
            if (header != null) { lines.Add(header); }

            // Add values line
            var valueLine = GetCsvValueLines(obj, objPropertyFullName, index);
            lines.AddRange(valueLine);

            return lines;
        }


        private string GetCsvHeaderLine<T>(T obj, string objPropertyFullName)
        {
            var header = string.Empty;

            //if (obj == null) { return null; }

            Type objType = obj.GetType();

            IEnumerable<PropertyDescriptor> props = TypeDescriptor.GetProperties(objType).OfType<PropertyDescriptor>();

            // Add Header line
            if (!IsValueType(objType))
            {
                header = ",Properties,";
                if (objPropertyFullName != string.Empty) { header = objPropertyFullName + header; }
                else { header = obj.GetType().Name + header; }
                header += string.Join(",", props.ToList().Select(x => x.Name));
                return header;
            }
            return null;
        }

        private IList<string> GetCsvValueLines<T>(T obj, string objPropertyFullName, int? index)
        {
            var lines = new List<string>();

            Type objType = obj.GetType();

            IEnumerable<PropertyDescriptor> props = TypeDescriptor.GetProperties(objType).OfType<PropertyDescriptor>();

            // Add values line
            var valueLine = ",Values";
            if (index != null) { valueLine += "[" + index + "]"; }
            valueLine += ",";
            if (objPropertyFullName != string.Empty) { valueLine = objPropertyFullName + valueLine; }
            else { valueLine = obj.GetType().Name + valueLine; }

            // If value type
            if (IsValueType(objType))
            {
                valueLine += obj.ToString() + ",";
                lines.Add(valueLine);
                return lines;
            }

            // If ref class type
            foreach (var property in props)
            {
                var propertyObj = obj.GetType().GetProperty(property.Name).GetValue(obj);
                valueLine += GetCsvValue(propertyObj);
            }
            lines.Add(valueLine);

            // Add Sets and ref classes lines
            foreach (var property in props)
            {
                var propertyFullName = obj.GetType().Name + "." + property.Name;

                var propertyObj = obj.GetType().GetProperty(property.Name).GetValue(obj);

                if (propertyObj == null) { continue; }

                var propertyObjType = propertyObj.GetType();

                if (!IsValueType(propertyObjType))
                {
                    // Add sets
                    if (IsSetType(propertyObjType))
                    {
                        lines.AddRange(GetCsvSetLines(propertyObj, propertyFullName));
                    }

                    // Add ref classes
                    if (IsClassType(propertyObjType))
                    {
                        lines.AddRange(GetCsvObjectLines(propertyObj, propertyFullName, index));
                    }
                }
            }
            return lines;
        }

        private string GetCsvValue<T>(T obj)
        {
            if (obj is null) { return "null,"; }

            var objType = obj.GetType();

            var val = string.Empty;
            if (IsValueType(objType)) // Is ValueType
            {
                val += obj.ToString();
            }
            if (IsSetType(objType)) // Is collection Type
            {
                val += obj.GetType().Name + "[]";
            }
            if (IsClassType(objType)) // Is ref Class Type
            {
                val += "[" + obj.GetType().Name + "]";
            }

            val += ",";

            return val;
        }

        private IList<string> GetCsvSetLines(dynamic obj, string objPropertyFullName)
        {
            var lines = new List<string>();

            var counter = 0;
            foreach (var o in obj)
            {
                if (counter == 0)
                {
                    var header = GetCsvHeaderLine(o, objPropertyFullName);
                    if (header != null) { lines.Add(header); }
                }

                // Add values line
                var valueLine = GetCsvValueLines(o, objPropertyFullName, counter);
                lines.AddRange(valueLine);

                counter++;
            }
            return lines;
        }


        static bool IsValueType(Type objType)
        {
            return objType == typeof(string) || objType.IsValueType;
        }
        static bool IsSetType(Type objType)
        {
            return !IsValueType(objType) && objType.IsClass && !objType.IsTypeDefinition;
        }
        static bool IsClassType(Type objType)
        {
            return !IsValueType(objType) && objType.IsClass && objType.IsTypeDefinition;
        }



        //private List<string> GetTypeDefinition(SaveToCsvMock saveToCsvMock, string path)
        //{
        //    var lines = new List<string>();

        //    var header = "Type,Value,Is Value Type,Is Type Definition,Is Primitive,Is Class,";
        //    lines.Add(header);

        //    //lines.Add(GetCsvLineDefinition(saveToCsvMock.String_Test));
        //    //lines.Add(GetCsvLineDefinition(saveToCsvMock.MyEnumTest));
        //    //lines.Add(GetCsvLineDefinition(saveToCsvMock.Long_Test));
        //    //lines.Add(GetCsvLineDefinition(saveToCsvMock.DateTime_Test));
        //    //lines.Add(GetCsvLineDefinition(saveToCsvMock.Object_Test));
        //    //lines.Add(GetCsvLineDefinition(saveToCsvMock.Class_TestProperty));

        //    //saveToCsvMock.Object_Test = "object to string";
        //    //lines.Add(GetCsvLineDefinition(saveToCsvMock.Object_Test));

        //    //lines.Add(GetCsvLineDefinition(saveToCsvMock.StringArray_Test));
        //    //lines.Add(GetCsvLineDefinition(saveToCsvMock.ObjectArray_Test));
        //    //lines.Add(GetCsvLineDefinition(saveToCsvMock.ClassArray_Test));
        //    //lines.Add(GetCsvLineDefinition(saveToCsvMock.ClassList_Test));
        //    //lines.Add(GetCsvLineDefinition(saveToCsvMock.ClassCollection_Test));
        //    return lines;
        //}

        private string GetCsvLineDefinition<T>(T obj)
        {
            var objType = obj.GetType();

            var line = string.Empty;

            line += objType.ToString() + ",";
            line += obj.ToString() + ",";
            line += objType.IsValueType.ToString() + ",";
            line += objType.IsTypeDefinition.ToString() + ",";
            line += objType.IsPrimitive.ToString() + ",";
            line += objType.IsClass.ToString() + ",";

            return line;
        }
    }
}
