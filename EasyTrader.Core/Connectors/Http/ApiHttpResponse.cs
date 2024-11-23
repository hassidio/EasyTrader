

namespace EasyTrader.Core.Connectors.Http
{
    public class ApiHttpResponse
    {
        public ApiHttpResponse() { }

        public System.Net.Http.HttpResponseMessage HttpResponse { get; set; }

        public object Response { get; set; }

        public IDictionary<string, IEnumerable<string>> Headers
        {
            get 
            {
                if (HttpResponse is null) { return null; }
                return HttpResponse.Headers.ToDictionary(a => a.Key, a => a.Value); 
            }
        }

        public int ResponseCurrentWeightUsed { get; set; }
    }
}
