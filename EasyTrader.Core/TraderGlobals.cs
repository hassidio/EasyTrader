
using EasyTrader.Core.Common;
using EasyTrader.Core.Configuration;
using EasyTrader.Core.Models;
using EasyTrader.Core.PersistenceServices;
using EasyTrader.Core.Views.OutputView.ApiThrottleWindowView;
using EasyTrader.Core.Views.OutputView.OutputWindowView;



namespace EasyTrader.Core
{
    public static class TraderGlobals
    {
        private static Portfolio _portfolio;

        public static string AppConfigFileName = "AppConfig.json";
        public static Configurations Configurations { get; private set; }
        public static Portfolio Portfolio
        {
            get 
            {
                if(_portfolio is null && Configurations is not null)
                {
                    _portfolio = new Portfolio(Configurations.ExchangeConfigurations);
                }
                return _portfolio;
            }
        }

        /// <summary>
        /// Read AppConfig.json configuration file
        /// </summary>
        /// <param name="configurationDirectoryPath"></param>
        /// <param name="defaultExchangeName"></param>
        /// <returns>Static Configurations object</returns>
        public static Configurations ReadConfigurations(string configurationDirectoryPath)
        {
            Configurations = FileManager.ReadJsonFile<Configurations>(
                configurationDirectoryPath, AppConfigFileName);

            return Configurations;
        }
    }
}
