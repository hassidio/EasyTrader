using EasyTrader.Core.Models;

namespace EasyTrader.Tests.UnitTests.Sandbox
{
    [TestClass]
    public class PortfolioTests : UnitTestBase //: FixtureBase   //:ProcessTestBase 
    {
        public static string TestsOutputDirectoryPath = @"..\..\..\TestsOutput";


        public PortfolioTests()
        {
        }

        [TestMethod]
        public void Sandbox2_Test()
        {
            var p = new Portfolio(MockConfigurations.ExchangeConfigurations);

            var ec = p.GetExchangeConfiguration(ExchangeNameTest);

          

        }
    }

}