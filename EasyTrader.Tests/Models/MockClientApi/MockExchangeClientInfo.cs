using EasyTrader.Core.Connectors.Http;
using EasyTrader.Core.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTrader.Tests.Models.MockClientApi
{
    public class MockExchangeClientInfo
    {
        public MockExchangeClientInfo(ApiHttpRequest mockApiRequest)
        {
            MockApiRequest = mockApiRequest;

            MockApiResponse = new ApiHttpResponse();
            MockApiResponse.HttpResponse = new HttpResponseMessage();
            MockApiResponse.Response = "Exchange MockApiController response Info";
        }


        public ApiHttpResponse MockApiResponse { get; set; }

        [JsonIgnore]
        public ApiHttpRequest MockApiRequest { get; set; }

    }
}
