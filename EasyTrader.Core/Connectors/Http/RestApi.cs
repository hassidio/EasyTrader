using EasyTrader.Core.Common;
using EasyTrader.Core.Models.Exceptions;
using Newtonsoft.Json;
using System.Diagnostics;


namespace EasyTrader.Core.Connectors.Http
{
    public class RestApi : HttpApi
    {
        public RestApi() : base() { }

        public override ApiHttpResponse ApiHttpResponse { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiHttpRequest">of type ApiHttpRequest</param>
        /// <returns></returns>
        public override async Task<ApiHttpResponse> OnSendAsync(ApiHttpRequest apiHttpRequest)
        {
            using (var request = apiHttpRequest.HttpRequestMessage)
            {
                var apiHttpResponse = new ApiHttpResponse();

                apiHttpResponse.HttpResponse = HttpClient.SendAsync(request).Result; //<<<

                if (apiHttpResponse.HttpResponse.IsSuccessStatusCode)
                {
                    using (HttpContent responseContent = apiHttpResponse.HttpResponse.Content)
                    {
                        apiHttpResponse.Response = await responseContent.ReadAsStringAsync();

                        apiHttpResponse.ResponseCurrentWeightUsed =
                            apiHttpRequest.Requestor.GetResponseWeight(apiHttpRequest, apiHttpResponse);

                        //*/ remove
                        try
                        {
                            Debug.WriteLine($"");
                            Debug.WriteLine($"Response Headers:");

                            Debug.WriteLine($"x-mbx-used-weight: {apiHttpResponse.Headers["x-mbx-used-weight"].ToArray()[0]}");
                            Debug.WriteLine($"x-mbx-used-weight: {apiHttpResponse.Headers["x-mbx-used-weight-1m"].ToArray()[0]}");
                        }
                        catch { }
                        //foreach (KeyValuePair<string, IEnumerable<string>> h in apiHttpResponse.Headers)
                        //{
                        //    var txt = "";

                        //    foreach (var a in h.Value) { txt += $"{a},"; }
                        //    txt = $"{h.Key}: {txt}";
                        //    Debug.WriteLine(txt);
                        //}

                        Log.Debug("Response IsSuccessStatusCode!");

                        return apiHttpResponse;
                    }
                }
                else
                {
                    throw await apiHttpRequest.GetApiHttpException(apiHttpResponse.HttpResponse);
                }
            }
        }
    }
}