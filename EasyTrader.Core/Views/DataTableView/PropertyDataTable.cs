using EasyTrader.Core.Views.PropertyView;
using System.ComponentModel;

namespace EasyTrader.Core.Views.DataTableView
{
    public class PropertyDataTable : BaseDataTable
    {
        private const string PropertyName = "PropertyName";
        private const string PropertyValue = "PropertyValue";
        private const string PropertyType = "PropertyType";
        public PropertyDataTable(object obj, string? name = null) : base(obj, name) { }

        protected override void SetHeaders(dynamic obj)
        {
            // Add Header columns
            Columns.Add(PropertyName);
            Columns.Add(PropertyValue);
            Columns.Add(PropertyType);
        }

        protected override void OnSetRows(dynamic obj)
        {
            foreach (PropertyDescriptor p in Properties)
            {
                var childObj = obj.GetType().GetProperty(p.Name).GetValue(obj);

                // If value type
                if (Property.IsValueType(childObj.GetType()))
                {
                    var row = NewRow();

                    row[PropertyName] = p.Name;
                    row[PropertyValue] = childObj; //enum to string???
                    row[PropertyType] = childObj.GetType(); //enum to string???

                    Rows.Add(row);
                }
            }

            foreach (PropertyDescriptor p in Properties)
            {
                var childObj = obj.GetType().GetProperty(p.Name).GetValue(obj);

                // Is class
                if (Property.IsClassType(childObj.GetType()))
                {
                    var cdt = new PropertyDataTable(childObj, GetPropertyDisplayName(p));

                    ChildTables.Add(cdt);
                }

                // Is collection Type
                if (Property.IsSetType(childObj.GetType()))
                {
                    throw new NotImplementedException();
                }

            }

        }

    }

}
