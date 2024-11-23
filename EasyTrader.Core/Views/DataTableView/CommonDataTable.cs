using EasyTrader.Core.Views.PropertyView;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Data;

namespace EasyTrader.Core.Views.DataTableView
{
    public class CommonDataTable : BaseDataTable
    {
        public CommonDataTable(object obj, string? name = null) : base(obj, name) { }

        protected override void SetHeaders(dynamic obj)
        {
            // Add Header columns
            foreach (PropertyDescriptor p in Properties)
            {
                if (Property.IsValueType(p.PropertyType)) // ????
                {
                    Columns.Add(GetPropertyDisplayName(p), p.PropertyType);
                }
            }
        }

        protected override void OnSetRows(dynamic obj)
        {
            var row = NewRow();

            foreach (PropertyDescriptor p in Properties)
            {
                var childObj = obj.GetType().GetProperty(p.Name).GetValue(obj);

                // If value type
                if (Property.IsValueType(childObj.GetType()))
                {
                    row[GetPropertyDisplayName(p)] = childObj; //enum to string???
                }
            }
            Rows.Add(row);


            foreach (PropertyDescriptor p in Properties)
            {
                var childObj = obj.GetType().GetProperty(p.Name).GetValue(obj);

                // Is class
                if (Property.IsClassType(childObj.GetType()))
                {
                    var cdt = new CommonDataTable(childObj, GetPropertyDisplayName(p));

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
