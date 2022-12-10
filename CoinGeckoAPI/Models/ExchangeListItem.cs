using Newtonsoft.Json;

namespace CoinGeckoAPI.Models
{
    public class ExchangeListItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
