using Newtonsoft.Json;

namespace CoinGeckoAPI.Models
{
    public class IndexListItem
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
