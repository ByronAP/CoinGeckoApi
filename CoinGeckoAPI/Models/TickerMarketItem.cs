using Newtonsoft.Json;

namespace CoinGeckoAPI.Models
{
    public class TickerMarketItem
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        [JsonProperty("has_trading_incentive")]
        public bool HasTradingIncentive { get; set; }

        [JsonProperty("logo")]
        public string Logo { get; set; }
    }
}
