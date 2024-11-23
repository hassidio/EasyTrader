using EasyTrader.Core.Common;
using EasyTrader.Core.Models.Exceptions;
using Newtonsoft.Json;


namespace EasyTrader.Core.Models.Binance
{
    public static class BinanceException
    {
        public async static Task<HttpException> GetBinanceHttpException(HttpResponseMessage response) //*/ test
        {
            using (HttpContent responseContent = response.Content)
            {
                HttpException httpException = null;
                string contentString = await responseContent.ReadAsStringAsync();
                int statusCode = (int)response.StatusCode;
                if (statusCode >= 400 && statusCode < 500)
                {
                    if (string.IsNullOrWhiteSpace(contentString))
                    {
                        httpException = new ClientException("Unsuccessful response with no content", statusCode);
                    }
                    else
                    {
                        try
                        {
                            httpException = JsonConvert.DeserializeObject<ClientException>(contentString);
                        }
                        catch (JsonReaderException ex)
                        {
                            httpException = new ClientException(SeverityEnum.Error, contentString, statusCode, ex);
                        }
                    }
                }
                else
                {
                    httpException = new ServerException(contentString);
                }

                httpException.StatusCode = statusCode;
                httpException.Headers = response.Headers.ToDictionary(a => a.Key, a => a.Value);

                return httpException;
            }
        }
    }
}
