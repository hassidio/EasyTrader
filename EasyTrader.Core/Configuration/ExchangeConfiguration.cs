using Newtonsoft.Json;

namespace EasyTrader.Core.Configuration
{
    public class ExchangeConfiguration
    {
        public string ExchangeName { get; set; }

        public string DataDirectoryPath { get; set; }

        public DateTime LastUpdated { get; set; }

        public ApiConfiguration ApiConfiguration { get; set; }

    }
}
