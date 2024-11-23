using System.Collections;


namespace EasyTrader.Core.Views.PropertyView
{
    public class Property
    {
        public string PropertyName { get; set; }
        public string PropertyDisplayName { get; set; }
        public string PropertyNameView { get { return PropertyDisplayName ?? PropertyName; } }

        public dynamic PropertyValue { get; set; }
        public Type PropertyType { get; set; }

        public bool IsValue { get { return IsValueType(PropertyType); } }
        public bool IsClass { get { return IsClassType(PropertyType); } }
        public bool IsSet { get { return IsSetType(PropertyType); } }

        public static bool IsJsonType(Type objType)
        {
            return objType.AssemblyQualifiedName.StartsWith("Newtonsoft.Json");
        }

        public static bool IsValueType(Type objType)
        {
            return 
                objType == typeof(string) || 
                objType.IsValueType;
        }

        public static bool IsSetType(Type objType)
        {
            return
                typeof(IEnumerable).IsAssignableFrom(objType) && 
                !IsValueType(objType);
        }

        public static bool IsClassType(Type objType)
        {
            return 
                !IsValueType(objType) && 
                !IsSetType(objType);
        }
    }
}
