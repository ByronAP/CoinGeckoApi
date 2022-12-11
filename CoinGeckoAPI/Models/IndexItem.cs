using Newtonsoft.Json;

namespace CoinGeckoAPI.Models
{
    public class IndexItem
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("market")]
        public string Market { get; set; }

        [JsonProperty("last")]
        public decimal? Last { get; set; }

        [JsonProperty("is_multi_asset_composite")]
        public bool? IsMultiAssetComposite { get; set; }
    }
}
