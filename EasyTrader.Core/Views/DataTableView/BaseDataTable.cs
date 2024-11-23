using Newtonsoft.Json;
using System.ComponentModel;
using System.Data;

namespace EasyTrader.Core.Views.DataTableView
{
    public abstract class BaseDataTable : DataTable
    {
        public BaseDataTable(dynamic obj, string? name = null) : base()
        {
            if (obj is null) { return; }
            if (name is not null) { TableName = name; }

            ObjectType = obj.GetType();
            Properties = TypeDescriptor.GetProperties(ObjectType).OfType<PropertyDescriptor>();

            ChildTables = new List<BaseDataTable>();

            ChildTables.Add(GetTable(obj));
        }

        protected Type ObjectType { get; set; }

        protected IEnumerable<PropertyDescriptor> Properties { get; set; }

        protected string SerializedObject { get { return JsonConvert.SerializeObject(this); } }

        protected ICollection<BaseDataTable> ChildTables { get; set; }

        private BaseDataTable? GetTable(dynamic obj)
        {
            SetHeaders(obj);
            SetRows(obj);
            return this;
        }

        protected abstract void SetHeaders(dynamic obj);

        protected void SetRows(dynamic obj)
        {
            OnSetRows(obj);
        }

        protected abstract void OnSetRows(dynamic obj);

        public static string GetPropertyDisplayName(PropertyDescriptor h)  // DisplayName???
        {
            return h.Name;
        }


    }

}
