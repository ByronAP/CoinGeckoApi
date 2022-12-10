using Newtonsoft.Json;

namespace CoinGeckoAPI.Models
{
    public class CoinSearchItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("api_symbol")]
        public string ApiSymbol { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("market_cap_rank")]
        public int? MarketCapRank { get; set; }

        [JsonProperty("thumb")]
        public string Thumb { get; set; }

        [JsonProperty("large")]
        public string Large { get; set; }
    }
}
