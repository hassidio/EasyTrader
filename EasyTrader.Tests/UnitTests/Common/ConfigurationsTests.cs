using EasyTrader.Core.Configuration;
using EasyTrader.Core.Common;
using EasyTrader.Core;

namespace EasyTrader.Tests.UnitTests.Common
{
    [TestClass]
    public class ConfigurationsTests : UnitTestBase
    {
        public ConfigurationsTests()
        {
        }

        [TestMethod]
        public void Test_Read()
        {
            Assert.IsTrue(TraderGlobals.AppConfigFileName == AppConfigFileName);

            var configurations = TraderGlobals.ReadConfigurations(DataDirectoryPathTest);

            Assert.IsTrue(configurations is not null);
        }

        [Ignore]
        [TestMethod]
        public void Test_Save()
        {
            Configurations configurations = new Configurations
            {
                IsDebugEnabled = true,
                IsLoggingEnabled = true,
                LogFileName = @"Easy Trader Log TEST.txt",
                LogFilePath = @DataDirectoryPathTest
            };

            var exchangeConfiguration = new ExchangeConfiguration
            {
                ExchangeName = ExchangeNameTest,
                DataDirectoryPath = DataDirectoryPathTest,
                ApiConfiguration = new ApiConfiguration
                {
                    ApiBaseUri = @"https://api.mock",
                    ApiExchangeInfoUri = @"/exchangeInfo",
                    ApiMarketCandlesUri = @"/candels",
                    CallsPerSecondPolicy = 80,
                    WeightCooldownSecondsPolicy = 10,
                    MaximumTotalWeight = 1000,
                    ResponseHeaderWeightKeyName = "",
                }
            };

            configurations.ExchangeConfigurations.Add(exchangeConfiguration);

            FileManager.SaveJsonFile(DataDirectoryPathTest, AppConfigFileName, configurations);

            Test_Read();
        }
    }
}