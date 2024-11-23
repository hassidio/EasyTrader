using EasyTrader.Core.Connectors;
using EasyTrader.Core.Connectors.Http;
using EasyTrader.Tests.Models.MockClientApi;
using EasyTrader.Tests.UnitTests;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace EasyTrader.Tests.Models
{
    public class MockApi : IApi
    {
        public MockApi()
        {
            ApiHttpResponse = new ApiHttpResponse();

            MaximumTotalWeight = 100;
        }

        public int? OnSendSleepMilliseconds { get; set; }

        public int SendCount { get; set; }
        public DateTime LastSent { get; set; }
        public int MaximumTotalWeight { get; set; }
        public IDictionary<string, IEnumerable<string>> HttpResponseHeaders => throw new NotImplementedException();
        public string TimeStamp { get { return $"{DateTime.Now.ToString("mm:ss.fff")} Thread[{Thread.CurrentThread.ManagedThreadId}]"; } }
        public int? ExceptionCode { get; set; }


        #region IApi

        public ApiHttpResponse ApiHttpResponse { get; set; }

        public async Task<ApiHttpResponse> OnSendAsync(ApiHttpRequest mockApiRequest)
        {
            Debug.WriteLine($"{TimeStamp}: MockApi Sending request. waiting for response...");
            LastSent = DateTime.Now;
            SendCount++;

            // Exceptions
            if (ExceptionCode is not null)
            {
                ApiHttpResponse.HttpResponse = new HttpResponseMessage((HttpStatusCode)ExceptionCode);
                throw await mockApiRequest.GetApiHttpException(ApiHttpResponse.HttpResponse);
            }

            // If Exchange call
            if (mockApiRequest.Requestor.GetType() == typeof(MockExchange))
            {
                ApiHttpResponse.Response =
                    JsonConvert.SerializeObject(
                        new MockExchangeClientInfo(mockApiRequest));
            }

            // If Market call
            if (mockApiRequest.Requestor.GetType() == typeof(MockMarket))
            {
                ApiHttpResponse.Response =
                    JsonConvert.SerializeObject(
                        UnitTestBase.NewCandlesDefaultList());
            }

            // Throttle
            if (OnSendSleepMilliseconds is not null)
            {
                Thread.Sleep((int)OnSendSleepMilliseconds);
                Debug.WriteLine($"{TimeStamp}: MockApi Operation ended after {OnSendSleepMilliseconds} milliseconds");
            }

            if (ApiHttpResponse.HttpResponse is null)
            {
                ApiHttpResponse.HttpResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                ApiHttpResponse.HttpResponse.Headers.Add(
                    UnitTestBase.ResponseHeaderWeightKeyNameTest,
                    [$"{UnitTestBase.ResponseHeaderWeightValueTest}"]);
            }

            Debug.WriteLine($"{TimeStamp}: MockApi MaximumTotalWeight: {MaximumTotalWeight} ApiHttpResponse.ResponseCurrentWeightUsed: {ApiHttpResponse.ResponseCurrentWeightUsed}.");

            if (this.MaximumTotalWeight <= ApiHttpResponse.ResponseCurrentWeightUsed)
            { ApiHttpResponse.ResponseCurrentWeightUsed = 0; }

            ApiHttpResponse.ResponseCurrentWeightUsed +=
                mockApiRequest.Requestor.GetResponseWeight(mockApiRequest, ApiHttpResponse);

            Debug.WriteLine($"{TimeStamp}: MockApi Response received.");

            return ApiHttpResponse;
        }

        public Task<ApiHttpResponse> SendAsync(ApiHttpRequest mockApiRequest)
        {
            return OnSendAsync(mockApiRequest);
        }

        #endregion

    }
}