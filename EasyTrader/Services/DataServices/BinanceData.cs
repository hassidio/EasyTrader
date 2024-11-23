using EasyTrader.Core.Models.Binance;
using Newtonsoft.Json.Linq;
using EasyTrader.Core.Connectors.Http;
using EasyTrader.Core.Views.OutputView;
using EasyTrader.Core.Configuration;
using EasyTrader.Core.DataServices;
using EasyTrader.Core.Models.Entities;

namespace EasyTrader.Services.DataServices
{
    public class BinanceData : TraderData
            <BinanceExchange,
            BinanceApi_ExchangeInfo,
            BinanceMarket,
            JArray,
            BinanceCandle,
            RestApi>
    {

        public BinanceData(
            ExchangeConfiguration exchangeConfiguration,
            IClientOutput? clientOutput = null)
            : base(exchangeConfiguration, clientOutput)
        {

        }



    }


}
