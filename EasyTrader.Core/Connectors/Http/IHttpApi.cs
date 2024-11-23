namespace EasyTrader.Core.Connectors.Http
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">TParent for Exchange or Market objects used in Tasks</typeparam>
    public interface IHttpApi : IApi
    {
        HttpClient HttpClient { get; set; }
    }
}