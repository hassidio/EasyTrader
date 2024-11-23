
using EasyTrader.Core.Common;

namespace EasyTrader.Core.Configuration
{
    public class Configurations 
    {
        public Configurations()
        {
            ExchangeConfigurations = new HashSet<ExchangeConfiguration>();
        }

        public bool IsLoggingEnabled { get; set; }
        public bool IsDebugEnabled { get; set; }
        public string LogFilePath { get; set; }
        public string LogFileName { get; set; }

        public ICollection<ExchangeConfiguration> ExchangeConfigurations { get; set; }
    }
}
