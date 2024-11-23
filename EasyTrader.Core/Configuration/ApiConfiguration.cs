namespace EasyTrader.Core.Configuration
{
    public class ApiConfiguration
    {
        public string ApiBaseUri { get; set; }
        public string ApiExchangeInfoUri { get; set; }
        public string ApiMarketCandlesUri { get; set; }

        public int? CallsPerSecondPolicy { get; set; }
        public int? WeightCooldownSecondsPolicy { get; set; }
        public int? MaximumTotalWeight { get; set; }
        public string? ResponseHeaderWeightKeyName { get; set; }
    }
}
