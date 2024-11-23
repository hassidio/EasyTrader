using System.Reflection;

namespace EasyTrader.Core.Common.JsonEntityVersions
{
    public class EntityProertiesConvertor : IEntityProertiesConvertor //, IObjectVersionTransform
    {
        public TDerived CopyPropertiesTo<TDerived>() where TDerived : IEntityProertiesConvertor
        {
            var thisType = GetType();
            var derivedType = typeof(TDerived);
            var derivedInstance = Activator.CreateInstance(derivedType);

            PropertyInfo[] thisProperties = thisType.GetProperties();
            PropertyInfo[] derivedProperties = derivedType.GetProperties();

            foreach (var derivedProperty in derivedProperties)
            {
                foreach (var thisProperty in thisProperties)
                {
                    if (thisProperty.Name == derivedProperty.Name)
                    {
                        // set base object derivedProperties
                        try
                        {
                            var _value = thisProperty.GetValue(this, null);
                            derivedProperty.SetValue(derivedInstance, _value, null);
                        }
                        catch (TargetException) {/* derivedProperties that don't exist in base object */}
                        catch (Exception) { throw; }

                    }
                }
            }

            return (TDerived)derivedInstance;
        }
    }
}
