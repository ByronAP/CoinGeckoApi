using Newtonsoft.Json;

namespace CoinGeckoAPI.Models
{
    public class CoinImage
    {
        [JsonProperty("thumb")]
        public string Thumb { get; set; }

        [JsonProperty("small")]
        public string Small { get; set; }

        [JsonProperty("large")]
        public string Large { get; set; }
    }
}
