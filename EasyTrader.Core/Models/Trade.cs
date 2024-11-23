//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.ViewTasks;

//namespace EasyTrader.Core.Models
//{
//    //Binance:
//    // Old trade lookup:    /api/v3/historicalTrades
//    //Recent trades list:     /api/v3/trades

//    public class Trade
//    {
//        public string EntityNameId { get; set; }
//        public string MarketId { get; set; }
//        public int ClientId { get; set; }
//        public DateTime TimeStamp { get; set; }
//        public double Quantity { get; set; }
//        public double Price { get; set; }
//        public double Total { get; set; }

//        public IDictionary<string, object> PropertyBag { get; set; }

//        public Trade()
//        {
//            PropertyBag = new Dictionary<string, object>
//            {
//                { TradePropertyBagKey.FillType.ToString(), null },
//                { TradePropertyBagKey.OrderType.ToString(), null }
//            };
//        }
//    }

//    public enum TradePropertyBagKey
//    {
//        FillType,
//        OrderType
//    }
//}
