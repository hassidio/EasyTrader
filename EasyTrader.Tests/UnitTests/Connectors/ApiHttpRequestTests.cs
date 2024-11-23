using EasyTrader.Core.Common;
using EasyTrader.Core.Connectors.Http;
using EasyTrader.Core.Models.Exceptions;
using EasyTrader.Tests.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace EasyTrader.Tests.UnitTests.Connectors
{
    [TestClass]
    public class ApiHttpRequestTests : UnitTestBase
    {
        #region ApiHttpRequest

        [TestMethod]
        public void ApiHttpRequest_Test()
        {
            Log.Debug("Start ApiHttpRequest_Test");

            // Setup
            var apiHttpRequest = new ApiHttpRequest(new MockEntity(), @"/BaseUrlTest/", @"/PathUriTest/");

            // Test
            Assert.IsTrue(apiHttpRequest.BaseUrl == "BaseUrlTest");
            Assert.IsTrue(apiHttpRequest.PathUri == "PathUriTest");
            Assert.IsTrue(apiHttpRequest.HttpMethod == HttpMethod.Get);
            Assert.IsTrue(apiHttpRequest.Encoding == Encoding.UTF8);
            Assert.IsTrue(apiHttpRequest.MediaType == "application/json");
        }

        [TestMethod]
        public void ApiHttpRequest_RequestUrl_Test()
        {
            Log.Debug("Start ApiHttpRequest_RequestUrl_Test");

            // Setup
            var apiHttpRequest = NewMockApiHttpRequest(new MockEntity());

            // Test
            Assert.IsTrue(apiHttpRequest.RequestUrl == @"http://TestBaseUrl/TestPathUri?symbol=UnitTestBase+Market1&interval=10m");
        }

        [TestMethod]
        public void ApiHttpRequest_JoinRequestDictionary_Test()
        {
            // Setup
            ApiRequestOptions requestOptions = new ApiRequestOptions
            {
                ApiRequestQueryOptions = CustomRequestQueryOptions(),
                ApiRequestHeadersOptions = CustomRequestHeadersOptions(),
            };

            // Test
            var apiHttpRequest =
                new ApiHttpRequest(new MockEntity(), BaseUrlTest, PathUriTest, requestOptions);

            Assert.IsTrue(apiHttpRequest.Query.Count == 3);
            Assert.IsTrue(apiHttpRequest.Query["symbol"] == MarketIdTest);
            Assert.IsTrue(apiHttpRequest.Query["interval"] == "15m");
            Assert.IsTrue(apiHttpRequest.Query["limit"] == "100");
            Assert.IsTrue(apiHttpRequest.Headers.Count == 3);
            Assert.IsTrue(apiHttpRequest.Headers["Header1"] == "Header Test");
            Assert.IsTrue(apiHttpRequest.Headers["Header2"] == "Value2");
            Assert.IsTrue(apiHttpRequest.Headers["Header3"] == "Value3");
        }

        [TestMethod]
        public void ApiHttpRequest_RequestOptionsNull_Test()
        {
            // Setup
            ApiRequestOptions requestOptions = null;

            // Test
            var apiHttpRequest =
                new ApiHttpRequest(new MockEntity(), BaseUrlTest, PathUriTest, requestOptions);

            Assert.IsTrue(apiHttpRequest.Query.Count == 2);
            Assert.IsTrue(apiHttpRequest.Query["symbol"] == MarketIdTest);
            Assert.IsTrue(apiHttpRequest.Query["interval"] == "10m");

            Assert.IsTrue(apiHttpRequest.Headers.Count == 2);
            Assert.IsTrue(apiHttpRequest.Headers["Header1"] == "Header Test");
            Assert.IsTrue(apiHttpRequest.Headers["Header2"] == "apiKey");

            Assert.IsTrue(apiHttpRequest.Content == ApiRequestContentTest);
            Assert.IsTrue(apiHttpRequest.ResponseHeaderWeightKeyName == null);
        }

        [TestMethod]
        public void ApiHttpRequest_RequestOptionsNotNull_Test()
        {
            // Setup
            var content = "New request Content with ";
            ApiRequestOptions requestOptions = new ApiRequestOptions
            {
                ApiRequestQueryOptions = CustomRequestQueryOptions(),
                ApiRequestHeadersOptions = CustomRequestHeadersOptions(),
                ApiRequestContentOptions = content,
                ResponseHeaderWeightKeyName = ResponseHeaderWeightKeyNameTest,
            };

            // Test
            var apiHttpRequest =
                new ApiHttpRequest(new MockEntity(), BaseUrlTest, PathUriTest, requestOptions);

            Assert.IsTrue(apiHttpRequest.Query.Count == 3);
            Assert.IsTrue(apiHttpRequest.Query["symbol"] == MarketIdTest);
            Assert.IsTrue(apiHttpRequest.Query["interval"] == "15m");
            Assert.IsTrue(apiHttpRequest.Query["limit"] == "100");
            Assert.IsTrue(apiHttpRequest.Headers.Count == 3);
            Assert.IsTrue(apiHttpRequest.Headers["Header1"] == "Header Test");
            Assert.IsTrue(apiHttpRequest.Headers["Header2"] == "Value2");
            Assert.IsTrue(apiHttpRequest.Headers["Header3"] == "Value3");

            Assert.IsTrue(apiHttpRequest.Content.ToString() == content + ApiRequestContentTest);
            Assert.IsTrue(apiHttpRequest.ResponseHeaderWeightKeyName == ResponseHeaderWeightKeyNameTest);
        }

        [TestMethod]
        public void ApiHttpRequest_HttpRequestMessage_Test()
        {
            Log.Debug("Start ApiHttpRequest_HttpRequestMessage_Test");

            // Setup
            var apiHttpRequest = NewMockApiHttpRequest(new MockEntity());

            // Test
            var request = apiHttpRequest.HttpRequestMessage;
            var Header1 = new List<string>(request.Headers.GetValues("Header1"));
            var Header2 = new List<string>(request.Headers.GetValues("Header2"));

            Assert.IsTrue(request.RequestUri.AbsoluteUri == @"http://testbaseurl/TestPathUri?symbol=UnitTestBase+Market1&interval=10m");
            Assert.IsTrue(request.Method == HttpMethod.Get);
            Assert.IsTrue(Header1[0] == "Header Test");
            Assert.IsTrue(Header2[0] == "apiKey");
            Assert.IsTrue(request.Content.Headers.ContentType.MediaType == apiHttpRequest.MediaType);
            Assert.IsTrue(request.Content.Headers.ContentType.CharSet == Encoding.UTF8.WebName);
            Assert.IsTrue(request.Content is not null);

            var contentResult = JsonConvert.DeserializeObject<object>(request.Content.ReadAsStringAsync().Result);
            Assert.IsTrue(contentResult.GetType() == typeof(string));

            Assert.IsTrue(contentResult.ToString() == ApiRequestContentTest.ToString());
        }

        [TestMethod]
        public void ApiHttpRequest_GetApiHttpException_Test()
        {
            // Setup
            var apiHttpRequest =
                new ApiHttpRequest(new MockEntity(), BaseUrlTest, PathUriTest);

            HttpResponseMessage httpResponse = new HttpResponseMessage(HttpStatusCode.BadRequest);

            // Test
            Task<HttpException> task = apiHttpRequest.GetApiHttpException(httpResponse);
            task.Wait();

            HttpException httpException = task.Result;
            ClientException clientException = (ClientException)task.Result;

            Assert.IsTrue(httpException.StatusCode == (int)HttpStatusCode.BadRequest);
            Assert.IsTrue(clientException.Code == (int)HttpStatusCode.BadRequest);
        }


        // Helpers:

        private static Dictionary<string, string> CustomRequestQueryOptions()
        {
            // Setup
            var clientRequestQuery = new Dictionary<string, string>
            {
                //{ "symbol", MarketIdTest  },  //removed
                { "interval", "15m" },          // was "10m"
                { "limit", "100" }              // new
            };
            return clientRequestQuery;
        }

        private static Dictionary<string, string> CustomRequestHeadersOptions()
        {
            var lientRequestHeaders = new Dictionary<string, string>()
            {
                //{ "Header1", "Header Test"  },    //removed
                { "Header2", "Value2" },            // was "apiKey"
                { "Header3", "Value3" }             // new
            };
            return lientRequestHeaders;
        }


        #endregion
    }
}