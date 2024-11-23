using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTrader.Core.Models.Binance
{

    // Do not change this Class

    /*
     * Response Headers:
            [x-mbx-used-weight]
            [x-mbx-used-weight-1m]
            [Strict-Transport-Security]
            [X-Frame-Options]
            [X-XSS-Protection]
            [X-Content-Type-Options]
            [Content-Security-Policy]
            [X-Content-Security-Policy]
            [X-WebKit-CSP]
            [Cache-Control]
            [Pragma]
            [Access-Control-Allow-Origin]
            [X-Cache]
            [Via]
            [X-Amz-Cf-Pop]
            [X-Amz-Cf-EntityNameId]
     */
    public class BinanceApi_ExchangeInfo
    {
        public string timezone { get; set; }
        public long serverTime { get; set; }
        public Ratelimit[] rateLimits { get; set; }
        public object[] exchangeFilters { get; set; }
        public Symbol[] symbols { get; set; }
    }

    public class Ratelimit
    {
        public string rateLimitType { get; set; }
        public string interval { get; set; }
        public int intervalNum { get; set; }
        public int limit { get; set; }
    }

    public class Symbol
    {
        public string symbol { get; set; }
        public string status { get; set; }
        public string baseAsset { get; set; }
        public int baseAssetPrecision { get; set; }
        public string quoteAsset { get; set; }
        public int quotePrecision { get; set; }
        public int quoteAssetPrecision { get; set; }
        public int baseCommissionPrecision { get; set; }
        public int quoteCommissionPrecision { get; set; }
        public string[] orderTypes { get; set; }
        public bool icebergAllowed { get; set; }
        public bool ocoAllowed { get; set; }
        public bool otoAllowed { get; set; }
        public bool quoteOrderQtyMarketAllowed { get; set; }
        public bool allowTrailingStop { get; set; }
        public bool cancelReplaceAllowed { get; set; }
        public bool isSpotTradingAllowed { get; set; }
        public bool isMarginTradingAllowed { get; set; }
        public Filter[] filters { get; set; }
        public object[] permissions { get; set; }
        public string[][] permissionSets { get; set; }
        public string defaultSelfTradePreventionMode { get; set; }
        public string[] allowedSelfTradePreventionModes { get; set; }
    }

    public class Filter
    {
        public string filterType { get; set; }
        public string minPrice { get; set; }
        public string maxPrice { get; set; }
        public string tickSize { get; set; }
        public string minQty { get; set; }
        public string maxQty { get; set; }
        public string stepSize { get; set; }
        public int limit { get; set; }
        public int minTrailingAboveDelta { get; set; }
        public int maxTrailingAboveDelta { get; set; }
        public int minTrailingBelowDelta { get; set; }
        public int maxTrailingBelowDelta { get; set; }
        public string bidMultiplierUp { get; set; }
        public string bidMultiplierDown { get; set; }
        public string askMultiplierUp { get; set; }
        public string askMultiplierDown { get; set; }
        public int avgPriceMins { get; set; }
        public string minNotional { get; set; }
        public bool applyMinToMarket { get; set; }
        public string maxNotional { get; set; }
        public bool applyMaxToMarket { get; set; }
        public int maxNumOrders { get; set; }
        public int maxNumAlgoOrders { get; set; }
        public string maxPosition { get; set; }
    }


}
