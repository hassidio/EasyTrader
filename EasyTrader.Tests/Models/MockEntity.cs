using EasyTrader.Core.Common;
using EasyTrader.Core.Connectors.Http;
using EasyTrader.Core.Models.Entities;
using EasyTrader.Core.Models.Exceptions;
using EasyTrader.Core.Views.PropertyView;
using EasyTrader.Tests.Models.MockClientApi;
using EasyTrader.Tests.UnitTests;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection.Metadata;

namespace EasyTrader.Tests.Models
{
    public class MockEntity : Entity
    {
        public MockEntity()
        {
            RawClientData = new MockClientObject { EntityName = "RawClientData" };
        }

        /// <summary>
        /// If true, the request will include the cilent Query, Content and Headers, otherwise they are null.
        /// It is used for calling test fixture end point like www.google.com.
        /// </summary>
        public bool IsMockApiRequest = true;

        #region IEntity

        public override string EntityId { get; set; }

        public override string? EntityParentId { get; set; }

        public override string? EntityStamp
        {
            get
            {
                if (RawClientData is null) { return null; }
                if (ClientDataArray is null) { return UnitTestBase.EntityStampTest; }

                var first = new List<dynamic>(ClientDataArray).First().ToString();
                var last = new List<dynamic>(ClientDataArray).Last().ToString();

                return $"{first}_{last}";
            }
        }
        public override bool IsRawClientDataTypeIEnumerable
        {
            get
            {
                if (RawClientData is null) { return false; }

                try { return JsonConvert.DeserializeObject<dynamic>(RawClientData) is not null; }
                catch { return Property.IsSetType(RawClientData.GetType()); }
            }
        }

        public override dynamic RawClientData { get; set; }

        public override void OnSetEntity(
            string? id = null,
            string? parentId = null,
            dynamic? rawClientData = null)
        {
            if (rawClientData is not null)
            { RawClientData = rawClientData; }
        }

        public override void SetEntity(
            string? id = null,
            string? parentId = null,
            dynamic? rawClientData = null)
        {
            if (!string.IsNullOrEmpty(id)) { EntityId = id; }
            if (!string.IsNullOrEmpty(parentId)) { EntityParentId = parentId; }

            OnSetEntity(id, parentId, rawClientData);
        }

        public override List<dynamic> GetRawClientDataSets(int recodesCount)
        {
            if (RawClientData is null) { return null; }

            var setsClient = new List<dynamic>();
            var listClient = new List<string>();

            var count = 0;

            var list = ClientDataArray.ToObject<List<string>>();

            foreach (var item in list)
            {
                listClient.Add(item);
                count++;

                if (count >= recodesCount)
                {
                    var setString = JsonConvert.SerializeObject(listClient.Order().ToList());
                    setsClient.Add(setString);
                    listClient = new List<string>();
                    count = 0;
                }
            }

            if (listClient.Count > 0)
            { setsClient.Add(JsonConvert.SerializeObject(listClient.Order().ToList())); }

            return setsClient.OrderBy(s => s).ToList();
        }

        #endregion

        #region IApiRequestor
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
            return IsMockApiRequest ?
                (requestContent is not null ? requestContent.ToString() : string.Empty)
                + UnitTestBase.ApiRequestContentTest : null;
        }

        public override Dictionary<string, string>? ApiRequestClientHeaders
        {
            get { return IsMockApiRequest ? UnitTestBase.ApiRequestHeadersTest : null; }
        }

        public override Dictionary<string, string>? ApiRequestClientQuery
        {
            get { return IsMockApiRequest ? UnitTestBase.ApiRequestQueryTest : null; }
        }

        public override int GetResponseWeight(ApiHttpRequest apiHttpRequest, ApiHttpResponse apiHttpResponse)
        {
            return GetWeightFromHeader(apiHttpRequest, apiHttpResponse);
        }

        #endregion

    }

    internal class MockClientObject
    {
        public string EntityName { get; set; }
    }
}
