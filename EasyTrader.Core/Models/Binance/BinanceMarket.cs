using EasyTrader.Core.Connectors.Http;
using EasyTrader.Core.Models.Entities;
using EasyTrader.Core.Models.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EasyTrader.Core.Models.Binance
{
    public class BinanceMarket : Market
    {
        public BinanceMarket() : base() { }


        #region IEntity

        public override void OnSetEntity(
            string? id = null,
            string? parentId = null,
            dynamic? clientCandlesData = null)
        {
            if (clientCandlesData is not null)
            {
                RawClientData = (JArray)clientCandlesData;

                RefreshCandles<BinanceCandle>();
            }

            ExchangeName = parentId;
        }


        #endregion

        #region IApiRequestor

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


        #region IMarket

        public override IExchange Exchange
        {
            get { return GetDeserializedEntityParent<BinanceExchange>(); }
        }

        public override void OnAddCandle<TCandle>(TCandle candle)
        {
            RawClientData.Add((JToken)candle.ClientCandleData);
        }

        public override IList<dynamic> GetRawClientDataSets(int recodesCount)
        {
            var setsBinanceCandle = GetClientCandlesSets<BinanceCandle>(recodesCount);

            if (setsBinanceCandle is null) { return null; }

            var setsClientRawData = new List<dynamic>();

            foreach (var list in setsBinanceCandle)
            {
                dynamic rawClientdata = new JArray();

                foreach (var candle in list)
                {
                    rawClientdata.Add((JToken)candle.ClientCandleData);
                }

                setsClientRawData.Add(rawClientdata);
            }
            return setsClientRawData;

        }
        #endregion



    }
}
