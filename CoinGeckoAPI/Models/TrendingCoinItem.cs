using Newtonsoft.Json;

namespace CoinGeckoAPI.Models
{
    public class TrendingCoinItem
    {
        [JsonProperty("item")]
        public CoinSearchItem CoinItem { get; set; }
    }
}
