namespace EasyTrader.Core.Connectors
{
    public class ApiRequest : IApiRequest
    {
        public  IApiRequestor Requestor { get; set; }

        public ApiRequest(IApiRequestor requestor)
        {
            Requestor = requestor;
        }
    }
}