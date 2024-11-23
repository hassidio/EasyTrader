using EasyTrader.Core.Connectors.Http;
using EasyTrader.Core.Models.Entities;
using EasyTrader.Core.Models.Exceptions;
using EasyTrader.Tests.UnitTests;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EasyTrader.Tests.Models
{
    public class MockMarket : Market
    {
        #region IEntity
        public override void OnSetEntity(
            string? id = null,
            string? parentId = null,
            dynamic? clientData = null)
        {
            if (clientData is not null)
            {
                RawClientData = clientData;
            }

            ExchangeName = parentId;
        }

        #endregion

        #region IMarket
        public override IExchange Exchange
        {
            get { return GetDeserializedEntityParent<MockExchange>(); }
        }

        public override void OnAddCandle<TCandle>(TCandle candle)
        {
            if (RawClientData.GetType() == typeof(JArray))
            {
                ((JArray)RawClientData).Add(JObject.Parse(JsonConvert.SerializeObject(candle)));
            }
            else
            {
                RawClientData.Add(candle);
            }
        }

        public override IList<dynamic> GetRawClientDataSets(int recodesCount)
        {
            var setsClientCandle = GetClientCandlesSets<MockCandle>(recodesCount);

            if (setsClientCandle is null) { return null; }

            var setsClientRawData = new List<dynamic>();

            foreach (var list in setsClientCandle)
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

        #region IApiRequestor

        [JsonIgnore]
        public override Dictionary<string, string>? ApiRequestClientQuery
        {
            get { return UnitTestBase.ApiRequestQueryTest; }
        }

        [JsonIgnore]
        public override Dictionary<string, string>? ApiRequestClientHeaders
        {
            get { return UnitTestBase.ApiRequestHeadersTest; }
        }

        public override object? GetApiRequestClientContent(object? requestContent = null)
        {
            return (requestContent is not null ? requestContent.ToString() : string.Empty)
                + UnitTestBase.ApiRequestContentTest;
        }

        public override int GetResponseWeight(ApiHttpRequest apiHttpRequest, ApiHttpResponse apiHttpResponse)
        {
            return GetWeightFromHeader(apiHttpRequest, apiHttpResponse);//*/ test GetResponseWeight
        }

        public override Task<HttpException> GetApiHttpException(HttpResponseMessage response)
        {
            throw new NotImplementedException();
        }

        #endregion





    }
}