using Newtonsoft.Json;

namespace CoinGeckoAPI.Models
{
    public class CoinPublicInterestStats
    {
        [JsonProperty("alexa_rank")]
        public long? AlexaRank { get; set; }

        [JsonProperty("bing_matches")]
        public long? BingMatches { get; set; }
    }
}
