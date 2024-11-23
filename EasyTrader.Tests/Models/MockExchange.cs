using EasyTrader.Core.Common;
using EasyTrader.Core.Connectors.Http;
using EasyTrader.Core.Models.Entities;
using EasyTrader.Core.Models.Exceptions;
using EasyTrader.Tests.Models.MockClientApi;
using EasyTrader.Tests.UnitTests;
using Newtonsoft.Json;

namespace EasyTrader.Tests.Models
{
    public class MockExchange : Exchange
    {
        #region IEntity
        public override void OnSetEntity(
            string? id = null,
            string? parentId = null,
            dynamic? rawClientData = null)
        {
            if (rawClientData is not null)
            {
                this.RawClientData = (MockExchangeClientInfo)rawClientData;
                var exchangeInfo = RawClientData.MockApiResponse.ToString();

                this.Created = DateTime.Now;
                var marketInfo = new MarketInfo
                {
                    Id = UnitTestBase.MarketIdTest,
                    MarketName = UnitTestBase.MarketIdTest,
                    BaseCurrency = exchangeInfo,
                    MarketCurrency = exchangeInfo
                };

                MarketsInfoList.Add(marketInfo);
            }
        }

        public override IList<dynamic> GetRawClientDataSets(int recodesCount)
        {
            return null;
        }

        #endregion

        #region IApiRequestor

        [JsonIgnore]
        public override Dictionary<string, string>? ApiRequestClientHeaders { get; }

        [JsonIgnore]
        public override Dictionary<string, string>? ApiRequestClientQuery { get; }

        public async override Task<HttpException> GetApiHttpException(HttpResponseMessage response)
        {
            using (HttpContent responseContent = response.Content)
            {
                HttpException httpException = CommonException.UnsuccessfulResponse((int)response.StatusCode);
                httpException.HttpExceptionResponseContent = await responseContent.ReadAsStringAsync();
                httpException.Headers = response.Headers.ToDictionary(a => a.Key, a => a.Value);
                return httpException;
            }
        }

        public override object? GetApiRequestClientContent(object? requestContent = null)
        {
            return null;
        }

        private int _currentWeight = 0;

        public override int GetResponseWeight(ApiHttpRequest apiHttpRequest, ApiHttpResponse apiHttpResponse)
        {
            int weight = GetWeightFromHeader(apiHttpRequest, apiHttpResponse);
            _currentWeight += weight;
            return _currentWeight;
        }

        #endregion



    }
}
