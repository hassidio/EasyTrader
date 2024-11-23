namespace EasyTrader.Core.Connectors
{
    public interface IApiRequest
    {
        IApiRequestor Requestor { get; }
    }
}