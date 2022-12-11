using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoinGeckoAPI.Models
{
    public class CoinsListItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("platforms")]
        public Dictionary<string, string> Platforms { get; set; }
    }
}
