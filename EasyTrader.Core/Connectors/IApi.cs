using EasyTrader.Core.Connectors.Http;

namespace EasyTrader.Core.Connectors
{
    
    public interface IApi
    {
        ApiHttpResponse ApiHttpResponse { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiHttpRequest"></param>
        /// <returns></returns>
        Task<ApiHttpResponse> SendAsync(ApiHttpRequest apiHttpRequest); //*/ todo: generic api request

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">TParent for Exchange or Market objects used in Tasks</typeparam>
        /// <param name="apiHttpRequest"></param>
        /// <returns></returns>
        Task<ApiHttpResponse> OnSendAsync(ApiHttpRequest apiHttpRequest);

    }
}