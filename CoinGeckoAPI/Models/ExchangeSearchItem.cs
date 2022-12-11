using Newtonsoft.Json;

namespace CoinGeckoAPI.Models
{
    public class ExchangeSearchItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("market_type")]
        public string MarketType { get; set; }

        [JsonProperty("thumb")]
        public string Thumb { get; set; }

        [JsonProperty("large")]
        public string Large { get; set; }
    }
}
