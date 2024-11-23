using EasyTrader.Core.Common;
using EasyTrader.Core.Models.Exceptions;
using Newtonsoft.Json;
using System.Text;

namespace EasyTrader.Core.Connectors.Http
{
    public class ApiHttpRequest : ApiRequest
    {
        private readonly string _mediaType = "application/json";

        public ApiHttpRequest(
           IApiRequestor requestor,
           string baseUrl,
           string? pathUri = null,
           ApiRequestOptions? requestOptions = null) : base(requestor)
        {

            BaseUrl = TrimUrlString(baseUrl);
            PathUri = TrimUrlString(pathUri);

            if (requestOptions is not null)
            {
                // Query
                Query = JoinRequestDictionary(
                    requestOptions.ApiRequestQueryOptions,
                    requestor.ApiRequestClientQuery);

                // Headers
                Headers = JoinRequestDictionary(
                    requestOptions.ApiRequestHeadersOptions,
                    requestor.ApiRequestClientHeaders);

                // Content
                var clientContent =
                    requestor.GetApiRequestClientContent(requestOptions.ApiRequestContentOptions);

                if (clientContent is not null) { Content = clientContent; }
                else { Content = requestOptions.ApiRequestContentOptions; }

                // The Header Weight Key EntityId coming back on the Http Response
                ResponseHeaderWeightKeyName = requestOptions.ResponseHeaderWeightKeyName;
            }
            else
            {
                Query = requestor.ApiRequestClientQuery;
                Headers = requestor.ApiRequestClientHeaders;
                Content = requestor.GetApiRequestClientContent();
            }

            HttpMethod = HttpMethod.Get;
            Encoding = Encoding.UTF8;
            MediaType = _mediaType;
        }

        /// <summary>
        /// Update requestOptions items with clientRequestOptions items, if client item does not exist it will add to requestOptions.
        /// The requestOptions dictionary overwrite the values in the requestor request options.
        /// </summary>
        /// <param name="requestOptions">The dictionary to update.</param>
        /// <param name="clientRequestOptions">The dictionary to update with.</param>
        /// <returns>Returns the joined requestOptions</returns>
        private Dictionary<string, string> JoinRequestDictionary(
            Dictionary<string, string> requestOptions,
            Dictionary<string, string> clientRequestOptions)
        {
            if (requestOptions is not null)
            {
                if (clientRequestOptions is not null)
                {
                    foreach (var clientItem in clientRequestOptions)
                    {
                        string result;
                        if (!requestOptions.TryGetValue(clientItem.Key, out result))
                        {
                            requestOptions.Add(clientItem.Key, clientItem.Value);
                        }
                    }
                }
            }
            return requestOptions;
        }

        public async Task<HttpException> GetApiHttpException(HttpResponseMessage httpResponse)
        {
            return await Requestor.GetApiHttpException(httpResponse);
        }

        public string RequestUrl { get { return GetRequestUrl(); } }
        public HttpRequestMessage HttpRequestMessage { get { return GetHttpRequestMessage(); } }

        public string BaseUrl { get; private set; }
        public string? PathUri { get; set; }
        public Dictionary<string, string>? Query { get; set; }
        public Dictionary<string, string>? Headers { get; set; }
        public object? Content { get; set; }
        public HttpMethod HttpMethod { get; set; }
        public Encoding? Encoding { get; set; }
        public string MediaType { get; set; }
        public string ResponseHeaderWeightKeyName { get; set; }

        private HttpRequestMessage GetHttpRequestMessage()
        {
            var httpRequest = new HttpRequestMessage();

            Log.Debug("Request Url: " + RequestUrl);

            httpRequest = new HttpRequestMessage(HttpMethod, RequestUrl);

            // Headers
            if (Headers is not null)
            {
                foreach (var h in Headers.Where(h => h.Value is not null))
                {
                    httpRequest.Headers.Add(h.Key, h.Value);
                }
            }

            // Content
            if (Content is not null)
            {
                Log.Debug("Request Content: " + Content.ToString());
                httpRequest.Content = new StringContent(
                    JsonConvert.SerializeObject(Content), Encoding, MediaType);
            }

            return httpRequest;
        }

        private string GetRequestUrl()
        {
            var requestUri = string.Empty;

            if (PathUri is not null) { requestUri = PathUri; }

            if (Query is not null)
            {
                var queryString = Helpers.BuildQueryString(Query);

                if (!string.IsNullOrEmpty(queryString))
                { requestUri += "?" + queryString; }
            }

            return $"{BaseUrl}/{requestUri}";
        }

        private string TrimUrlString(string? txt)
        {
            if (txt is null) { return string.Empty; }

            txt = txt.TrimStart('/', '\\');
            txt = txt.TrimEnd('/', '\\');

            return txt;
        }
    }
}
