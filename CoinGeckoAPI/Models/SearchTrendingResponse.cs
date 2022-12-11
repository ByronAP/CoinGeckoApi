using Newtonsoft.Json;

namespace CoinGeckoAPI.Models
{
    public class SearchTrendingResponse
    {
        [JsonProperty("coins")]
        public TrendingCoinItem[] Coins { get; set; }

        [JsonProperty("exchanges")]
        public object[] Exchanges { get; set; }
    }
}
