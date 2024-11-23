using EasyTrader.Core.PersistenceServices;
using EasyTrader.Core.Views.DataTableView;
using Newtonsoft.Json.Linq;

namespace EasyTrader.Core.Models.Entities
{
    public interface IEntity
    {
        string EntityId { get; set; }

        string? EntityParentId { get; set; }

        string? SerializedEntityParent { get; set; }

        string? EntityStamp { get; }


        dynamic RawClientData { get; set; }
        bool IsRawClientDataTypeIEnumerable { get; }

        IList<dynamic> ClientDataList { get; }
        JArray ClientDataArray { get; }

        //CommonDataTable ClientCommonDataTable { get; }
        //PropertyDataTable ClientPropertyDataTable { get; }

        T GetDeserializedEntityParent<T>();

        IList<dynamic> GetRawClientDataSets(int recodesCount);


        void SetEntity(
            string? id = null,
            string? parentId = null,
            dynamic? rawClientData = null);

        void OnSetEntity(
            string? id = null,
            string? parentId = null,
            dynamic? rawClientData = null);
    }
}
