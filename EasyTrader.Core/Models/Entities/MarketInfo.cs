using EasyTrader.Core.Views.PropertyView;


namespace EasyTrader.Core.Models.Entities
{
    public class MarketInfo : IDisposable
    {
        [PropertyData("Id")]
        public string Id { get; set; }

        [PropertyData("Market Name")]
        public string MarketName { get; set; }

        [PropertyData("Market Currency")]
        public string MarketCurrency { get; set; }

        [PropertyData("Currency")]
        public string BaseCurrency { get; set; } //The second asset in the symbol (e.g. USDT is the quote asset of symbol BTCUSDT) which represents the asset being used to quote prices (the price).

        //public string MarketCurrencyLong { get; set; }
        //public string BaseCurrencyLong { get; set; }
        //public double MinTradeSize { get; set; }
        //public bool IsActive { get; set; }
        //public bool IsRestricted { get; set; }
        //public DateTime Created { get; set; }
        //public string Notice { get; set; }
        //public string IsSponsored { get; set; }
        //public string LogoUrl { get; set; }
        //public DateTime LastUpdate { get; set; }
        //public bool IsObsolete { get; set; }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
