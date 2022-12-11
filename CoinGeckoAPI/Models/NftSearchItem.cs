using Newtonsoft.Json;

namespace CoinGeckoAPI.Models
{
    public class NftSearchItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("thumb")]
        public string Thumb { get; set; }
    }
}
