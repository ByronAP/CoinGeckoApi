using Newtonsoft.Json;

namespace CoinGeckoAPI.Models
{
    public class GlobalMarketCapChartResponse
    {
        [JsonProperty("market_cap_chart")]
        public MarketCapChartData MarketCapChart { get; set; }
    }
}
