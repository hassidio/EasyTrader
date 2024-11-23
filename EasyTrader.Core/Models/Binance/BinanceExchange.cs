using EasyTrader.Core.Connectors.Http;
using EasyTrader.Core.Models.Entities;
using EasyTrader.Core.Models.Exceptions;
using Newtonsoft.Json;
using System.Diagnostics;

namespace EasyTrader.Core.Models.Binance
{
    public class BinanceExchange : Exchange
    {
        public BinanceExchange() { }

        #region IEntity
        public override void OnSetEntity(
            string? id = null,
            string? parentId = null,
            dynamic? rawClientData = null)
        {
            if (rawClientData is not null)
            {
                var exchangeInfo = (BinanceApi_ExchangeInfo)rawClientData;

                RawClientData = exchangeInfo;

                Timezone = exchangeInfo.timezone;
                ServerTime = exchangeInfo.serverTime;
                Created = DateTime.UnixEpoch.AddMilliseconds(exchangeInfo.serverTime);


                //*/ todo: CallsPerSecondPolicy and rateLimit
                Ratelimit? rateLimit =
                    exchangeInfo.rateLimits.Where(rl => rl.rateLimitType == "SECOND").FirstOrDefault();

                //*/ remove
                //Debug.WriteLine($"Rate Limits:");
                //foreach (var rl in exchangeInfo.rateLimits)
                //{
                //    Debug.WriteLine($"");
                //    Debug.WriteLine($"rateLimitType: {rl.rateLimitType}");
                //    Debug.WriteLine($"intervalNum: {rl.intervalNum}");
                //    Debug.WriteLine($"interval: {rl.interval}");
                //    Debug.WriteLine($"limit: {rl.limit}");

                //}
                //if (rateLimit is not null)
                //{
                //    AllowedCallsPerSecondPolicy = rateLimit.intervalNum;
                //    rateLimit.
                //}

                // Markets Info
                foreach (var s in exchangeInfo.symbols)
                {
                    var marketInfo = new MarketInfo
                    {
                        Id = s.symbol,
                        MarketName = s.symbol,
                        MarketCurrency = s.baseAsset,
                        BaseCurrency = s.quoteAsset
                    };

                    MarketsInfoList.Add(marketInfo);
                }
            }
        }

        public override IList<dynamic> GetRawClientDataSets(int recodesCount)
        {
            return null;
        }

        #endregion

        #region IApiRequester

        [JsonIgnore]
        public override Dictionary<string, string>? ApiRequestClientQuery { get; }

        [JsonIgnore]
        public override Dictionary<string, string>? ApiRequestClientHeaders { get; }

        public override object? GetApiRequestClientContent(object? requestContent = null)
        {
            return null;
        }

        public override int GetResponseWeight(ApiHttpRequest apiHttpRequest, ApiHttpResponse apiHttpResponse)
        {
            return GetWeightFromHeader(apiHttpRequest, apiHttpResponse);
        }

        public async override Task<HttpException> GetApiHttpException(HttpResponseMessage response)
        {
            return await BinanceException.GetBinanceHttpException(response);
        }




        #endregion


    }
}
/*
 Rate Limits:

rateLimitType: REQUEST_WEIGHT
intervalNum: 1
interval: MINUTE
limit: 6000

rateLimitType: ORDERS
intervalNum: 10
interval: SECOND
limit: 100

rateLimitType: ORDERS
intervalNum: 1
interval: DAY
limit: 200000

rateLimitType: RAW_REQUESTS
intervalNum: 5
interval: MINUTE
limit: 61000

 */