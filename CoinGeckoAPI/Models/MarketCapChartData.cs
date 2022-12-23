using Newtonsoft.Json;

namespace CoinGeckoAPI.Models
{
    public class MarketCapChartData
    {
        [JsonProperty("market_cap")]
        public double[][] MarketCap { get; set; }

        [JsonProperty("volume")]
        public double[][] Volume { get; set; }
    }
}
