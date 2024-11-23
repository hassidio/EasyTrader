using EasyTrader.Core.Common;
using EasyTrader.Core.Connectors.Http;
using EasyTrader.Core.Models.Exceptions;
using Newtonsoft.Json;

namespace EasyTrader.Core.Connectors
{


    //*/
    //public class Ratelimit
    //{
    //    public string rateLimitType { get; set; }
    //    public string interval { get; set; }
    //    public int intervalNum { get; set; }
    //    public int limit { get; set; }
    //}


    public class ApiController<TApi>
        where TApi : IApi, new()
    {
        public ApiController(Throttle throttle)
        {
            Api = new TApi();

            Throttle = throttle;
        }

        public Throttle Throttle { get; set; }

        public TApi Api { get; private set; }


        public ApiHttpRequest ApiHttpRequest { get; private set; }

        public ApiHttpResponse ApiHttpResponse { get; private set; }

        public event ErrorEventHandler Error;

        public T Send<T>(ApiHttpRequest request)
        {
            ApiHttpRequest = request;

            if(SendRequest())
            {
                return MapResponse<T>();
            }

            return default;
        }

        private bool SendRequest()
        {
            try
            {
                Throttle.Start();

                ApiHttpResponse = Api.SendAsync(ApiHttpRequest).Result; //<<<

                Throttle.SetCurrentWeightUsed(ApiHttpResponse.ResponseCurrentWeightUsed);

                return true;
            }
            catch (Exception ex)
            {
                OnError(new System.IO.ErrorEventArgs(ex));
                return false;
            }
        }

        private T MapResponse<T>()
        {
            try
            {
                if (typeof(T) == typeof(string))
                {
                    return (T)ApiHttpResponse.Response;
                }
                else
                {
                    return JsonConvert.DeserializeObject<T>(ApiHttpResponse.Response.ToString());
                }
            }
            catch (JsonReaderException ex) //*/  move to Error event
            {
                var clientException = new ClientException(
                    SeverityEnum.Error,
                    $"Failed to map server response from '${ApiHttpRequest.RequestUrl}' to given type: {typeof(T)}",
                    -1,
                    ex);

                clientException.StatusCode = (int)ApiHttpResponse.HttpResponse.StatusCode;
                clientException.Headers = ApiHttpResponse.Headers;

                OnError(new System.IO.ErrorEventArgs(clientException));
            }
            catch (Exception ex)
            {
                OnError(new System.IO.ErrorEventArgs(ex));
            }

            return default;
        }

        void OnError(System.IO.ErrorEventArgs e)
        {
            ErrorEventHandler handler = Error;
            if (handler != null) { handler(this, e); }
        }
    }

    public class ApiEventArgs : EventArgs
    {
        public ApiEventArgs() { }

        public ApiEventArgs(
            IApi? api, 
            Throttle? throttle, 
            ApiHttpRequest? request, 
            ApiHttpResponse? response)
        {
            Api = api;
            Throttle = throttle;
            Request = request;
            Response = response;
        }

        public IApi? Api { get; set; }
        public Throttle? Throttle { get; set; }
        public ApiHttpRequest? Request { get; set; }
        public ApiHttpResponse? Response { get; set; }
    }

}
