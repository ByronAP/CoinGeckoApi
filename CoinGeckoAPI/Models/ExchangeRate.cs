using Newtonsoft.Json;

namespace CoinGeckoAPI.Models
{
    public class ExchangeRate
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("type")]
        public string AssetType { get; set; }
    }
}
