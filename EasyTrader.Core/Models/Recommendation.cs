using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyTrader.Core.Models.Entities;

namespace EasyTrader.Core.Models
{
    public class Recommendation
    {
        public string Id { get; set; }
        public string LeadMarketId { get; set; }
        public string TrailingMarketId { get; set; }
        public double Score { get; set; }
        public DateTime TimeStamp { get; set; }
        public virtual Market LeadingMarket { get; set; }
        public virtual Market TrailingMarket { get; set; }
    }
}
