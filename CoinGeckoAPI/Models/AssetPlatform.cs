using Newtonsoft.Json;

namespace CoinGeckoAPI.Models
{
    public class AssetPlatform
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("chain_identifier")]
        public long? ChainIdentifier { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("shortname")]
        public string Shortname { get; set; }
    }
}
