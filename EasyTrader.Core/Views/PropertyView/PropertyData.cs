using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace EasyTrader.Core.Views.PropertyView
{
    public class PropertyData : Property
    {
        public PropertyData(object obj, PropertyDataAttribute? propertyDataAttribute = null, string? name = null)
        {
            if (obj is null) { return; }
            PropertyType = obj.GetType();

            // PropertyData Attribute
            if (propertyDataAttribute is not null)
            {
                PropertyDataAttribute = propertyDataAttribute;
                PropertyName = PropertyDataAttribute.DisplayName;
            }
            else
            {
                Attribute? classAttribute = Attribute.GetCustomAttribute(PropertyType, typeof(PropertyDataAttribute));
                if (classAttribute is not null)
                {
                    PropertyDataAttribute = (PropertyDataAttribute)classAttribute;
                }
            }

            // PropertyData Name
            if (PropertyDataAttribute is null) { PropertyName = name; }
            else { PropertyName = PropertyDataAttribute.DisplayName; }

            ChildObjects = new List<PropertyData>();
            Properties = new List<Property>();

            if (IsJsonType(PropertyType))
            {
                PropertyType = typeof(string);
                SetValueProperty(obj.ToString());
                return;
            }

            if (IsValueType(PropertyType))
            {
                SetValueProperty(obj);
                return;
            }

            if (IsClassType(PropertyType))
            {
                SetClassProperty(obj);
                return;
            }

            if (IsSetType(PropertyType))
            {
                SetCollectionProperty(objList: obj, name: PropertyName);
                return;
            }
        }

        public PropertyDataAttribute? PropertyDataAttribute { get; private set; }
        public ICollection<Property> Properties { get; set; }
        public ICollection<PropertyData> ChildObjects { get; set; }

        private static Property GetNewProperty(object obj, PropertyDescriptor propertyDescriptor, PropertyDataAttribute propertyDataAttribute)
        {
            return new Property
            {
                PropertyValue = obj,
                PropertyName = propertyDescriptor.Name,
                PropertyDisplayName = propertyDataAttribute.DisplayName,
                PropertyType = propertyDescriptor.PropertyType,
            };
        }

        private void SetValueProperty(object obj)
        {
            var property = new Property()
            {
                PropertyValue = obj,
                PropertyName = PropertyName,
                PropertyDisplayName = PropertyDisplayName,
                PropertyType = PropertyType,
            };

            Properties.Add(property);
        }

        private void SetClassProperty(object obj)
        {
            var propertyDescriptorList =
                            TypeDescriptor.GetProperties(PropertyType).OfType<PropertyDescriptor>();

            foreach (PropertyDescriptor propertyDescriptor in propertyDescriptorList)
            {
                var childObj = obj.GetType().GetProperty(propertyDescriptor.Name).GetValue(obj);

                var childObjAttribute = propertyDescriptor.Attributes[typeof(PropertyDataAttribute)];

                if (childObjAttribute is not null)
                {
                    var propertyDataAttribute = (PropertyDataAttribute)childObjAttribute;

                    if (string.IsNullOrEmpty(propertyDataAttribute.DisplayName)) { propertyDataAttribute.DisplayName = propertyDescriptor.Name; }

                    var childObjProperty = GetNewProperty(childObj, propertyDescriptor, propertyDataAttribute);

                    if (childObj is null)
                    {
                        if (childObjProperty is not null)
                        {
                            Properties.Add(childObjProperty);
                        }
                    }
                    else
                    {
                        var objType = childObj.GetType();

                        // If value type
                        if (IsValueType(childObj.GetType()))
                        {
                            if (childObjProperty is not null) { Properties.Add(childObjProperty); }
                        }

                        // Is class
                        if (IsClassType(childObj.GetType()))
                        {
                            var properyData = new PropertyData(childObj, propertyDataAttribute);
                            ChildObjects.Add(properyData);
                        }

                        // Is collection Type
                        if (IsSetType(childObj.GetType()))
                        {
                            SetCollectionProperty(childObj, propertyDataAttribute);
                        }
                    }
                }
            }
        }

        private void SetCollectionProperty(dynamic objList, PropertyDataAttribute? attribute = null, string? name = null) 
        {
            if (string.IsNullOrEmpty(name)) { name = attribute.DisplayName; }
            var  pdListWrapper = new PropertyData(name, attribute, name);
            
            var count = 0;
            foreach (var item in objList)
            {
                var pdItem = new PropertyData(item, attribute, name );
                pdItem.PropertyDisplayName = $"{name}[{count}]";
                pdListWrapper.ChildObjects.Add(pdItem);
                count++;
            }

            pdListWrapper.Properties = new List<Property>();

            ChildObjects.Add(pdListWrapper);
        }

        public PropertyData Clone()
        {
            return new PropertyData(null)
            {
                PropertyName = PropertyName,
                PropertyDisplayName = PropertyDisplayName,
                PropertyValue = PropertyValue,
                PropertyType = PropertyType,
                ChildObjects = ChildObjects,
                Properties = Properties,
            };
        }
    }
}
