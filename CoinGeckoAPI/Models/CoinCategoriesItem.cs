using Newtonsoft.Json;
using System;

namespace CoinGeckoAPI.Models
{
    public class CoinCategoriesItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("market_cap")]
        public decimal? MarketCap { get; set; }

        [JsonProperty("market_cap_change_24h")]
        public decimal? MarketCapChange24H { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("top_3_coins")]
        public string[] Top3_Coins { get; set; }

        [JsonProperty("volume_24h")]
        public decimal? Volume24H { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
