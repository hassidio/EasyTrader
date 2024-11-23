
namespace EasyTrader.Core.Connectors.Http
{
    public abstract class HttpApi : IHttpApi
    {
        public HttpApi()
        {
            HttpClient = new HttpClient();
        }

        public HttpClient HttpClient { get; set; }

        public abstract ApiHttpResponse ApiHttpResponse { get; set; }

        public Task<ApiHttpResponse> SendAsync(ApiHttpRequest apiHttpRequest)
        {
            var response = OnSendAsync(apiHttpRequest);
            return response;
        }

        public abstract Task<ApiHttpResponse> OnSendAsync(ApiHttpRequest apiHttpRequest);

    }
}