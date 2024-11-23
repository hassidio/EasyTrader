using EasyTrader.Core.Views.PropertyView;
using Newtonsoft.Json;
using System.ComponentModel;

namespace EasyTrader.Core.Models.Entities
{
    public abstract class Exchange : Entity, IExchange
    {
        public Exchange()
        {
            MarketsInfoList = new HashSet<MarketInfo>();
        }

        [PropertyData("Entity Id")]
        public override string EntityId { get; set; }

        [PropertyData("Entity Parent Id")]
        public override string? EntityParentId { get; set; }

        [PropertyData("Entity Stamp")]
        [JsonIgnore]
        public override string? EntityStamp { get { return Created.Ticks.ToString(); } }

        public string Timezone { get; set; }
        public long ServerTime { get; set; }
        public DateTime Created { get; set; }
        public virtual ICollection<MarketInfo> MarketsInfoList { get; set; }

        public override dynamic RawClientData { get; set; }

        [JsonIgnore]
        public override bool IsRawClientDataTypeIEnumerable { get { return false; } }


        [PropertyData("Market Names")]
        [JsonIgnore]
        public virtual ICollection<string> MarketNames
        {
            get
            {
                var names = new HashSet<string>();
                MarketsInfoList.ToList().ForEach(p => names.Add(p.MarketName));
                return names.Order().ToHashSet().ToList();
            }
        }

       

        public MarketInfo? GetMarketInfoById(string id)
        {
            return MarketsInfoList.Where(m => m.Id == id).FirstOrDefault();
        }

        public override void SetEntity(
            string? id = null,
            string? parentId = null,
            dynamic? rawClientData = null)
        {
            if (id is not null) { EntityId = id; }

            OnSetEntity(id, parentId, rawClientData);
        }
    }
}
